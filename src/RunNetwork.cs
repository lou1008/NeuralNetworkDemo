namespace NeuroNet;

public static class RunNetwork
{
    public static void RunNeuralNetwork(List<List<Neuron>> network, List<double> inputData)
    {
        for (int i = 0; i < network.Count; i++)
        {
            for (int j = 0; j < network[i].Count; j++)
            {
                if (i == 0)
                {
                    network[i][j].Fire(inputData.ToArray());
                }
                else
                {
                    double[] inputs = network[i - 1].Select(neuron => neuron.value).ToArray();
                    network[i][j].Fire(inputs);
                }
            }
        }
    }
}