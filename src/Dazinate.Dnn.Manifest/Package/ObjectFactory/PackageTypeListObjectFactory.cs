﻿using System;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Ioc;

namespace Dazinate.Dnn.Manifest.Package.ObjectFactory
{
    public class PackageTypeListObjectFactory : BaseObjectFactory, IPackageTypeListObjectFactory
    {
        public PackageTypeListObjectFactory(IObjectActivator activator) : base(activator)
        {
        }
       
        public PackageTypeList Fetch()
        {
            var list = CreateInstance<PackageTypeList>();
            SetIsReadOnly(list, false);

            foreach (PackageType item in Enum.GetValues(typeof(PackageType)))
            {
                var listItem = new Csla.NameValueListBase<string, string>.NameValuePair(item.ToString(),
                    Enum.GetName(typeof(PackageType), item));
                list.Add(listItem);
            }

            // This is where new package types could be added in, in the future.
            SetIsReadOnly(list, true);
            return list;
        }


    }
}