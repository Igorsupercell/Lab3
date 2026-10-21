using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Lab3
{
    public class Sentence : Token
    {
        
        [XmlIgnore]
        public List<Token> Items { get; set; } = new List<Token>();
        public Sentence(string content) : base(content){}
        public Sentence() : base("") { }

        [XmlElement("word")]
        public List<string> WordsForXml
        {
            get => Items.OfType<Word>().Select(w => w.Content).ToList();
            set
            {
                if (value != null)
                {
                    Items = value.Select(w => (Token)new Word(w)).ToList();
                }
            }
        }

        public void Add(Token token)
        {
            Items.Add(token);
        }
        public override string ToString()
        {
            
            StringBuilder Result=new StringBuilder();
            Token[] Sent=Items.ToArray();
            for(int i=0; i<Sent.Length;i++)
                if (Sent[i] is Word && i==0) 
                {
                    Result.Append($" {Sent[i]}");
                }
                else
                {
                    Result.Append(Sent[i]);
                }
                return Result.ToString();
        }

        public int WordCount => Items.OfType<Word>().Count();

        public int Length => ToString().Length;

        public bool IsQuestion => Items.OfType<Punctuation>().Any(p => p.Content.Contains("?"));

    }
}
