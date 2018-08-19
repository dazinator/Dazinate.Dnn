using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Factory;
using Dazinate.Dnn.Manifest.Package.ObjectFactory;
using Csla.Core;

namespace Dazinate.Dnn.Manifest.Package
{


    [ObjectFactory(typeof(IPackagesListObjectFactory))]
    [Serializable]
    public class PackagesList : BusinessListBase<PackagesList, IPackage>, IPackagesList
    {
        private readonly IPackageFactory _factory;

        public PackagesList() : this(new PackageFactory())
        {
        }

        internal PackagesList(IPackageFactory factory)
        {
            _factory = factory;
        }


#if !AddNewCoreReturnVoid
        protected override IPackage AddNewCore()
        {
            //base.AddNewCore();
            var child = _factory.CreateNew();
            Add(child);
            return child;
        }
#else
         protected override void AddNewCore()
        {
            //base.AddNewCore();
            var child = _factory.CreateNew();
            Add(child);           
        }
#endif

        public void Accept(IManifestVisitor visitor)
        {
            visitor.Visit(this);
        }

        public IPackage AddNewPackage()
        {
            this.AddNewCore();
            //var result = this.AddNew();
            return (IPackage)this.Items[this.Items.Count -1];
        }
    }
}

//public static class AddNewCoreHelper
//{

//    public static TItem AddNewCore<TList, TItem>(TList list)
//        where TList : BusinessListBase<TList, TItem>
//         where TItem : IEditableBusinessObject
//    {
//        var item = Csla.DataPortal.Create<TItem>();
//        list.Add(item);
//        return item;
//    }

//}