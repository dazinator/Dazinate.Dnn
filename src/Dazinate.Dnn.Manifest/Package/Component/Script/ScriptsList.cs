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

#if NETDESKTOP
        protected override IScript AddNewCore()
        {
            //base.AddNewCore();
            var item = Csla.DataPortal.Create<Script>();
            Add(item);
            return item;
        }
#else
        protected override void AddNewCore()
        {
            //base.AddNewCore();
             var item = Csla.DataPortal.Create<Script>();
            Add(item);           
        }
#endif

    }
}