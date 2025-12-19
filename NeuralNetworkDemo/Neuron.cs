namespace NeuralNetworkDemo;
class Neuron
{
    private double bias;
    private double[] weights;
    private double value;

    public Neuron(double bias, double[] weights)
    {
        Console.WriteLine("New neuron created");
        this.bias = bias;
        this.weights = weights;
        this.value = 0;
        Console.WriteLine($"Value of the neuron is { this.value }");
    }
    public double Fire(double[] inputs)
    {
        if (inputs.Length != weights.Length)
        {
            throw new ArgumentException("Input length must match weights length.");
        }

        double totalInput = bias;
        for (int i = 0; i < inputs.Length; i++)
        {
            totalInput += inputs[i] * weights[i];
        }

        value = Functions.sigmoid(totalInput);
        return value;
    }
}