using System.Collections.Generic;
using Windows.AI.MachineLearning;

namespace MLANELib.WinML {
    public class SqueezeNetOutput : IMachineLearningOutput {
        //  public TensorString classLabel; // shape(-1,1)
        public List<(string label, float probability)> classLabelProbs;
        public SqueezeNetOutput() { }
    }
}