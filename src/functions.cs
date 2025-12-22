namespace NeuroNet;

public static class Functions
{
    public static double sigmoid(double x)
    {
        return 1 / (1 + Math.Exp(-x));
    }
}