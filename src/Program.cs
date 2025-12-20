using System;
using System.Runtime.InteropServices;
using NeuralNetworkDemo;
using static NeuralNetworkDemo.Main;

partial class Program
{
    static void Main()
    {
        string baseDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string appDataPath = Path.Combine(baseDataPath, "NeuroNet");
        int UserOutput;
        bool Error;
        do {
            Error = false;
            Console.Clear();
            Console.WriteLine("Neural Network Demo");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Create a NeuralNetwork");
            Console.WriteLine("2. Open a NeuralNetwork (Coming Soon)");
            if (!int.TryParse(Console.ReadLine(), out UserOutput)){
                Console.WriteLine("Please type in a valid number");
                Error = true;
            }
            switch (UserOutput) {
                case 1:
                    CreateNeuralNetwork();
                    break;
                case 2:
                    Console.WriteLine("Feature coming soon!");
                    Error = true;
                    break;
                default:
                    Console.WriteLine("Please type in one of the shown options");
                    Error = true;
                    break;
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        } while (Error);
    }
}