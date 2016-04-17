namespace Dazinate.Dnn.Manifest.Base
{
    public interface IVisitable<in TVisitor>
    {
        void Accept(TVisitor visitor);

    }
}