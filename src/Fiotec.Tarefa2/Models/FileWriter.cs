using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiotec.Tarefa2.Models
{
    public class FileWriter
    {
        public static void Write(List<string> lines, string outputFile)
        {
            File.WriteAllLines(outputFile, lines, Encoding.UTF8);
        }
    }
}