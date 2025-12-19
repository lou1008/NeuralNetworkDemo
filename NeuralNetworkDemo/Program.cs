using System;
using NeuralNetworkDemo;
using static NeuralNetworkDemo.Main;

partial class Program
{
    static void Main()
    {
        int[] testmenge = new int[] { 1, 5, 9, 7 };

        int UserOutput;
        do {
            Console.Clear();
            Console.WriteLine("Neural Network Demo");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Create a NeuralNetwork");
            if (!int.TryParse(Console.ReadLine(), out UserOutput)){
                Console.WriteLine("ERROR: INPUT IS NOT A NUMBER!");
                UserOutput = 0;
            }
            switch (UserOutput) {
                case 1:
                    CreateNeuralNetwork(testmenge);
                    break;
                default:
                    Console.WriteLine("Please type in one of the shown options");
                    break;
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        } while (UserOutput != 3);
    }
}


