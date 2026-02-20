using System;

namespace VisitorChallenge
{
    public class Image : DocumentElement
    {
        public string Url { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Alt { get; set; }

        public Image(string url, int width, int height)
        {
            Url = url;
            Width = width;
            Height = height;
            Alt = "";
        }

        public override void Render()
        {
            Console.WriteLine($"[Imagem] {Url} ({Width}x{Height})");
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
