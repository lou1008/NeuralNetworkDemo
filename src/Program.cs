using System;
using System.Runtime.InteropServices;
using NeuroNet;
using static NeuroNet.Main;
using static NeuroNet.Filemanagement;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using static NeuroNet.LoadNetwork;

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
            Error = LoadNeuralNetwork();
            break;
        default:
            Console.WriteLine("Please type in one of the shown options");
            Error = true;
            break;
    }
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
} while (Error);