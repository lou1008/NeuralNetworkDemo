using System;
using System.Runtime.InteropServices;
using NeuroNet;
using static NeuroNet.Main;
using static NeuroNet.Filemanagement;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

int UserOutput;
bool Error;
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
            CreateNeuralNetwork();
            break;
        case 2:
            Console.WriteLine("Loading Neural Network...");
            ListSavedNetworks();
            Console.WriteLine("Please type in the name of the Neural Network you would like to load:");
            string nnName = Console.ReadLine() ?? "MyNeuralNetwork";
            string networkData = Filemanagement.LoadNetworkFromFile(nnName);
            if (!string.IsNullOrEmpty(networkData))
            {
                Console.WriteLine("Neural Network loaded successfully.");
            }
            else
            {
                Console.WriteLine("Failed to load Neural Network.");
                Error = true;
                break;
            }
            List<List<Neuron>>? network;
            try
            {
                network = JsonSerializer.Deserialize<List<List<Neuron>>>(networkData);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error deserializing Neural Network: " + e.Message);
                Console.WriteLine("Do you want to report the Issue to GitHub? (y/n)");
                string reportChoice = Console.ReadLine() ?? "y";
                if (reportChoice.ToLower() == "y")
                {
                    Console.WriteLine("Opening GitHub Issues page...");
                    string title = "Deserialization Error in NeuroNet";
                    string body = $"Error Message: {e.Message}\n\nStack Trace:\n{e.StackTrace}\n\nPlease investigate the Neuron class serialization.";
                    string url = $"https://github.com/aichlou/NeuroNet/issues/new?title={Uri.EscapeDataString(title)}&body={Uri.EscapeDataString(body)}";
                    try
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = url,
                            UseShellExecute = true
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Failed to open browser: " + ex.Message);
                    }
                }
                Error = true;
                break;
            }
            Console.WriteLine("Neural Network Structure:");
            foreach (var layer in network!)
            {
                Console.WriteLine("Layer with " + layer.Count + " neurons.");
            }
            break;
        default:
            Console.WriteLine("Please type in one of the shown options");
            Error = true;
            break;
    }
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
} while (Error);
