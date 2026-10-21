using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Xml.Serialization;

namespace Lab3
{
    public abstract class Token
    {
        [XmlText]
        public string Content { get; set; }
        protected Token() : this("") { }
        public abstract override string ToString();
        protected Token(string content)
        {
            Content = content;
        }



    }
}
