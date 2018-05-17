using System;
using System.Threading.Tasks;
using Windows.AI.MachineLearning.Preview;

namespace MLANELib.WinML {
    public sealed class GoogLeNetPlacesModel : IMachineLearningModel {
        public LearningModelPreview LearningModel { get; set; }

        public async Task<IMachineLearningOutput> EvaluateAsync(IMachineLearningInput input) {
            var modelInput = input as GoogLeNetPlacesModelInput;
            var output = new GoogLeNetPlacesModelOutput();
            var binding = new LearningModelBindingPreview(LearningModel);
            binding.Bind("sceneImage", modelInput?.sceneImage);
            binding.Bind("sceneLabel", output.SceneLabel);
            binding.Bind("sceneLabelProbs", output.SceneLabelProbs);
            await LearningModel.EvaluateAsync(binding, string.Empty);
            return output;
        }
    }
}