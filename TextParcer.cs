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
                    Text text= new Text(); 
                    int j = 0;
                    while ((line=sr.ReadLine()) != null) 
                    {
                        line = line.Trim();
                        if (string.IsNullOrEmpty(line)) continue;

                        for (int i = 0; i < line.Length; i++)
                        {
                            char CurrentChar = line[i];
                            if (char.IsWhiteSpace(CurrentChar))
                            {
                                if (WordBuilder.Length>0)
                                {   
                                    text.Sentences[j].Add(new Word(WordBuilder.ToString()));
                                    WordBuilder.Clear();
                                }
                                continue;
                            }


                            if (Punctuation.Allowed.Contains(CurrentChar.ToString()))
                            {
                                if (WordBuilder.Length > 0)
                                {
                                    text.Sentences[j].Add(new Word(WordBuilder.ToString()));
                                    WordBuilder.Clear();
                                }
                                PunctBuilder.Append(CurrentChar.ToString());

                                if (Punctuation.AllowedToendSentence.Contains(CurrentChar.ToString()))
                                {
                                    if (i+1<line.Length && Punctuation.AllowedToendSentence.Contains(line[i + 1].ToString()))
                                    {
                                        PunctBuilder.Append(line[i + 1]);
                                        i++;
                                        if (i + 2 < line.Length && Punctuation.AllowedToendSentence.Contains(line[i + 2].ToString()))
                                        { 
                                            PunctBuilder.Append(line[i + 2].ToString());
                                            i++;
                                        }
                                    }
                                    text.Sentences[j].Add(new Punctuation(PunctBuilder.ToString()));
                                    PunctBuilder.Clear();
                                    text.Add(new Sentence());
                                    j++;
                                }
                                else
                                { 
                                    text.Sentences[j].Add(new Punctuation(PunctBuilder.ToString()));
                                    PunctBuilder.Clear(); 
                                }
                            }
                            else
                            {
                                WordBuilder.Append(line[i]);
                            }


                            if (WordBuilder.Length > 0)
                            {
                                text.Sentences[j].Add(new Word(WordBuilder.ToString()));
                                WordBuilder.Clear();
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
