using NeuroNet.Core;

namespace NeuroNet.CLI;

class EditCLI
{
    public static List<List<Neuron>> RandomizeIfNeeded(List<List<Neuron>> network, string currentnnName)
    {
        bool allWeightsZero = Edit.allWeightsZero(network);
        if (allWeightsZero) {
            network = Edit.RandomizeWeights(network);
            Console.WriteLine("The weights were randomized since they were all null");
            Console.WriteLine("Do you want to save the new weights to the file?");
            if((Console.ReadLine() ?? string.Empty).ToLower() == "y")
            {
                SaveCLI.SaveNetworkToFile(network, "overwrite", currentnnName);
            }
            else
            {
                Console.WriteLine("Changes not saved to file");
            }
        }
        return network;
    }
}