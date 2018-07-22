using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.AI.MachineLearning.Preview;
using Windows.Graphics.Imaging;
using Windows.Media;
using Windows.Storage;
using Windows.Storage.Streams;
using MLANELib.WinML;
using TuaRua.FreSharp;
using TuaRua.FreSharp.Display;
using TuaRua.FreSharp.Exceptions;
using FREObject = System.IntPtr;
using FREContext = System.IntPtr;

#pragma warning disable 4014

namespace MLANELib {
    public interface IMachineLearningInput { }


    public class MainController : FreSharpMainController {
        private GoogLeNetPlacesModel _model;
        private const string Result = "MLANE.OnModelResult";

        // Must have this function. It exposes the methods to our entry C++.
        public string[] GetFunctions() {
            FunctionsDict =
                new Dictionary<string, Func<FREObject, uint, FREObject[], FREObject>> {
                    {"init", InitController},
                    {"predict", Predict}
                };
            return FunctionsDict.Select(kvp => kvp.Key).ToArray();
        }

        public FREObject Predict(FREContext ctx, uint argc, FREObject[] argv) {
            if (argv[0] == FREObject.Zero) return FREObject.Zero;
            if (argv[1] == FREObject.Zero) return FREObject.Zero;
            var modelPath = argv[1].AsString();
            var bmd = new FreBitmapDataSharp(argv[0]);
            bmd.Acquire();
            var ptr = bmd.Bits32;
            var byteBuffer = new byte[bmd.LineStride32 * bmd.Height * 4];
            Marshal.Copy(ptr, byteBuffer, 0, byteBuffer.Length);
            bmd.Release();
            try {
                EvaluteImageAsync(byteBuffer.AsBuffer(), bmd.Width, bmd.Height, modelPath);
            }
            catch (Exception e) {
                return new FreException(e).RawValue;
            }

            return FREObject.Zero;
        }

        private async Task EvaluteImageAsync(IBuffer buffer, int width, int height, string modelPath) {
            var softwareBitmap =
                SoftwareBitmap.CreateCopyFromBuffer(buffer, BitmapPixelFormat.Bgra8, width, height);
            var inputImage = VideoFrame.CreateWithSoftwareBitmap(softwareBitmap);

            if (_model == null) {
                var modelFile = await StorageFile.GetFileFromPathAsync(modelPath);
                _model = new GoogLeNetPlacesModel();
                var learningModel = await LearningModelPreview.LoadModelFromStorageFileAsync(modelFile);
                _model.LearningModel = learningModel;
            }

            try {
                if (_model != null) {
                    var startTime = DateTime.Now;
                    var input = new GoogLeNetPlacesModelInput {
                        sceneImage = inputImage
                    };
                    if (await _model.EvaluateAsync(input) is GoogLeNetPlacesModelOutput res) {
                        var results = res.SceneLabelProbs.Select(kv => new LabelResult {
                                Label = kv.Key,
                                Result = (float) Math.Round(kv.Value * 100, 2)
                            })
                            .ToList();
                        results.Sort((p1, p2) => p2.Result.CompareTo(p1.Result));
                        var costTime = (DateTime.Now - startTime).TotalSeconds;
                        Trace("Time taken to evaluate", costTime, "seconds");
                        DispatchEvent(Result, res.SceneLabel.FirstOrDefault());
                    }
                }
            }
            catch (Exception ex) {
                Trace(ex.Message, ex.StackTrace);
            }
        }

        public FREObject InitController(FREContext ctx, uint argc, FREObject[] argv) {
            return FREObject.Zero;
        }

        public override void OnFinalize() { }
    }
}