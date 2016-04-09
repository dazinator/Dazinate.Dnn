using System.Xml.XPath;
using Dazinate.Dnn.Manifest.Ioc;
using Dazinate.Dnn.Manifest.Model.Node.ObjectFactory;

namespace Dazinate.Dnn.Manifest.Model.NodesList.ObjectFactory
{
    public class NodesListObjectFactory : BaseObjectFactory, INodesListObjectFactory
    {
        private readonly INodeObjectFactory _nodeObjectFactory;

        public NodesListObjectFactory(IObjectActivator activator, INodeObjectFactory nodebjectFactory) : base(activator)
        {
            _nodeObjectFactory = nodebjectFactory;
        }

        public INodesList Fetch(XPathNavigator xpathNavigator)
        {
            //  var packagesNav = xpathNavigator.Select("packages/package");

            var list = CreateInstance<NodesList>();
            list.RaiseListChangedEvents = false;

            // loop through packages.
            foreach (XPathNavigator node in xpathNavigator.Select("configuration/nodes/node"))
            {
                LoadNodeItem(node, list);
            }

            list.RaiseListChangedEvents = true;

            MarkOld(list);
            MarkAsChild(list);
            CheckRules(list);
            return list;
        }


        private void LoadNodeItem(XPathNavigator nav, INodesList list)
        {
            var item = _nodeObjectFactory.Fetch(nav);
            list.Add(item);
        }

    }
}