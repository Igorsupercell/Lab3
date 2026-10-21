using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3
{
    internal class Sentence : Token
    {
        public List<Token> Items { get; set; } = new List<Token>();
        public Sentence(string content) : base(content){}
        public Sentence() : base("") { }

        public void Add(Token token)
        {
            Items.Add(token);
        }
        public override string ToString()
        {
            string Result="";
            Token[] Sent=Items.ToArray();
            for(int i=0; i<Sent.Length;i++)
                if (Sent[i] is Word) 
                {
                    Result += ($"{Sent[i]} ");
                }
                else
                {
                    Result += (Sent[i]);
                }
                return Result;
        }
    }
}
