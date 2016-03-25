using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Factory;
using Dazinate.Dnn.Manifest.ObjectFactory;

namespace Dazinate.Dnn.Manifest
{
    [ObjectFactory(typeof(IPackageObjectFactory))]
    [Serializable]
    public class Package : BusinessBase<Package>, IPackage
    {

        [NonSerialized]
        private readonly IPackageTypeListFactory _packageTypeListFactory;

        public Package() : this(new PackageTypeListFactory())
        {
        }

        public Package(IPackageTypeListFactory packageTypeListFactory)
        {
            _packageTypeListFactory = packageTypeListFactory;
        }

        //public static readonly PropertyInfo<IDnnManifest> ManifestProperty = RegisterProperty<IDnnManifest>(c => c.Manifest);
        //public IDnnManifest Manifest
        //{
        //    get { return GetProperty(ManifestProperty); }
        //    set { SetProperty(ManifestProperty, value); }
        //}

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }

        public static readonly PropertyInfo<string> TypeProperty = RegisterProperty<string>(c => c.Type);
        public string Type
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

        public static readonly PropertyInfo<string> FriendlyNameProperty = RegisterProperty<string>(c => c.FriendlyName);
        public string FriendlyName
        {
            get { return GetProperty(FriendlyNameProperty); }
            set { SetProperty(FriendlyNameProperty, value); }
        }

        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(c => c.Description);
        public string Description
        {
            get { return GetProperty(DescriptionProperty); }
            set { SetProperty(DescriptionProperty, value); }
        }

        public static readonly PropertyInfo<string> IconFileProperty = RegisterProperty<string>(c => c.IconFile);
        public string IconFile
        {
            get { return GetProperty(IconFileProperty); }
            set { SetProperty(IconFileProperty, value); }
        }

        public static readonly PropertyInfo<bool?> AzureCompatibleProperty = RegisterProperty<bool?>(c => c.AzureCompatible);
        public bool? AzureCompatible
        {
            get { return GetProperty(AzureCompatibleProperty); }
            set { SetProperty(AzureCompatibleProperty, value); }
        }

        public static readonly PropertyInfo<Owner> OwnerProperty = RegisterProperty<Owner>(c => c.Owner);
        public IOwner Owner
        {
            get { return GetProperty(OwnerProperty); }
            set { SetProperty(OwnerProperty, value); }
        }

        public static readonly PropertyInfo<License> LicenseProperty = RegisterProperty<License>(c => c.License);
        public ILicense License
        {
            get { return GetProperty(LicenseProperty); }
            set { SetProperty(LicenseProperty, value); }
        }

        public static readonly PropertyInfo<ReleaseNotes> ReleaseNotesProperty = RegisterProperty<ReleaseNotes>(c => c.ReleaseNotes);
        public IReleaseNotes ReleaseNotes
        {
            get { return GetProperty(ReleaseNotesProperty); }
            set { SetProperty(ReleaseNotesProperty, value); }
        }
        
        protected override void AddBusinessRules()
        {

            base.AddBusinessRules();

            //  BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(ManifestProperty));

            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(NameProperty));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(TypeProperty));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(VersionProperty));

            BusinessRules.AddRule(new Csla.Rules.CommonRules.Lambda(VersionProperty, (c) =>
            {
                Package target = (Package)c.Target;
                Version result;
                if (!System.Version.TryParse(target.Version, out result))
                {
                    c.AddErrorResult("Invalid version number.");
                }
            }));

            BusinessRules.AddRule(new Csla.Rules.CommonRules.Lambda(TypeProperty, (c) =>
            {
                Package target = (Package)c.Target;
                var type = target.Type;
                var packageTypesList = _packageTypeListFactory.Get();
                if (!packageTypesList.ContainsKey(type))
                {
                    c.AddWarningResult("Unrecognised package type, are you sure this is correct?");
                }
            }));

            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(FriendlyNameProperty));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.MaxLength(FriendlyNameProperty, 250));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.MaxLength(DescriptionProperty, 2000));

            //  BusinessRules.AddRule(new Csla.Rules.CommonRules.Dependency(IconFileProperty, ManifestProperty));

            BusinessRules.AddRule(new Csla.Rules.CommonRules.Lambda(IconFileProperty, (c) =>
            {
                // Icon file is invalid for a pre version 5 manifest.
                Package target = (Package)c.Target;
                var iconFile = target.IconFile;

                if (!string.IsNullOrWhiteSpace(iconFile))
                {
                    if (target.Parent != null)
                    {
                        var parentManifest = target.Parent.Parent as IDnnManifest;
                        if (parentManifest != null)
                        {
                            var version = System.Version.Parse(parentManifest.Version);
                            if (version < new Version(5, 0))
                            {
                                c.AddErrorResult("IconFile is not valid for manifest's below version 5.0 format.");
                            }
                        }
                    }
                   

                    var iconExtension = System.IO.Path.GetExtension(iconFile);

                    if (iconExtension.ToLowerInvariant() != ".png")
                    {
                        c.AddInformationResult("The .png format is the preferred format for icons.");
                    }

                }


            }));


            BusinessRules.AddRule(new Csla.Rules.CommonRules.Lambda(AzureCompatibleProperty, (c) =>
            {
                // Icon file is invalid for a pre version 5 manifest.
                Package target = (Package)c.Target;
                var azureCompat = target.AzureCompatible;

                if (azureCompat.HasValue)
                {
                    var parentManifest = target.Parent.Parent as IDnnManifest;
                    if (parentManifest != null)
                    {
                        var version = System.Version.Parse(parentManifest.Version);
                        if (version < new Version(5, 0))
                        {
                            c.AddErrorResult("Azure Compatiblity flag is not valid for manifest's below version 5.0 format.");
                        }
                    }


                }
            }));


        }

    }
}