using System.Data;

namespace NeuralNetworkDemo;

public static class Main
{
public static void CreateNeuralNetwork(int[] neurons)
    {
        for (int i = 0; i < neurons.Length; i++)
        {
            for (int j = 0; j < neurons[i]; j++)
            {
                new Neuron();
                Console.WriteLine("New Neuron");
            }
        }
        //Neuron neuron1 = new Neuron(5.4);
    }
}