using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.AI.MachineLearning;
using Windows.Graphics.Imaging;
using Windows.Media;
using Windows.Storage;
using MLANELib.WinML;
using TuaRua.FreSharp;
using TuaRua.FreSharp.Exceptions;
using FREObject = System.IntPtr;
using FREContext = System.IntPtr;

#pragma warning disable 4014

namespace MLANELib {
    public interface IMachineLearningInput { }

    public class MainController : FreSharpMainController {
        private SqueezeNetModel _model;
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

        private FREObject Predict(FREContext ctx, uint argc, FREObject[] argv) {
            if (argv[0] == FREObject.Zero) {
                return FREObject.Zero;
            }

            if (argv[1] == FREObject.Zero) {
                return FREObject.Zero;
            }

            var imagePath = argv[0].AsString();
            var modelPath = argv[1].AsString();
            try {
                EvaluateImageAsync(imagePath, modelPath);
            }
            catch (Exception e) {
                return new FreException(e).RawValue;
            }

            return FREObject.Zero;
        }

        private async Task EvaluateImageAsync(string imagePath, string modelPath) {
            var selectedStorageFile = await StorageFile.GetFileFromPathAsync(imagePath);
            SoftwareBitmap softwareBitmap;
            using (var stream = await selectedStorageFile.OpenAsync(FileAccessMode.Read)) {
                // Create the decoder from the stream 
                var decoder = await BitmapDecoder.CreateAsync(stream);
                // Get the SoftwareBitmap representation of the file in BGRA8 format
                softwareBitmap = await decoder.GetSoftwareBitmapAsync();
                softwareBitmap = SoftwareBitmap.Convert(softwareBitmap, BitmapPixelFormat.Bgra8,
                    BitmapAlphaMode.Premultiplied);
            }

            // Encapsulate the image within a VideoFrame to be bound and evaluated
            var inputImage = VideoFrame.CreateWithSoftwareBitmap(softwareBitmap);

            if (_model == null) {
                var modelFile = await StorageFile.GetFileFromPathAsync(modelPath);
                _model = new SqueezeNetModel {LearningModel = await LearningModel.LoadFromStorageFileAsync(modelFile)};
                _model.Session = new LearningModelSession(_model.LearningModel,
                    new LearningModelDevice(LearningModelDeviceKind.Default));
                _model.Binding = new LearningModelBinding(_model.Session);
            }

            if (_model == null) {
                return;
            }

            var input = new SqueezeNetInput {
                Image = ImageFeatureValue.CreateFromVideoFrame(inputImage)
            };

            try {
                var output = (SqueezeNetOutput) await _model.EvaluateAsync(input);
                var (label, probability) = output.classLabelProbs.FirstOrDefault();
                DispatchEvent(Result, probability + ", " + label);
            }
            catch (Exception ex) {
                Trace(ex.Message, ex.StackTrace);
            }
        }

        private FREObject InitController(FREContext ctx, uint argc, FREObject[] argv) {
            return FREObject.Zero;
        }

        public override void OnFinalize() { }

        public override string TAG => "MainController";
    }
}