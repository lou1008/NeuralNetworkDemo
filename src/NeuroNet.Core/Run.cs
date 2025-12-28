using static NeuroNet.Core.Edit;
namespace NeuroNet.Core;

public class Run
{
    public static void RunNeuralNetwork(List<List<Neuron>> network, List<double> inputData, Action<string>? Message)
    {
        for (int i = 0; i < network.Count; i++)
        {
            for (int j = 0; j < network[i].Count; j++)
            {
                Message?.Invoke("Processing Neuron " + (j + 1) + " in Layer " + (i + 1));
                if (i == 0)
                {
                    network[i][j].Fire([inputData.ToArray()[j]]);
                }
                else
                {
                    double[] inputs = network[i - 1].Select(neuron => neuron.value).ToArray();
                    network[i][j].Fire(inputs);
                }
            }
        }
        Message?.Invoke("Neural Network Output:");
        for (int j = 0; j < network[network.Count - 1].Count; j++)
        {
            Message?.Invoke($"Neuron {j + 1}: {network[network.Count - 1][j].value}");
        }
    }
}