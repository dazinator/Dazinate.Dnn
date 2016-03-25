using System.IO;
using System.Xml;
using System.Xml.XPath;
using Csla;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Wip;

namespace Dazinate.Dnn.Manifest.ObjectFactory
{
    public class PackagesDnnManifestObjectFactory : BaseObjectFactory, IPackagesDnnManifestObjectFactory
    {

        private readonly IPackagesListObjectFactory _packagesListFactory;

        public PackagesDnnManifestObjectFactory(IObjectActivator activator, IPackagesListObjectFactory packagesListFactory) : base(activator)
        {
            _packagesListFactory = packagesListFactory;
        }

        public PackagesDnnManifest Fetch(SingleCriteria<string> xmlContents)
        {
            string xml = xmlContents.Value;

            using (var manifestFileStream = new XmlTextReader(new StringReader(xml)))
            {
                var instance = Load(manifestFileStream);
                MarkOld(instance);
                CheckRules(instance);
                return instance;
            }
        }

        private PackagesDnnManifest Load(XmlTextReader manifestFileStream)
        {

            var dnnManifest = CreateInstance<PackagesDnnManifest>();

            var doc = new XPathDocument(manifestFileStream);

            //Read the root node to determine what version the manifest is
            XPathNavigator rootNav = doc.CreateNavigator();
            rootNav.MoveToFirstChild();

            if (rootNav.Name.ToLower() == "dotnetnuke")
            {
                LoadDotNetNukeManifest(rootNav, dnnManifest);
                //packageType = ReadAttribute(rootNav, "type");
            }
            //else if (rootNav.Name.ToLower() == "languagepack")
            //{
            //    throw new NotSupportedException("This manifest is not in a supported format.");
            //    //LoadLanguagePackManifest(rootNav, dnnManifest);
            //}
            else
            {
                throw new InvalidManifestFormatException();
            }

            return dnnManifest;

        }

        private void LoadDotNetNukeManifest(XPathNavigator rootNav, PackagesDnnManifest dnnPackagesManifest)
        {

            var packageTypeString = XmlUtils.ReadAttribute(rootNav, "type");

            switch (packageTypeString.ToLower())
            {
                case "package":
                    //  InstallerInfo.IsLegacyMode = false;
                    //Parse the package nodes
                    LoadPackageManifest(rootNav, dnnPackagesManifest);
                    break;
                //case "module":
                //case "languagepack":
                //case "skinobject":
                //    rootNav = ConvertLegacyNavigator(rootNav);
                //    LoadPackageManifest(rootNav, dnnManifest);
                //  InstallerInfo.IsLegacyMode = true;
                //break;
                default:
                    throw new InvalidManifestFormatException();
            }

            //throw new NotImplementedException();
        }

        private void LoadPackageManifest(XPathNavigator rootNav, PackagesDnnManifest dnnPackagesManifest)
        {

            LoadProperty(dnnPackagesManifest, PackagesDnnManifest.TypeProperty, ManifestType.Package);
            var version = XmlUtils.ReadRequiredAttribute(rootNav, "version");
            LoadProperty(dnnPackagesManifest, PackagesDnnManifest.VersionProperty, version);

           

            var packagesList = _packagesListFactory.Fetch(rootNav);
           
            
            LoadProperty(dnnPackagesManifest, PackagesDnnManifest.PackagesListProperty, packagesList);
            //   throw new NotImplementedException();
        }

        public PackagesDnnManifest Update(PackagesDnnManifest businessObject)
        {

            // nothing really to do
            foreach (var i in businessObject.Packages)
            {
                MarkOld(i);
                MarkOld(i.License);
                MarkOld(i.Owner);
                if (i.ReleaseNotes != null)
                {
                    MarkOld(i.ReleaseNotes);
                }
            }

            businessObject.Packages.GetDeletedList();
            // var deletedList = businessObject.Packages.GetDeletedList();
            var deletedList = GetDeletedList<IPackage>(businessObject.Packages);
            deletedList.Clear();

            MarkOld(businessObject.Packages);
            MarkOld(businessObject);
            return businessObject;

        }

     


    }
}


