using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Package.Component.JavascriptFile.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Package.Component.JavascriptFile
{
    [ObjectFactory(typeof(IJavascriptFilesListObjectFactory))]
    [Serializable]
    public class JavascriptFilesList : BusinessListBase<JavascriptFilesList, IJavascriptFile>, IJavascriptFilesList
    {
        public JavascriptFilesList()
        {
        }

        public void Accept(IManifestVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override IJavascriptFile AddNewCore()
        {
            var item = Csla.DataPortal.Create<JavascriptFile>();
            this.Add(item);
            return item;
        }

    }
}