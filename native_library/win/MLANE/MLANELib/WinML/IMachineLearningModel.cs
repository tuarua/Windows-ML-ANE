using System.Threading.Tasks;
using Windows.AI.MachineLearning.Preview;

namespace MLANELib.WinML {
    public interface IMachineLearningModel {
        LearningModelPreview LearningModel { get; set; }

        Task<IMachineLearningOutput> EvaluateAsync(IMachineLearningInput input);
    }
}