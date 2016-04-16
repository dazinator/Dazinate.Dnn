using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Model.Component.ObjectFactory;
using Dazinate.Dnn.Manifest.Model.Package;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.Component
{
    [ObjectFactory(typeof(IComponentObjectFactory))]
    [Serializable]
    public class SkinObjectComponent : BusinessBase<SkinObjectComponent>, ISkinObjectComponent
    {

        public static readonly PropertyInfo<string> ControlKeyProperty = RegisterProperty<string>(c => c.ControlKey);
        /// <summary>
        /// This is the key to how you reference the skin object when building a skin. e.g. [COPYRIGHT]
        /// </summary>
        public string ControlKey
        {
            get { return GetProperty(ControlKeyProperty); }
            set { SetProperty(ControlKeyProperty, value); }
        }

        public static readonly PropertyInfo<string> ControlSourceProperty = RegisterProperty<string>(c => c.ControlSource);
        /// <summary>
        /// This is the path to install the skin object's ascx file.
        /// </summary>
        public string ControlSource
        {
            get { return GetProperty(ControlSourceProperty); }
            set { SetProperty(ControlSourceProperty, value); }
        }

        
        public static readonly PropertyInfo<bool> SupportsPartialRenderingProperty = RegisterProperty<bool>(c => c.SupportsPartialRendering);
        /// <summary>
        /// If your skin object supports partial rendering via a MS AJAX update panel wrapper.
        /// </summary>
        public bool SupportsPartialRendering
        {
            get { return GetProperty(SupportsPartialRenderingProperty); }
            set { SetProperty(SupportsPartialRenderingProperty, value); }
        }

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool IsCompatibleWithPackage(IPackage package)
        {
            return package.Type.ToLowerInvariant() == "skinobject";
        }
    }
}