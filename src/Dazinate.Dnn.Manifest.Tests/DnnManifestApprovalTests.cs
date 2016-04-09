using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using ApprovalTests;
using ApprovalTests.Reporters;
using Autofac;
using Dazinate.Dnn.Manifest.Factory;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Model;
using Dazinate.Dnn.Manifest.Model.Component;
using Dazinate.Dnn.Manifest.Model.Manifest;
using Dazinate.Dnn.Manifest.Model.Package;
using Xunit;

namespace Dazinate.Dnn.Manifest.Tests
{
    [UseReporter(typeof(DiffReporter))]
    [Collection("Csla")]
    public class DnnManifestApprovalTests : BaseBusinessTest, IDisposable
    {

        /// <summary>
        /// Constructor is executed prior to every individual test.
        /// </summary>
        public DnnManifestApprovalTests()
        {
            Console.Write("initialising");
        }

        private string LoadManifestXml(string localFileName)
        {
            var dir = System.IO.Directory.GetCurrentDirectory();
            var filePath = Path.Combine(dir, localFileName);
            var xmlContents = File.ReadAllText(filePath);
            return xmlContents;
        }


        [Theory]
        [InlineData("manifest.xml")]
        public void Can_Load_And_Save_Manifest(string manifestFile)
        {
            //  var names = new[] { "Llewellyn", "James", "Dan", "Jason", "Katrina" };
            // Array.Sort(names);
            var xmlContents = LoadManifestXml(manifestFile);
            IDnnManifestFactory<IPackagesDnnManifest> factory = new PackagesDnnManifestFactory();

            // Act           
            var dnnManifest = factory.Get(xmlContents);
            dnnManifest.Packages[0].Description = "changed";
           
            var xmlStringBuilder = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(new StringWriter(xmlStringBuilder)))
            {
                dnnManifest = (IPackagesDnnManifest)dnnManifest.SaveToXml(xmlWriter);
            }
            
            // Now verify the xml looks good.
            Approvals.VerifyXml(xmlStringBuilder.ToString());
        }



    }


}


