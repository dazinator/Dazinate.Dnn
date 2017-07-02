using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Package.Component.Script.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Package.Component.Script
{
    [ObjectFactory(typeof(IScriptsListObjectFactory))]
    [Serializable]
    public class ScriptsList : BusinessListBase<ScriptsList, IScript>, IScriptsList
    {
        // private readonly IPackageFactory _factory;

        public ScriptsList()
        {
        }

        public void Accept(IManifestVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override IScript AddNewCore()
        {
            var item = Csla.DataPortal.Create<Script>();
            this.Add(item);
            return item;
        }

    }
}