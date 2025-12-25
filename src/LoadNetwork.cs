using System;
using System.Runtime.InteropServices;
using static NeuroNet.Main;
using static NeuroNet.Filemanagement;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using MeuroNet;

namespace NeuroNet
{
    public static class LoadNetwork
    {
        public static MultipleValues<List<List<Neuron>>> LoadNeuralNetwork()
        {
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
                return new MultipleValues<List<List<Neuron>>>
                {
                    Value = null!,
                    HasError = true,
                    ErrorMessage = "Failed to load Neural Network."
                };
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
                return new MultipleValues<List<List<Neuron>>>
                {
                    Value = null!,
                    HasError = true,
                    ErrorMessage = "Deserialization Error."
                };

            }
            for(int i=0; i < network!.Count; i++)
            {
                for(int j=0; j < network[i].Count; j++)
                {
                    if(i == 0)
                    {
                        network[i][j].EditWeights(new double[1]);
                    }
                    else
                    {
                        network[i][j].EditWeights(new double[network[i - 1].Count]);
                    }
                }
            }


            Console.WriteLine("Neural Network Structure:");
            foreach (var layer in network!)
            {
                Console.WriteLine("Layer with " + layer.Count + " neurons.");
            }
            return new MultipleValues<List<List<Neuron>>>
            {
                Value = network,
                HasError = false,
                ErrorMessage = string.Empty
            };
        }
    }
}