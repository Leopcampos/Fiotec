using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiotec.Tarefa2.Models
{
    public class WordFrequencyCalculator
    {
        public static Dictionary<string, int> Calculate(List<string> emails)
        {
            Dictionary<string, int> wordFrequency = new Dictionary<string, int>();

            foreach (string email in emails)
            {
                string[] words = email.Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string word in words)
                {
                    string cleanedWord = word.ToLower().Replace("-", "");

                    if (wordFrequency.ContainsKey(cleanedWord))
                    {
                        wordFrequency[cleanedWord]++;
                    }
                    else
                    {
                        wordFrequency.Add(cleanedWord, 1);
                    }
                }
            }
            return wordFrequency;
        }
    }
}