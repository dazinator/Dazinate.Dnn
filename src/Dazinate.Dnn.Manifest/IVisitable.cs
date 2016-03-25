namespace Dazinate.Dnn.Manifest
{
    public interface IVisitable<in TVisitor>
    {
        void Accept(TVisitor visitor);

    }
}