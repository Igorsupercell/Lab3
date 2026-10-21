using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace Lab3
{
    public abstract class Token
    {
        public string Сontent { get; set; }
        public abstract override string ToString();
        protected Token(string content)
        {
            Сontent = content;
        }

    }
}
