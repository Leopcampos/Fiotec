Tarefa 1

Antes de executar o projeto, é necessário criar o banco de dados e passar a string de conexão para dentro do arquivo App.config

Estou usando o localDb(SQL Server) do Visual Studio

<?xml version="1.0" encoding="utf-8"?>
<configuration>
	<connectionStrings>
		<add name="Emails" connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BD_FioTec;Integrated Security=True;Connect Timeout=30" />
	</connectionStrings>
</configuration>

Foi criada a pasta C:\Mensagens onde estão armazenados todos os emails para verificação e após verificação, estão sendo salvos no banco de dados conforme solicitado.

Tarefa 2

Usei a pasta C:\Mensagens como referência e fiz uma verificação com o arquivo palavras.txt(dicionário), em seguida salvez o arquivo Categoria.txt na pasta tempo ou uma outra pasta de sua escolha.

OBS: no arquivo Categoria.txt contém as palavras - categoria a qual ela pertence.
Exemplo: 
João - Nome Próprio
Se - Dicionário
www = Outros
