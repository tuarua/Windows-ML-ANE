using Windows.AI.MachineLearning;

namespace MLANELib.WinML {
    public class SqueezeNetInput : IMachineLearningInput {
        public ImageFeatureValue image { get; set; }
    }
}