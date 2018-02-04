
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Common;
using Juhta.Net.Helpers;
using System;
using System.Xml;

namespace Juhta.Net.Extensions
{
    /// <summary>
    /// A static class that contains extension methods for the <see cref="XmlNode"/> class.
    /// </summary>
    public static class XmlNodeExtensions
    {
        #region Public Methods

        /// <summary>
        /// Appends a child node to the current node.
        /// </summary>
        /// <param name="node">Specifies the current node.</param>
        /// <param name="childName">Specifies a name for the child node.</param>
        /// <returns>Returns the appended child node.</returns>
        public static XmlNode AppendChild(this XmlNode node, string childName)
        {
            return(node.AppendChild(childName, null, null, null));
        }

        /// <summary>
        /// Appends a child node to the current node.
        /// </summary>
        /// <param name="node">Specifies the current node.</param>
        /// <param name="childName">Specifies a name for the child node.</param>
        /// <param name="childInnerText">Specifies an inner text for the child node.</param>
        /// <returns>Returns the appended child node.</returns>
        public static XmlNode AppendChild(this XmlNode node, string childName, string childInnerText)
        {
            return(node.AppendChild(childName, childInnerText, null, null));
        }

        /// <summary>
        /// Appends a child node to the current node.
        /// </summary>
        /// <param name="node">Specifies the current node.</param>
        /// <param name="qualifiedChildName">Specifies a qualified name for the child node.</param>
        /// <param name="namespaceUri">Specifies a namespace URI for the child node.</param>
        /// <param name="innerText">Specifies an inner text for the child node. Can be null.</param>
        /// <returns>Returns the appended child node.</returns>
        public static XmlNode AppendChild(this XmlNode node, string qualifiedChildName, string namespaceUri, string innerText)
        {
            XmlNode child;

            child = node.OwnerDocument.CreateElement(qualifiedChildName, namespaceUri);

            if (innerText != null)
                child.InnerText = innerText;

            return(node.AppendChild(child));
        }

        /// <summary>
        /// Appends a child node to the current node.
        /// </summary>
        /// <param name="node">Specifies the current node.</param>
        /// <param name="childName">Specifies a name for the child node.</param>
        /// <param name="childInnerText">Specifies an inner text for the child node. Can be null.</param>
        /// <param name="attributeNames">A string array specifying names for the attributes to be added to the child
        /// node. Can be null.</param>
        /// <param name="attributeValues">A string array specifying values for the attributes to be added to the child
        /// node. This parameter will be ignored if <paramref name="attributeNames"/> is null.</param>
        /// <returns>Returns the appended child node.</returns>
        /// <remarks>The lengths of the arrays <paramref name="attributeNames"/> and <paramref name="attributeValues"/>
        /// must match.</remarks>
        public static XmlNode AppendChild(this XmlNode node, string childName, string childInnerText, string[] attributeNames, string[] attributeValues)
        {
            XmlNode child;
            int i;
            XmlAttribute attribute;

            child = node.OwnerDocument.CreateElement(childName);

            if (childInnerText != null)
                child.InnerText = childInnerText;

            if (attributeNames != null)
            {
                ArgumentHelper.CheckNotNull(nameof(attributeValues), attributeValues);

                if (attributeNames.Length != attributeValues.Length)
                    throw new ArgumentException(CommonMessages.Error013.FormatMessage("attributeNames", "attributeValues"));

                for (i = 0; i < attributeNames.Length; i++)
                {
                    attribute = node.OwnerDocument.CreateAttribute(attributeNames[i]);
                    attribute.Value = attributeValues[i];

                    child.Attributes.Append(attribute);
                }
            }

            node.AppendChild(child);

            return(child);
        }

        /// <summary>
        /// Copies the current node to another node.
        /// </summary>
        /// <param name="node">Specifies the current node.</param>
        /// <param name="destination">Specifies a destination node.</param>
        /// <remarks>Copying covers all attributes and child nodes.</remarks>
        public static void CopyTo(this XmlNode node, XmlNode destination)
        {
            destination.InnerXml = node.InnerXml;

            destination.Attributes.RemoveAll();

            foreach (XmlAttribute attr in node.Attributes)
                destination.SetAttribute(attr.Name, attr.Value);
        }

        /// <summary>
        /// Gets an attribute value from the current node.
        /// </summary>
        /// <param name="node">Specifies the current node.</param>
        /// <param name="name">Specifies an attribute name.</param>
        /// <returns>Returns the value of the specified attribute or an empty string, if the attribute wasn't found.</returns>
        public static string GetAttribute(this XmlNode node, string name)
        {
            XmlNode attribute;

            if ((attribute = node.Attributes.GetNamedItem(name)) != null)
                return(attribute.Value);
            else
                return("");
        }

