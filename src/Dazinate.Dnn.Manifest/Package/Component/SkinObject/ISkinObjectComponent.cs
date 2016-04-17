namespace Dazinate.Dnn.Manifest.Package.Component.SkinObject
{
    public interface ISkinObjectComponent : IComponent
    {

        string ControlKey { get; set; }

        string ControlSource { get; set; }
        
        bool SupportsPartialRendering { get; set; }

    }
}