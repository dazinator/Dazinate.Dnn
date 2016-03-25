using System;
using System.Xml;
using System.Xml.XPath;

namespace Dazinate.Dnn.Manifest.Wip
{
    public static class XmlUtils
    {
        public static string ReadElement(XPathNavigator nav, string elementName)
        {
            return ValidateNode(GetNodeValue(nav, elementName), false, "");
        }

        public static string ReadElement(XPathNavigator nav, string elementName, string defaultValue)
        {
            return ValidateNode(GetNodeValue(nav, elementName), false, defaultValue);
        }

        public static string ReadElement(XPathNavigator nav, string elementName, bool isRequired, string defaultValue)
        {
            return ValidateNode(GetNodeValue(nav, elementName), isRequired, defaultValue);
        }

        /// <summary>Gets the value of a child node as a <see cref="string"/></summary>
        /// <param name="navigator">A navigator pointing to the parent node</param>
        /// <param name="path">An XPath expression to find the child node</param>
        /// <returns>The value of the node or <see cref="string.Empty"/> if the node doesn't exist or doesn't have a value</returns>
        public static string GetNodeValue(XPathNavigator navigator, string path)
        {
            return GetNodeValue(navigator, path, String.Empty);
        }

        /// <summary>Gets the value of a child node as a <see cref="string"/></summary>
        /// <param name="navigator">A navigator pointing to the parent node</param>
        /// <param name="path">An XPath expression to find the child node</param>
        /// <param name="defaultValue">Default value to return if the node doesn't exist or doesn't have a value</param>
        /// <returns>The value of the node or <paramref name="defaultValue"/></returns>
        public static string GetNodeValue(XPathNavigator navigator, string path, string defaultValue)
        {
            var childNodeNavigator = navigator.SelectSingleNode(path);
            if (childNodeNavigator == null)
            {
                return defaultValue;
            }

            var strValue = childNodeNavigator.Value;
            if (String.IsNullOrEmpty(strValue) && !String.IsNullOrEmpty(defaultValue))
            {
                return defaultValue;
            }

            return strValue;
        }

        public static string ReadAttribute(XPathNavigator nav, string attributeName)
        {
            return ValidateNode(nav.GetAttribute(attributeName, ""), false, "");
        }

        public static string ReadRequiredAttribute(XPathNavigator nav, string attributeName)
        {
            return ValidateNode(nav.GetAttribute(attributeName, ""), true, "");
        }

        private static string ValidateNode(string propValue, bool isRequired, string defaultValue)
        {
            if (String.IsNullOrEmpty(propValue))
            {
                if (isRequired)
                {
                    //Log Error
                    throw new Exception("A required attribute named: " + propValue + " is missing");
                }
                //Use Default
                propValue = defaultValue;
            }
            return propValue;
        }


        /// <summary>
        /// Writes an elements contents as a string, and if the string contains invalid xml charactes, then encapsulates the
        /// content in a CDATA.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="content"></param>
        public static void WriteInsideCDataIfNecessary(this XmlWriter writer, string content)
        {
            if (String.IsNullOrEmpty(content))
            {
                return;
            }

            // WriteString will happily escape any XML markup characters. However, 
            // for legibility we write content that contains certain special
            // characters as CDATA 
            if (IsValidXmlString(content))
            {
                writer.WriteString(content);
            }
            else
            {
                writer.WriteCData(content);
            }
        }

        

        static bool IsValidXmlString(string text)
        {
            try
            {
                XmlConvert.VerifyXmlChars(text);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}