using System.Collections.Generic;

namespace MLANELib.WinML {
    public sealed class GoogLeNetPlacesModelOutput : IMachineLearningOutput {
        public IList<string> SceneLabel { get; set; }
        public IDictionary<string, float> SceneLabelProbs { get; set; }

        public GoogLeNetPlacesModelOutput() {
            SceneLabel = new List<string>();
            SceneLabelProbs = new Dictionary<string, float>();
            for (var i = 0; i < 205; i++) {
                SceneLabelProbs[i.ToString()] = float.NaN;
            }
        }
    }
}