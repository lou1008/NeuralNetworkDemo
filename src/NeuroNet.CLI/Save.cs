using NeuroNet.Core;

namespace NeuroNet.CLI;

class SaveCLI
{
    public static string SaveNetworkToFile(List<List<Neuron>> network)
    {
        Console.WriteLine("Do you want to save this Neural Network? (y/n)");
        string saveResponse = Console.ReadLine() ?? string.Empty;
        string nnName;
        if(saveResponse.ToLower() == "y")
        {
            Console.WriteLine("How do you name the Neural Network?");
            do
            {
                Console.WriteLine("Please name your Network Properly");
                nnName = Console.ReadLine()!;
            }
            while (string.IsNullOrWhiteSpace(nnName));
            var invalidChars = Path.GetInvalidFileNameChars();
            foreach (var c in invalidChars)
            {
                nnName = nnName.Replace(c, '_');
            }
            Save.SaveNetwork(nnName, network, "new", Console.WriteLine, () => Console.ReadLine() ?? string.Empty);
            Console.WriteLine("Neural Network saved as " + nnName); //Todo: Add Error Message to Function above
            return nnName;
        }
        else
        {
           Console.WriteLine("Neural Network not saved.");
           return "NoName";
        }
    }
}