//public static XPathNavigator ConvertLegacyNavigator(XPathNavigator rootNav)
//{
//    XPathNavigator nav = null;

//    var packageType = "";
//    if (rootNav.Name.ToLowerInvariant() == "dotnetnuke")
//    {
//        packageType = XmlUtils.ReadAttribute(rootNav, "type");
//    }
//    else if (rootNav.Name.ToLowerInvariant() == "languagepack")
//    {
//        packageType = "LanguagePack";
//    }

//    XPathDocument legacyDoc;
//    string legacyManifest;
//    switch (packageType.ToLower())
//    {
//        case "module":
//            var sb = new StringBuilder();
//            var writer = XmlWriter.Create(sb, new XmlWriterSettings() { ConformanceLevel = ConformanceLevel.Fragment });

//            //Write manifest start element
//            WriteManifestStartElement(writer);

//            //Legacy Module - Process each folder
//            foreach (XPathNavigator folderNav in rootNav.Select("folders/folder"))
//            {
//                ReadLegacyManifest(folderNav, true);
//                WriteManifest(writer, true);
//            }

//            //Write manifest end element
//            WriteManifestEndElement(writer);

//            //Close XmlWriter
//            writer.Close();

//            //Load manifest into XPathDocument for processing
//            legacyDoc = new XPathDocument(new StringReader(sb.ToString()));

//            // reset the navigator back to the dotnetnuke node.
//            nav = legacyDoc.CreateNavigator().SelectSingleNode("dotnetnuke");
//            break;
//        case "languagepack":
//            //Legacy Language Pack
//            var languageWriter = new LanguagePackWriter(rootNav, info);
//            info.LegacyError = languageWriter.LegacyError;
//            if (string.IsNullOrEmpty(info.LegacyError))
//            {
//                legacyManifest = languageWriter.WriteManifest(false);
//                legacyDoc = new XPathDocument(new StringReader(legacyManifest));

//                //Parse the package nodes
//                nav = legacyDoc.CreateNavigator().SelectSingleNode("dotnetnuke");
//            }
//            break;
//        case "skinobject":
//            //Legacy Skin Object
//            var skinControlwriter = new SkinControlPackageWriter(rootNav, info);
//            legacyManifest = skinControlwriter.WriteManifest(false);
//            legacyDoc = new XPathDocument(new StringReader(legacyManifest));

//            //Parse the package nodes
//            nav = legacyDoc.CreateNavigator().SelectSingleNode("dotnetnuke");
//            break;
//    }

//    return nav;
//}

//public void WriteManifest(XmlWriter writer, bool packageFragment)
//{
//    // Log.StartJob(Util.WRITER_CreatingManifest);

//    if (!packageFragment)
//    {
//        //Start dotnetnuke element
//        WriteManifestStartElement(writer);
//    }

//    //Start package Element
//    WritePackageStartElement(writer);

//    //write Script Component
//    if (Scripts.Count > 0)
//    {
//        var scriptWriter = new ScriptComponentWriter(BasePath, Scripts, Package);
//        scriptWriter.WriteManifest(writer);
//    }

//    //write Clean Up Files Component
//    if (CleanUpFiles.Count > 0)
//    {
//        var cleanupFileWriter = new CleanupComponentWriter(BasePath, CleanUpFiles);
//        cleanupFileWriter.WriteManifest(writer);
//    }

//    //Write the Custom Component
//    WriteManifestComponent(writer);

//    //Write Assemblies Component
//    if (Assemblies.Count > 0)
//    {
//        var assemblyWriter = new AssemblyComponentWriter(AssemblyPath, Assemblies, Package);
//        assemblyWriter.WriteManifest(writer);
//    }

//    //Write AppCode Files Component
//    if (AppCodeFiles.Count > 0)
//    {
//        var fileWriter = new FileComponentWriter(AppCodePath, AppCodeFiles, Package);
//        fileWriter.WriteManifest(writer);
//    }

//    //write Files Component
//    if (Files.Count > 0)
//    {
//        WriteFilesToManifest(writer);
//    }

