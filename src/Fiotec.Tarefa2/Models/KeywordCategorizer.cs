using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiotec.Tarefa2.Models
{
    public class KeywordCategorizer
    {
        public static List<string> Categorize(Dictionary<string, int> wordFrequency)
        {
            List<string> emails = new List<string>();
            int numEmails = emails.Count;
            List<string> keywords = new List<string>();

            foreach (string word in wordFrequency.Keys)
            {
                if (wordFrequency[word] >= numEmails * 0.05)
                {
                    keywords.Add(word);
                }
            }

            string dictionaryFilePath = @"C:\Palavras.txt";
            HashSet<string> dictionaryWords = new HashSet<string>(File.ReadAllLines(dictionaryFilePath));
            List<string> categorizedKeywords = new List<string>();

            foreach (string keyword in keywords)
            {
                if (dictionaryWords.Contains(keyword))
                {
                    categorizedKeywords.Add($"{keyword}: Dicionário");
                }
                else if (!string.IsNullOrEmpty(keyword) &&
                         keyword.Any(char.IsUpper) &&
                         keyword.All(c => char.IsUpper(c) || char.IsLower(c) || c == '-'))
                {
                    categorizedKeywords.Add($"{keyword}: Nome Próprio");
                }
                else
                {
                    categorizedKeywords.Add($"{keyword}: Outros");
                }
            }

            return categorizedKeywords;
        }
    }
}