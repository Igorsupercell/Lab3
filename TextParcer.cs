using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Lab3
{
    internal class TextParcer
    {
        static void Main(string[] args) 
        {
            string OutputFile = "";
            string InputFile = "";
            StringBuilder WordBuilder = new StringBuilder();
            StringBuilder PunctBuilder = new StringBuilder();
            try
            {
                using (StreamReader sr = new StreamReader(InputFile))
                using (StreamWriter sw = new StreamWriter(OutputFile))   
                {
                    string line;
                    while ((line=sr.ReadLine()) != null) 
                    {
                        line = line.Trim();
                        if (string.IsNullOrEmpty(line)) continue;

                        Text text= new Text();
                        int j = 0;
                        for (int i = 0; i < line.Length; i++)
                        {
                            if (Punctuation.Allowed.Contains(line[i].ToString()))
                            {
                                PunctBuilder.Append(line[i].ToString());

                                if (Punctuation.AllowedToendSentence.Contains(line[i].ToString()))
                                {
                                    if (Punctuation.AllowedToendSentence.Contains(line[i + 1].ToString()))
                                    {
                                        PunctBuilder.Append(line[i + 1]);
                                        if (Punctuation.AllowedToendSentence.Contains(line[i + 2].ToString())) PunctBuilder.Append(line[i + 2].ToString());
                                    }
                                    text.Sentences[j].Add(new Word(WordBuilder.ToString()));
                                    text.Sentences[j].Add(new Punctuation(PunctBuilder.ToString()));
                                    WordBuilder.Clear();
                                    PunctBuilder.Clear();
                                    text.Add(new Sentence());
                                    j++;
                                }
                                else
                                {
                                    text.Sentences[j].Add(new Word(WordBuilder.ToString()));
                                    text.Sentences[j].Add(new Punctuation(PunctBuilder.ToString()));
                                    WordBuilder.Clear();
                                    PunctBuilder.Clear(); 
                                }
                            }
                            else
                            {
                                WordBuilder.Append(line[i]);
                            }
                                
                        }

                    }
                }
            }
            catch (Exception ex) {
             Console.WriteLine($" Ошибка при выполнении программы: {ex.Message} " );
            }
        }
    }
}
