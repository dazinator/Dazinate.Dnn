using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Package.Component.JavascriptFile.ObjectFactory;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.JavascriptFile
{
    [ObjectFactory(typeof(IJavascriptFilesListObjectFactory))]
    [Serializable]
    public class JavascriptFilesList : BusinessListBase<JavascriptFilesList, IJavascriptFile>, IJavascriptFilesList
    {
        public JavascriptFilesList()
        {
        }

        public void Accept(IManifestXmlWriterVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}