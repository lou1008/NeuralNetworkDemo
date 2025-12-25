using System;
namespace MeuroNet;

public class MultipleValues<T>
{
    public T? Value { get; set; }
    public bool HasError { get; set; }
    public string? ErrorMessage { get; set; }
}
