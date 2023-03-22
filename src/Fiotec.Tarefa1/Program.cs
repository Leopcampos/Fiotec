using Fiotec.Tarefa1;
using System.Configuration;

string folderPath = @"C:\Mensagens\";
string fileExtension = "*.txt";
string connectionString = ConfigurationManager.ConnectionStrings["Emails"].ConnectionString;

try
{
    EmailProcessor processor = new EmailProcessor(folderPath, fileExtension, connectionString);
    processor.VerificarEmail();
}
catch (Exception ex)
{
    Console.WriteLine("Ocorreu um erro ao processar os e-mails: " + ex.Message);
}