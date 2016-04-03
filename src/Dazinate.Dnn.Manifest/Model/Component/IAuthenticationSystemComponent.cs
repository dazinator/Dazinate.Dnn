namespace Dazinate.Dnn.Manifest.Model.Component
{
    public interface IAuthenticationSystemComponent : IComponent
    {

        string Type { get; set; }

        string SettingsControlSource { get; set; }

        string LoginControlSource { get; set; }

        string LogoffControlSource { get; set; }

    }
}