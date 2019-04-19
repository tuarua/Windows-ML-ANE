using Windows.AI.MachineLearning;

namespace MLANELib.WinML {
    public class SqueezeNetInput : IMachineLearningInput {
        public ImageFeatureValue Image { get; set; }
    }
}