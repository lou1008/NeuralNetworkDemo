using System.Text.Json;

namespace NeuroNet.Core;

public class Save
{
    static string baseDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    static string appDataPath = Path.Combine(baseDataPath, "NeuroNet");

    public static void SaveNetworkToFile(string nnName, string jsonData, Action<string>? Message = null)
    {
        if (!Directory.Exists(appDataPath))
        {
            Directory.CreateDirectory(appDataPath);
        }
        string filePath = Path.Combine(appDataPath, nnName + ".nn");
        File.WriteAllText(filePath, jsonData);
        Message?.Invoke("Neural Network saved as " + nnName);
    }

    public static void SaveNetwork(string nnName, List<List<Neuron>> network, string status, Action<string>? Message = null)
    {
        if (status == "new" && File.Exists(Path.Combine(appDataPath, nnName + ".nn")))
        {
            Message?.Invoke("Neural Network already exists. Do you want to overwrite it? (y/n)");
            string overwriteChoice = Console.ReadLine() ?? "n";
            if (overwriteChoice.ToLower() != "y")
            {
                Message?.Invoke("Neural Network not saved.");
                return;
            }
        }
        if (status == "overwrite" && File.Exists(Path.Combine(appDataPath, nnName + ".nn")))
        {
            Message?.Invoke("Overwriting existing Neural Network: " + nnName);
        }
        if (status == "overwrite" && !File.Exists(Path.Combine(appDataPath, nnName + ".nn")))
        {
            Message?.Invoke("Neural Network does not exist. Saving as new Neural Network: " + nnName);
            GitHubReportIssue.ReportToGitHub(
                "Attempted to overwrite a non-existing Neural Network.",
                "",
                "",
                "The user tried to overwrite a Neural Network that does not exist.",
                true
            );
        }
        List<List<NeuronDto>> dtoNetwork = new List<List<NeuronDto>>();
        foreach(var layer in network)
        {
            List<NeuronDto> dtoLayer = new List<NeuronDto>();
            foreach(var neuron in layer)
            {
                dtoLayer.Add(neuron.ToDto());
            }
            dtoNetwork.Add(dtoLayer);
        }
        string contentJson = JsonSerializer.Serialize(dtoNetwork);
        Message?.Invoke($"Debug: JSON Original: {JsonSerializer.Serialize(network)}");
        Message?.Invoke($"Debug: Content JSON: {contentJson}");
        string baseDir = AppContext.BaseDirectory;
        Message?.Invoke($"Debug: Base Directory: {baseDir}");
        string appVersionPath = Path.Combine(baseDir, "../../../../AppVersion.json");
        string versionInfo =
                JsonSerializer
                    .Deserialize<Dictionary<string, string>>(
                        File.ReadAllText(appVersionPath)
                    )?["version"]
                ?? "Unknown Version";
        var metadata = new
        {
            Name = nnName,
            Type = "Default",
            Version = 0.1,
            Description = "A Neural Network created with NeuroNet.",
            GitHub = "https://github.com/aichlou/NeuroNet",
            VersionOfProramm = versionInfo,
            CreatedAt = DateTime.UtcNow,
        };

        string metadataJson = JsonSerializer.Serialize(metadata);
        string combinedJson = "{ \"Metadata\": " + metadataJson + ", \"Network\": " + contentJson + " }";
        SaveNetworkToFile(nnName, combinedJson);
    }
}