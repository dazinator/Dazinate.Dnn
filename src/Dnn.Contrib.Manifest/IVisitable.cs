namespace Dnn.Contrib.Manifest
{
    public interface IVisitable<in TVisitor>
    {
        void Accept(TVisitor visitor);

    }
}