namespace VisitorChallenge
{
    public abstract class DocumentElement
    {
        public abstract void Render();
        public abstract void Accept(IVisitor visitor);
    }
}
