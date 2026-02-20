using System.Collections.Generic;

namespace VisitorChallenge
{
    public class Document
    {
        public string Title { get; set; }
        public List<DocumentElement> Elements { get; set; }

        public Document(string title)
        {
            Title = title;
            Elements = new List<DocumentElement>();
        }

        public void AddElement(DocumentElement element)
        {
            Elements.Add(element);
        }

        public void Accept(IVisitor visitor)
        {
            foreach (var element in Elements)
            {
                element.Accept(visitor);
            }
        }
    }
}
