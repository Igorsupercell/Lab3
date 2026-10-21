using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lab3
{
    public class Text 
    {
        public List<Sentence> Sentences { get; set; } = new List<Sentence>();

        public void Add(Sentence sent) { Sentences.Add(sent); }

        public string ToString()
        {
            return string.Join(" ", Sentences.ToArray());
        }
        public void Output()
        {   
        }

        public Text()
        {
            Sentences.Add(new Sentence());
        }
    }
}
