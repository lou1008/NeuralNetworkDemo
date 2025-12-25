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
            string? nnName = Console.ReadLine();
            string networkData = Nameof(nnName);
            if (string.IsNullOrEmpty(networkData))
            {
                Console.WriteLine("Failed to load Neural Network.");
                return new MultipleValues<List<List<Neuron>>>
                {
                    Value = null!,
                    HasError = true,
                    ErrorMessage = "Failed to load Neural Network."
                };
            }
            List<List<NeuronDto>>? networkDto;
            try
            {
                var file = JsonSerializer.Deserialize<FileDto>(networkData) ?? throw new Exception("Deserialized file is null."); // I don't like this but it works for now
                networkDto = file.Network;
            }
            catch (Exception e)
            {
                GitHubReportIssue.ReportToGitHub("Failed to Load Neural Network", e.Message, e.StackTrace ?? "No stack trace available.", "Deserialization Error in LoadNeuralNetwork", true);
                return new MultipleValues<List<List<Neuron>>>
                {
                    Value = null!,
                    HasError = true,
                    ErrorMessage = "Deserialization Error."
                };
            }
            List<List<Neuron>>? network = networkDto?.Select(layer => layer.Select(neuronDto => neuronDto.ToNeuron()).ToList()).ToList();

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