using System.Linq;
using System.Text;

namespace Lab3
{
    public class Sentence : Token
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
            
            StringBuilder Result=new StringBuilder();
            Token[] Sent=Items.ToArray();
            for(int i=0; i<Sent.Length;i++)
                if (Sent[i] is Word && i<0) 
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
