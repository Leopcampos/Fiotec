using Fiotec.Tarefa2.Models;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        string path = @"C:\Mensagens\"; // altere o caminho para a pasta onde estão os e-mails
        List<string> emails = new List<string>();

        foreach (string file in Directory.GetFiles(path, "*.txt"))
        {
            emails.Add(File.ReadAllText(file, Encoding.UTF8));
        }

        Dictionary<string, int> wordFrequency = WordFrequencyCalculator.Calculate(emails);
        List<string> keywords = KeywordCategorizer.Categorize(wordFrequency);

        string outputFile = @"C:\Temp\Classificação.txt";
        FileWriter.Write(keywords, outputFile);

        Console.WriteLine("Arquivo Classificação.txt criado com sucesso!!!");
    }
}