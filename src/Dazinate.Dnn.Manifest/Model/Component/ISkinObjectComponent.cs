namespace Dazinate.Dnn.Manifest.Model.Component
{
    public interface ISkinObjectComponent : IComponent
    {

        string ControlKey { get; set; }

        string ControlSource { get; set; }
        
        bool SupportsPartialRendering { get; set; }

    }
}