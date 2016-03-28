namespace Dazinate.Dnn.Manifest.Ioc
{
    public interface IObjectActivator
    {
        T Activate<T>() where T : class;
    }
}