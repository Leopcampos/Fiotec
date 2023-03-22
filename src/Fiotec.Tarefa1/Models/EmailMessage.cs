using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiotec.Tarefa1.Models
{
    public class EmailMessage
    {
        public int Id { get; set; }
        public string NomeArquivo { get; set; }
        public string Remetente { get; set; }
        public string Destinatario { get; set; }
        public DateTime DataHora { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }
    }
}