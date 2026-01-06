using NeuroNet.Core;

namespace NeuroNet.CLI;

class SaveCLI
{
    public static string SaveNetworkToFile(List<List<Neuron>> network, string status, string? currentnnName = null)
    {
        Console.WriteLine("Do you want to save this Neural Network? (y/n)");
        string saveResponse = Console.ReadLine() ?? string.Empty;
        string nnName;
        if(saveResponse.ToLower() == "y")
        {
            switch (status) {
                case "new":
                    Console.WriteLine("How do you name the Neural Network?");
                    do
                    {
                        nnName = Console.ReadLine()!;
                        if(string.IsNullOrEmpty(nnName)) Console.WriteLine("Please name your Network Properly");
                    }
                    while (string.IsNullOrWhiteSpace(nnName));
                    var invalidChars = Path.GetInvalidFileNameChars();
                    foreach (var c in invalidChars)
                    {
                        nnName = nnName.Replace(c, '_');
                    }
                    break;
                case "override":
                    nnName = currentnnName ?? throw new Exception("No current Network loaded");
                    break;
                default:
                    Console.WriteLine("Something went wrong. Please report an Issue on GitHub and restart the program");
                    throw new Exception();
            }
            string Message = Save.SaveNetwork(nnName, network, status, Console.WriteLine, () => Console.ReadLine() ?? string.Empty);
            switch(Message)
            {
                case "done":
                    Console.WriteLine("Neural Network successfully saved as " + nnName);
                    break;
                case "already existing":
                    break;
                default:
                    Console.WriteLine("An error occured. Please Report the Issue on GitHub.");
                    
                    break;
            }
            
            return nnName;
        }
        else
        {
           Console.WriteLine("Neural Network not saved.");
           return "NoName";
        }
    }
}