        /// <summary>
        /// Gets an attribute value from the current node.
        /// </summary>
        /// <param name="node">Specifies the current node.</param>
        /// <param name="name">Specifies an attribute name.</param>
        /// <param name="defaultValue">Specifies a default value for the attribute.</param>
        /// <returns>Returns the value of the specified attribute or the default value, if the attribute wasn't found.</returns>
        public static string GetAttribute(this XmlNode node, string name, string defaultValue)
        {
            if (node.HasAttribute(name))
                return(node.GetAttribute(name));
            else
                return(defaultValue);
        }

        /// <summary>
        /// Checks whether the current node has a specified attribute.
        /// </summary>
        /// <param name="node">Specifies the current node.</param>
        /// <param name="name">Specifies an attribute name.</param>
        /// <returns>Returns true if the current node has the specified attribute, otherwise false.</returns>
        public static bool HasAttribute(this XmlNode node, string name)
        {
            return(node.Attributes.GetNamedItem(name) != null);
        }

        /// <summary>
        /// Sets an attribute to the current node.
        /// </summary>
        /// <param name="node">Specifies the current node.</param>
        /// <param name="name">Specifies a name for the attribute to be set.</param>
        /// <param name="value">Specifies a value for the attribute to be set.</param>
        /// <remarks>If the attribute already exists the function just updates its value.</remarks>
        public static void SetAttribute(this XmlNode node, string name, string value)
        {
            XmlNode attribute;
            XmlAttribute newAttribute;

            if ((attribute = node.Attributes.GetNamedItem(name)) == null)
            {
                newAttribute = node.OwnerDocument.CreateAttribute(name);

                attribute = node.Attributes.Append(newAttribute);
            }

            attribute.Value = value;
        }

        /// <summary>
        /// Converts this XmlNode instance to a built-in type object based on its type and value attributes that are
        /// assumed to exist in the node.
        /// </summary>
        /// <param name="node">Specifies the current XmlNode instance.</param>
        /// <returns>Returns the built-in type object, that is, the result of the conversion.</returns>
        public static object ToBuiltInTypeObject(this XmlNode node)
        {
            XmlNode attribute;
            string type, value;
            object @object = null;

            if ((attribute = node.Attributes.GetNamedItem("type")) != null)
                type = attribute.Value;
            else
                throw new ArgumentException("Specified node has no type attribute.", "node");

            if ((attribute = node.Attributes.GetNamedItem("value")) != null)
                value = attribute.Value;
            else
                throw new ArgumentException("Specified node has no value attribute.", "node");

            switch (type)
            {
                case "System.Boolean":
                    @object = Convert.ToBoolean(value);

                    break;

                case "System.Byte":
                    @object = Convert.ToByte(value);

                    break;

                case "System.Char":
                    @object = Convert.ToChar(value);

                    break;

                case "System.DateTime":
                    @object = Convert.ToDateTime(value.Replace(',', '.'));

                    break;

                case "System.Decimal":
                    @object = Convert.ToDecimal(value.Replace('.', ','));

                    break;

                case "System.Double":
                    @object = Convert.ToDouble(value.Replace('.', ','));

                    break;

                case "System.Int16":
                    @object = Convert.ToInt16(value);

                    break;

                case "System.Int32":
                    @object = Convert.ToInt32(value);

                    break;

                case "System.Int64":
                    @object = Convert.ToInt64(value);

                    break;

                case "System.SByte":
                    @object = Convert.ToSByte(value);

                    break;

                case "System.Single":
                    @object = Convert.ToSingle(value.Replace('.', ','));

                    break;

                case "System.String":
                    @object = Convert.ToString(value);

                    break;

                case "System.TimeSpan":
                    @object = TimeSpan.Parse(value.Replace(',', '.'));

                    break;

                case "System.UInt16":
                    @object = Convert.ToUInt16(value);

                    break;

                case "System.UInt32":
                    @object = Convert.ToUInt32(value);

                    break;

                case "System.UInt64":
                    @object = Convert.ToUInt64(value);

                    break;

                default:
                    throw new ArgumentException(String.Format("Type attribute '{0}' is not supported.", type), "node");
            }

            return(@object);
        }

        /// <summary>
        /// Converts the child nodes of this XmlNode instance to an array of built-in type objects.
        /// </summary>
        /// <param name="node">Specifies the current XmlNode instance.</param>
        /// <returns>Returns an array of the built-in type objects, that is, the result of the conversion.</returns>
        /// <seealso cref="ToBuiltInTypeObject"/>
        public static object[] ToBuiltInTypeObjectArray(this XmlNode node)
        {
            object[] objects;

            if (!node.HasChildNodes)
                throw new ArgumentException("Specified node has no child nodes.", "node");

            objects = new object[node.ChildNodes.Count];

            for (int i = 0; i < objects.Length; i++)
                objects[i] = node.ChildNodes[i].ToBuiltInTypeObject();

            return(objects);
        }

        #endregion
    }
}
