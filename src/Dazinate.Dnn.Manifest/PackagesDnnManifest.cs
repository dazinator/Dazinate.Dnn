using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Csla;
using Csla.Core;
using Csla.Server;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.ObjectFactory;
using Dazinate.Dnn.Manifest.Package;

namespace Dazinate.Dnn.Manifest
{
    [ObjectFactory(typeof(IPackagesDnnManifestObjectFactory))]
    [Serializable]
    public class PackagesDnnManifest : BusinessBase<PackagesDnnManifest>, IPackagesDnnManifest
    {


        public static readonly PropertyInfo<ManifestType> TypeProperty = RegisterProperty<ManifestType>(c => c.Type);
        public ManifestType Type
        {
            get { return GetProperty(TypeProperty); }
            set { SetProperty(TypeProperty, value); }
        }

        public static readonly PropertyInfo<string> VersionProperty = RegisterProperty<string>(c => c.Version);
        public string Version
        {
            get { return GetProperty(VersionProperty); }
            set { SetProperty(VersionProperty, value); }
        }

        public static readonly PropertyInfo<PackagesList> PackagesListProperty = RegisterProperty<PackagesList>(c => c.Packages);
        public IPackagesList Packages
        {
            get { return GetProperty(PackagesListProperty); }
            set { SetProperty(PackagesListProperty, value); }
        }

        protected override void AddBusinessRules()
        {

            base.AddBusinessRules();
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(TypeProperty));
            //  BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(VersionProperty));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Lambda(VersionProperty, (c) =>
            {
                PackagesDnnManifest target = (PackagesDnnManifest)c.Target;

                if (string.IsNullOrWhiteSpace(target.Version))
                {
                    c.AddErrorResult("Version number is required.");
                }
                else
                {
                    Version result;
                    if (!System.Version.TryParse(target.Version, out result))
                    {
                        c.AddErrorResult("Invalid version number.");
                    }
                }

                //foreach (var package in this.Packages)
                //{
                //    package.CheckRules();
                //}

            }));

            // should have atleaset 1 package
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Lambda(PackagesListProperty, (c) =>
            {
                PackagesDnnManifest target = (PackagesDnnManifest)c.Target;

                if (!target.Packages.Any())
                {
                    c.AddWarningResult("Manifest should contain atleast 1 package.");
                }
            }));



            //BusinessRules.AddRule(new Csla.Rules.CommonRules.Lambda(VersionProperty, (c) =>
            //{
            //    PackagesDnnManifest target = (PackagesDnnManifest)c.Target;
            //    var packages = target.Packages;
            //    foreach (var package in packages)
            //    {
            //        package.Manifest = this;
            //    }
            //}));
        }

        protected override void PropertyHasChanged(IPropertyInfo property)
        {
            if (property == VersionProperty)
            {
                foreach (var package in this.Packages)
                {
                    package.CheckRules();
                }
            }
            base.PropertyHasChanged(property);
        }
        
        public void Accept(IManifestVisitor visitor)
        {
            visitor.Visit(this);
        }

        public IDnnManifest SaveToXml(XmlWriter writer)
        {
            var result = Csla.DataPortal.Execute(new SaveToXmlCommand(this));
            writer.WriteRaw(result.Xml);
            writer.Flush();
            return result.PackagesDnnManifest;
        }

        protected override Task<PackagesDnnManifest> SaveAsync(bool forceUpdate, object userState, bool isSync)
        {
            throw new NotImplementedException("Please use SaveToXml instead.");
        }

        [ObjectFactory(typeof(IPackagesDnnManifestObjectFactory))]
        [Serializable]
        public class SaveToXmlCommand : Csla.CommandBase<SaveToXmlCommand>
        {


            public SaveToXmlCommand()
            {
                //_manifest = manifest;
            }

            public SaveToXmlCommand(PackagesDnnManifest manifest)
            {
                PackagesDnnManifest = manifest;
            }

            public IPackagesDnnManifest PackagesDnnManifest { get; set; }

            public string Xml { get; set; }
        }

    }






    //[Serializable()]
    //public class SaveToXmlDocumentCommand : CommandBase<SaveToXmlDocumentCommand>
    //{
    //    public SaveToXmlDocumentCommand()
    //    {
    //        // ResourceId = id;
    //    }

    //    public static PropertyInfo<int> ResourceIdProperty = RegisterProperty<int>(c => c.ResourceId);
    //    public int ResourceId
    //    {
    //        get { return ReadProperty(ResourceIdProperty); }
    //        private set { LoadProperty(ResourceIdProperty, value); }
    //    }

    //    public static PropertyInfo<bool> ResourceExistsProperty = RegisterProperty<bool>(c => c.ResourceExists);
    //    public bool ResourceExists
    //    {
    //        get { return ReadProperty(ResourceExistsProperty); }
    //        private set { LoadProperty(ResourceExistsProperty, value); }
    //    }



    //}
}
