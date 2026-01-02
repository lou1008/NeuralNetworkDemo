using static NeuroNet.Core.Load;
namespace NeuroNet.CLI;


public class Learn
{
    public static void UserDialoge()
    {
        bool Error;
        do {
            Error = false;
            Console.WriteLine("Enter the path to your data file (currently only CSV files(;) are supported)");
            Console.WriteLine("Type e for an Explanation how the CSV should be formatted");
            string? path = Console.ReadLine();
            if(path == "e")
            {
                Console.WriteLine("The CSV file must be structured in pairs of lines:");
                Console.WriteLine("- The first line contains the input values");
                Console.WriteLine("- The second line contains the expected output values");
                Console.WriteLine();
                Console.WriteLine("Each pair of lines represents one training dataset.");
                Console.WriteLine();
                Console.WriteLine("Example:");
                Console.WriteLine("0.2,0.5,1.0");
                Console.WriteLine("1");
                Console.WriteLine("0.1,0.9,0.3");
                Console.WriteLine("0");
                Console.WriteLine();
                Extras.PressKey();
                Error = true;
            }
            else if (!Directory.Exists(path)) {
                Console.WriteLine("You need to insert a existing path.");
                Extras.PressKey();
                Error = true;
            }
            else
            {
                double[,] Data = GetCSVData(path);
                //Todo: Check if the File is a vald File
                //Todo: Convert the double[rows,lines] to double[dataset,input/output,entry]
            }
        } while (Error) ;
    }
}