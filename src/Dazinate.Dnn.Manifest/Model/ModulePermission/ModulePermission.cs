using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Model.ModulePermission.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.ModulePermission
{
    [ObjectFactory(typeof(IModulePermissionObjectFactory))]
    [Serializable]
    public class ModulePermission : BusinessBase<ModulePermission>, IModulePermission
    {

        public static readonly PropertyInfo<string> CodeProperty = RegisterProperty<string>(c => c.Code);
        public string Code
        {
            get { return GetProperty(CodeProperty); }
            set { SetProperty(CodeProperty, value); }
        }

        public static readonly PropertyInfo<string> KeyProperty = RegisterProperty<string>(c => c.Key);
        public string Key
        {
            get { return GetProperty(KeyProperty); }
            set { SetProperty(KeyProperty, value); }
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }


        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}