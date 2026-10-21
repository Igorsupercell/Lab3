using System;
using System.IO;

namespace Lab3;

class Program
{
    static void Main(string[] args)
    {
        string OutputFile = "";
        string InputFile = "";
        Text userText;
        try
        {
            using (StreamReader sr = new StreamReader(InputFile))
            using (StreamWriter sw = new StreamWriter(OutputFile))
            {
                userText = TextParcer.Parse(sr);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при выполнении программы: {ex.Message}");
        }
    }
}