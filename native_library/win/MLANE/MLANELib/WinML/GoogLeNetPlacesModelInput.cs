using Windows.Media;

namespace MLANELib.WinML {
    public sealed class GoogLeNetPlacesModelInput : IMachineLearningInput {
        public VideoFrame sceneImage { get; set; }
    }
}