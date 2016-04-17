using System;
using Csla;
using Csla.Server;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Package.Component.Config.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Package.Component.Config
{
    [ObjectFactory(typeof(INodeObjectFactory))]
    [Serializable]
    public class Node : BusinessBase<Node>, INode
    {

        public static readonly PropertyInfo<string> PathProperty = RegisterProperty<string>(c => c.Path);
        public string Path
        {
            get { return GetProperty(PathProperty); }
            set { SetProperty(PathProperty, value); }
        }

        public static readonly PropertyInfo<NodeAction?> ActionProperty = RegisterProperty<NodeAction?>(c => c.Action);
        public NodeAction? Action
        {
            get { return GetProperty(ActionProperty); }
            set { SetProperty(ActionProperty, value); }
        }


        public static readonly PropertyInfo<string> TargetPathProperty = RegisterProperty<string>(c => c.TargetPath);
        public string TargetPath
        {
            get { return GetProperty(TargetPathProperty); }
            set { SetProperty(TargetPathProperty, value); }
        }

        public static readonly PropertyInfo<string> KeyProperty = RegisterProperty<string>(c => c.Key);
        public string Key
        {
            get { return GetProperty(KeyProperty); }
            set { SetProperty(KeyProperty, value); }
        }

        public static readonly PropertyInfo<NodeCollision?> CollisionProperty = RegisterProperty<NodeCollision?>(c => c.Collision);
        public NodeCollision? Collision
        {
            get { return GetProperty(CollisionProperty); }
            set { SetProperty(CollisionProperty, value); }
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }

        public static readonly PropertyInfo<string> ValueProperty = RegisterProperty<string>(c => c.Value);
        public string Value
        {
            get { return GetProperty(ValueProperty); }
            set { SetProperty(ValueProperty, value); }
        }

        public static readonly PropertyInfo<string> NamespaceProperty = RegisterProperty<string>(c => c.Namespace);
        public string Namespace
        {
            get { return GetProperty(NamespaceProperty); }
            set { SetProperty(NamespaceProperty, value); }
        }

        public static readonly PropertyInfo<string> NamespacePrefixProperty = RegisterProperty<string>(c => c.NamespacePrefix);
        public string NamespacePrefix
        {
            get { return GetProperty(NamespacePrefixProperty); }
            set { SetProperty(NamespacePrefixProperty, value); }
        }

        public static readonly PropertyInfo<string> InnerXmlProperty = RegisterProperty<string>(c => c.InnerXml);
        public string InnerXml
        {
            get { return GetProperty(InnerXmlProperty); }
            set { SetProperty(InnerXmlProperty, value); }
        }


        public void Accept(IManifestVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}