//    //write ResourceFiles Component
//    if (Resources.Count > 0)
//    {
//        var fileWriter = new ResourceFileComponentWriter(BasePath, Resources, Package);
//        fileWriter.WriteManifest(writer);
//    }

//    //Close Package
//    WritePackageEndElement(writer);

//    if (!packageFragment)
//    {
//        //Close Dotnetnuke Element
//        WriteManifestEndElement(writer);
//    }
//    Log.EndJob(Util.WRITER_CreatedManifest);
//}

//private void ReadLegacyManifest(XPathNavigator folderNav, bool processModule)
//{
//    if (processModule)
//    {
//        //Version 2 of legacy manifest
//        string name = XmlUtils.ReadElement(folderNav, "name");
//        DesktopModule.FolderName = name;
//        DesktopModule.ModuleName = name;
//        DesktopModule.FriendlyName = name;
//        string folderName = Util.ReadElement(folderNav, "foldername");
//        if (!string.IsNullOrEmpty(folderName))
//        {
//            DesktopModule.FolderName = folderName;
//        }
//        if (string.IsNullOrEmpty(DesktopModule.FolderName))
//        {
//            DesktopModule.FolderName = "MyModule";
//        }
//        string friendlyname = Util.ReadElement(folderNav, "friendlyname");
//        if (!string.IsNullOrEmpty(friendlyname))
//        {
//            DesktopModule.FriendlyName = friendlyname;
//            DesktopModule.ModuleName = friendlyname;
//        }
//        string iconFile = Util.ReadElement(folderNav, "iconfile");
//        if (!string.IsNullOrEmpty(iconFile))
//        {
//            Package.IconFile = iconFile;
//        }
//        string modulename = Util.ReadElement(folderNav, "modulename");
//        if (!string.IsNullOrEmpty(modulename))
//        {
//            DesktopModule.ModuleName = modulename;
//        }
//        string permissions = Util.ReadElement(folderNav, "permissions");
//        if (!string.IsNullOrEmpty(permissions))
//        {
//            DesktopModule.Permissions = permissions;
//        }
//        string dependencies = Util.ReadElement(folderNav, "dependencies");
//        if (!string.IsNullOrEmpty(dependencies))
//        {
//            DesktopModule.Dependencies = dependencies;
//        }
//        DesktopModule.Version = Util.ReadElement(folderNav, "version", "01.00.00");
//        DesktopModule.Description = Util.ReadElement(folderNav, "description");
//        DesktopModule.BusinessControllerClass = Util.ReadElement(folderNav, "businesscontrollerclass");

//        //Process legacy modules Node
//        foreach (XPathNavigator moduleNav in folderNav.Select("modules/module"))
//        {
//            ProcessModules(moduleNav, DesktopModule.FolderName);
//        }
//    }

//}

//private void ProcessModules(XPathNavigator moduleNav, string moduleFolder)
//{
//    var definition = new ModuleDefinitionInfo();

//    definition.FriendlyName = Util.ReadElement(moduleNav, "friendlyname");
//    string cacheTime = Util.ReadElement(moduleNav, "cachetime");
//    if (!string.IsNullOrEmpty(cacheTime))
//    {
//        definition.DefaultCacheTime = int.Parse(cacheTime);
//    }

//    //Process legacy controls Node
//    foreach (XPathNavigator controlNav in moduleNav.Select("controls/control"))
//    {
//        ProcessControls(controlNav, moduleFolder, definition);
//    }
//    DesktopModule.ModuleDefinitions[definition.FriendlyName] = definition;
//}

//public static void WriteManifestEndElement(XmlWriter writer)
//{
//    //Close packages Element
//    writer.WriteEndElement();

//    //Close root Element
//    writer.WriteEndElement();
//}

//public static void WriteManifestStartElement(XmlWriter writer)
//{
//    //Start the new Root Element
//    writer.WriteStartElement("dotnetnuke");
//    writer.WriteAttributeString("type", "Package");
//    writer.WriteAttributeString("version", "5.0");

//    //Start packages Element
//    writer.WriteStartElement("packages");
//}