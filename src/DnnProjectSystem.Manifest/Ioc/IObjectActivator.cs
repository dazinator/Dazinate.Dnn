namespace Dnn.Contrib.Manifest.Ioc
{
    public interface IObjectActivator
    {
        T Activate<T>();
    }
}