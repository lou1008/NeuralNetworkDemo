namespace NeuroNet.Core;

public class Edit
{
    public static bool allWeightsZero(List<List<Neuron>> network)
    {
        for (int i = 0; i < network.Count; i++)
        {
            for (int j = 0; j < network[i].Count; j++)
            {
                if(network[i][j].weights.All(x => x != 0))
                {
                    return false;
                }
                if(network[i][j].bias != 0)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public static List<List<Neuron>> RandomizeWeights(List<List<Neuron>> network)
    {
        Random rand = new Random();
        for (int i = 0; i < network.Count; i++)
        {
            for (int j = 0; j < network[i].Count; j++)
            {
                network[i][j].RandomizeWeights(rand);
            }
        }
        return network;
    }
}