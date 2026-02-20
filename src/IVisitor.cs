namespace VisitorChallenge
{
    public interface IVisitor
    {
        void Visit(Paragraph paragraph);
        void Visit(Image image);
        void Visit(Table table);
    }
}
