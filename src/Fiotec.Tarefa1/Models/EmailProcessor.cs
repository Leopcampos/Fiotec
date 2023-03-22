using Fiotec.Tarefa1.Models;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Data;

namespace Fiotec.Tarefa1
{
    public class EmailProcessor
    {
        private readonly string folderPath;
        private readonly string fileExtension;
        private readonly string connectionString;

        public EmailProcessor(string folderPath, string fileExtension, string connectionString)
        {
            this.folderPath = folderPath;
            this.fileExtension = fileExtension;
            this.connectionString = connectionString;

            VerificarETCriarTabelaEmails();
        }


        public void VerificarEmail()
        {
            int totalLidos = 0;
            int totalSalvos = 0;

            string[] files = Directory.GetFiles(folderPath, fileExtension);

            foreach (string file in files)
            {
                Console.WriteLine(file);
            }

            if (files.Length == 0)
            {
                Console.WriteLine("Nenhum arquivo encontrado na pasta especificada.");
                return;
            }

            foreach (string filePath in Directory.EnumerateFiles(folderPath, fileExtension, SearchOption.AllDirectories))
            {
                string nomeArquivo = Path.GetFileName(filePath);

                // Verifica se o arquivo é um email
                bool isEmail = false;
                string fileContent = File.ReadAllText(filePath, Encoding.GetEncoding("iso-8859-1"));
                if (fileContent.Contains("De:") && fileContent.Contains("Enviado em:") && fileContent.Contains("Para:") && fileContent.Contains("Assunto:"))
                {
                    isEmail = true;
                }

                if (isEmail)
                {
                    // Lê o conteúdo do arquivo usando iso-8859-1 como codificação
                    fileContent = File.ReadAllText(filePath, Encoding.GetEncoding("iso-8859-1"));

                    // Expressão regular para extrair as informações do email
                    Regex regex = new Regex(@"De:\s*(?<De>[^<]+?<[^>]+?>)\s*Enviado em:\s*(?<Enviado>[^<>]+?)\s*Para:\s*(?<Para>[^<>]+?)\s*Assunto:\s*(?<Assunto>[^\r\n]+)\r?\n(?<conteudo>.+)", RegexOptions.Singleline);
                    Match match = regex.Match(fileContent);

                    if (match.Success)
                    {
                        string remetente = match.Groups["De"].Value.Trim();
                        string destinatario = match.Groups["Para"].Value.Trim();
                        string datahoraStr = match.Groups["Enviado"].Value.Trim();
                        string assunto = match.Groups["Assunto"].Value.Trim();
                        string conteudo = match.Groups["conteudo"].Value.Trim();

                        totalLidos++;

                        if (DateTime.TryParse(datahoraStr, out DateTime datahora))
                        {
                            EmailMessage email = new EmailMessage
                            {
                                NomeArquivo = nomeArquivo,
                                Remetente = remetente,
                                Destinatario = destinatario,
                                DataHora = datahora,
                                Assunto = assunto,
                                Conteudo = conteudo
                            };

                            // Insere o email na base de dados
                            using SqlConnection connection = new SqlConnection(connectionString);

                            string query = @"IF NOT EXISTS(SELECT 1 FROM Emails WHERE 
                            Remetente = @Remetente AND 
                            Destinatario = @Destinatario AND 
                            DataHora = @DataHora AND
                            Assunto = @Assunto)

                            INSERT INTO Emails (NomeArquivo, Remetente,
                                    Destinatario, DataHora, Assunto, Conteudo) 
                            VALUES 
                                    (@NomeArquivo, @Remetente, @Destinatario,
                                    @DataHora, @Assunto, CONVERT(NVARCHAR(MAX), @Conteudo, 0));";


                            using SqlCommand command = new SqlCommand(query, connection);

                            connection.Open();
                            command.Parameters.AddWithValue("@NomeArquivo", email.NomeArquivo);
                            command.Parameters.AddWithValue("@Remetente", email.Remetente);
                            command.Parameters.Add("@Destinatario", SqlDbType.NVarChar).Value = email.Destinatario;
                            command.Parameters.AddWithValue("@DataHora", email.DataHora);
                            command.Parameters.AddWithValue("@Assunto", email.Assunto);
                            command.Parameters.AddWithValue("@Conteudo", email.Conteudo);
                            command.ExecuteNonQuery();
                            totalSalvos++;
                            connection.Close();
                        }
                    }
                }
            }

            Console.WriteLine($"Total de emails lidos: {totalLidos}");
            Console.WriteLine($"Total de emails salvos: {totalSalvos}");
        }

        public void VerificarETCriarTabelaEmails()
        {
            using SqlConnection connection = new SqlConnection(connectionString);

            // Cria a tabela se ela não existir
            connection.Open();
            string createTableQuery = @"IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Emails]') AND type in (N'U'))
                                CREATE TABLE Emails 
                                (ID INT IDENTITY(1,1) PRIMARY KEY, NomeArquivo NVARCHAR(255),
                                Remetente NVARCHAR(255), Destinatario NVARCHAR(255), 
                                DataHora DATETIME, Assunto NVARCHAR(255), 
                                Conteudo NVARCHAR(MAX))";

            using SqlCommand createTableCommand = new SqlCommand(createTableQuery, connection);
            createTableCommand.ExecuteNonQuery();
            connection.Close();
        }
    }
}