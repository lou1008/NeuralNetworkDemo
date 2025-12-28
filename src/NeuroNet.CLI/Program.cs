using NeuroNet.Core;
namespace NeuroNet.CLI;

class Program
{
    static void Main(string[] args)
    {
        int UserOutput;
        bool Error;
        List<List<Neuron>>? LoadedNetwork = null;
        string? currentnnName = null;
        do
        {
            
            Error = false;
            Console.Clear();
            Console.WriteLine("Neural Network Demo");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Create a NeuralNetwork");
            Console.WriteLine("2. Load a NeuralNetwork");
            if (!int.TryParse(Console.ReadLine(), out UserOutput))
            {
                Console.WriteLine("Please type in a valid number");
                Error = true;
            }
            switch (UserOutput)
            {
                case 1:
                    var OutputCreate = Create.CreateNeuralNetwork(Console.WriteLine, () => Console.ReadLine() ?? string.Empty);
                    LoadedNetwork = OutputCreate.Value1;
                    currentnnName = OutputCreate.Value2;
                    break;
                case 2:
                    var result = Load.LoadNeuralNetwork(Console.WriteLine, () => Console.ReadLine() ?? string.Empty);
                    if (result.HasError)
                    {
                        Error = true;
                    }
                    else
                    {
                        LoadedNetwork = result.Value;
                    }
                    break;
                default:
                    Console.WriteLine("Please type in one of the shown options");
                    Error = true;
                    break;
            }
        } while (Error);

        Extras.PressKey();

        Console.WriteLine("Do you want to run the Neural Network now? (y/n)");
        if(Console.ReadLine()!.ToLower() == "y")
        {
            List<double> inputData = new List<double>();
            int InputLenght = LoadedNetwork![0].Count;
            do {
                Error = false;
                Console.WriteLine($"You have to Input {InputLenght} Values.");
                Console.WriteLine("Please enter input data separated by commas (e.g., 0.5,0.2,0.8):");
                string? inputLine = Console.ReadLine();
                if (!string.IsNullOrEmpty(inputLine))
                {
                    try {
                        inputData = inputLine.Split(',').Select(s => double.Parse(s.Trim())).ToList();
                        if(inputData.Count != InputLenght)
                        {
                            Console.WriteLine($"Invalid number of inputs. Expected {InputLenght} values.");
                            Console.WriteLine($"You entered {inputData.Count} values.");
                            Console.Write("Press any key to continue...");
                            Console.ReadKey();
                            Error = true;
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input format. Please ensure you enter numbers separated by commas.");
                        Console.Write("Press any key to continue...");
                        Console.ReadKey();
                        Error = true;
                    }
                }
                else
                {
                    string defaultInput = string.Join(",", Enumerable.Repeat("0.0", InputLenght));
                    Console.WriteLine("No input data provided. Using default input data: " + defaultInput);
                    inputData = new List<double>(Enumerable.Repeat(0.0, InputLenght));
                }
            }
            while(Error);
            Edit.RandomizeIfNeeded(LoadedNetwork!, (message) => Console.WriteLine(message));
            Console.WriteLine("Do you want to save randomized weights and biases? (y/n)");
            if(Console.ReadLine()!.ToLower() == "y")
            {
                Save.SaveNetwork("Emma" ,LoadedNetwork!, "overwrite", Console.WriteLine); // Hardcoded name for testing
            }
            Run.RunNeuralNetwork(LoadedNetwork!, inputData, (message) => Console.WriteLine(message));
            Console.WriteLine("Running Neural Network...");
        }
        Console.WriteLine("Exiting Program...");
        Extras.PressKey();
    }
}
