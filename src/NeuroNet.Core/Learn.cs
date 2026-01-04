using System.Reflection.Metadata;

namespace NeuroNet.Core;

public class Learn {
    public static List<List<Neuron>> Learning(List<List<Neuron>> network, double[] input, double[] Expected_Output) {
        return network;
    }
    public double[] Cost(double[] output, double[] expected_output)
    {
        if(output.Length != expected_output.Length) { throw new Exception("Input and expected output must have the same length"); }
        double[] difference = new double[output.Length];
        for(int i = 0; i < output.Length; i++)
        {
            difference[i] = Math.Pow(output[i] - expected_output[i], 2);
        }
        return difference;
    }
    public double overall_Cost(double[] output, double[] expected_output)
    {
        double[] difference = Cost(output, expected_output);
        double overall = 0;
        for(int i = 0; i < difference.Length; i++)
        {
            overall += difference[i];
        }
        return overall;
    }
}