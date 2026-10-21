using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3
{
    internal class Word : Token 
    {
        public Word(string content) : base(content){}
       
        public override string ToString() => Сontent;
        
    }
}
