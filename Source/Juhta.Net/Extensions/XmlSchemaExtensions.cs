
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Collections.Generic;
using System.Xml.Schema;

namespace Juhta.Net.Extensions
{
    /// <summary>
    /// A static class that contains extension methods for the <see cref="XmlSchema"/> class.
    /// </summary>
    public static class XmlSchemaExtensions
    {
        #region Public Methods

        /// <summary>
        /// Gets an attribute's default value from the current XmlSchema instance.
        /// </summary>
        /// <param name="schema">Specifies the current XmlSchema object.</param>
        /// <param name="complexTypeName">Specifies the name of a complex type containing the attribute.</param>
        /// <param name="attributeName">Specifies an attribute name.</param>
        /// <returns>Returns the default value of the attribute or null if the complex type or the attribute has not
        /// been defined in the schema.</returns>
        public static string GetAttributeDefaultValue(this XmlSchema schema, string complexTypeName, string attributeName)
        {
            XmlSchemaComplexType complexType = null;
            List<XmlSchemaObjectCollection> attributeObjectCollections = new List<XmlSchemaObjectCollection>();
            XmlSchemaAttribute attribute;

            // Find the specified complex type

            foreach (XmlSchemaObject item in schema.Items)
            {
                if (!(item is XmlSchemaComplexType))
                    continue;

                // The schema item is a complex type element

                if (((XmlSchemaComplexType)item).Name == complexTypeName)
                {
                    complexType = (XmlSchemaComplexType)item;

                    break;
                }
                else
                    continue;
            }

            // Return if the specified complex type was not found
            if (complexType == null)
                return(null);

            // Get the attribute collections of the complex type

            attributeObjectCollections.Add(complexType.Attributes);

            if (complexType.ContentModel != null)
            {
                // Get the attributes defined with a simple content extension
                if (complexType.ContentModel.Content is XmlSchemaSimpleContentExtension)
                    attributeObjectCollections.Add(((XmlSchemaSimpleContentExtension)complexType.ContentModel.Content).Attributes);

                // Get the attributes defined with a complex content extension
                else if (complexType.ContentModel.Content is XmlSchemaComplexContentExtension)
                    attributeObjectCollections.Add(((XmlSchemaComplexContentExtension)complexType.ContentModel.Content).Attributes);
            }

            // Loop through the found attribute collections and their attributes

            foreach (XmlSchemaObjectCollection attributeObjectCollection in attributeObjectCollections)
                foreach (XmlSchemaObject attributeObject in attributeObjectCollection)
                {
                    attribute = (XmlSchemaAttribute)attributeObject;

                    if (attribute.Name == attributeName)
                        return(attribute.DefaultValue);
                }

            // The specified attribute was not found
            return(null);
        }

        #endregion
    }
}
