using System.Net.NetworkInformation;
using NeuroNet.Core;

namespace NeuroNet.CLI;

public class CreateCLI
{
    public static List<List<Neuron>> CreatingProcess()
    {
        bool Error;
        int layers = 0;
        do
        {
            Error = false;
            Console.WriteLine("How many layers would you like your Neural Network to have? (Including input and output layers)");
            layers = int.TryParse(Console.ReadLine() ?? string.Empty, out int parsedLayers) ? parsedLayers : 0;
            if (layers == 0)
            {
                Console.WriteLine("Please enter a valid number for the layers.");
                Error = true;
            }
            else if (layers < 2)
            {
                Console.WriteLine("A Neural Network must have at least 2 layers (input and output layers).");
                Error = true;
            }
            else if (layers > 100)
            {
                Console.WriteLine("The maximum number of layers is 100.");
                Error = true;
            }
        } while (Error);
        int[] networkData = new int[layers];
        for (int i = 0; i < layers; i++)
        {
            string layerType;
            if (i == 0)
            {
                layerType = " (Input Layer)";
            }
            else if (i == layers - 1)
            {
                layerType = " (Output Layer)";
            }
            else
            {
                layerType = " (Hidden Layer)";
            }
            Console.WriteLine("How many neurons would you like in layer " + (i + 1) + layerType + "?");
            do {
                Error = false;
                int neuronCount = int.TryParse(Console.ReadLine() ?? string.Empty, out int parsedNeuronCount) ? parsedNeuronCount : 0;
                if(neuronCount <= 0)
                {
                    Console.WriteLine("Invalid neuron count, defaulting to 1 neuron? (y/n)");
                    string response = Console.ReadLine() ?? string.Empty;
                    if(response.ToLower() == "y") {
                        Console.WriteLine("Confirmed");
                        neuronCount = 1; 
                    }
                    else {
                        Console.WriteLine("Not Confirmed");
                        Console.WriteLine("Please enter a valid number for the neurons in layer " + (i + 1) );
                        Error = true;
                    }
                }
                else if(neuronCount > 100000)
                {
                    Console.WriteLine("The maximum number of neurons per layer is 100000. Defaulting to 1000000? (y/n)");
                    string response = Console.ReadLine() ?? string.Empty;
                    if(response.ToLower() == "y") {
                        Console.WriteLine("Confirmed");
                        neuronCount = 1; 
                    }
                    else {
                        Console.WriteLine("Not Confirmed");
                        Console.WriteLine("Please enter a valid number for the neurons in layer " + (i + 1) );
                        Error = true;
                    }
                }
                else
                {
                    networkData[i] = neuronCount;
                }
            } while (Error);
        }
        Console.WriteLine("Creating Neural Network...");
        List<List<Neuron>> network = Create.CreateNeuralNetwork(networkData ,() => Console.ReadLine() ?? string.Empty, Console.WriteLine);
        Console.WriteLine("Neural Network created with " + layers + " layers.");
        return network;
    }
}