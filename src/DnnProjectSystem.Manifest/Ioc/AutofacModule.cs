using Autofac;
using Dnn.Contrib.Manifest.Factory;
using Dnn.Contrib.Manifest.ObjectFactory;

namespace Dnn.Contrib.Manifest.Ioc
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AutofacObjectActivator>().As<IObjectActivator>();

            // register all the object factories, and business objects here.           
            builder.RegisterType<PackagesDnnManifestObjectFactory>().As<IPackagesDnnManifestObjectFactory>();
            builder.RegisterType<PackagesDnnManifest>().As<IDnnManifest>();
            builder.RegisterType<PackagesDnnManifestFactory>().As<IDnnManifestFactory<IPackagesDnnManifest>>()
                                                              .As<IDnnManifestFactory<IDnnManifest>>();
           
            builder.RegisterInstance(new PackageTypeListFactory()).As<IPackageTypeListFactory>();
            builder.RegisterType<PackageTypeListObjectFactory>().As<IPackageTypeListObjectFactory>();

            builder.RegisterType<PackagesListObjectFactory>().As<IPackagesListObjectFactory>();

            builder.RegisterType<PackageFactory>().As<IPackageFactory>();
            builder.RegisterType<PackageObjectFactory>().As<IPackageObjectFactory>();


           


        }


    }
}
