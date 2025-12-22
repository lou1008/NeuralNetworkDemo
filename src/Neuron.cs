namespace NeuroNet;
class Neuron
{
    public double bias;
    public double[] weights;
    public double value;
    public Neuron()
    {
        this.bias = 0;
        this.weights = Array.Empty<double>();
        this.value = 0;
    }
    public Neuron(double bias, double[] weights)
    {
        this.bias = bias;
        this.weights = weights;
        this.value = 0;
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