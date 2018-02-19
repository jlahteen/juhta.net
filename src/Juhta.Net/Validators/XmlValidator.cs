
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace Juhta.Net.Validators
{
    /// <summary>
    /// A wrapper class that makes easier to validate XML documents against XML schemas.
    /// </summary>
    public class XmlValidator
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public XmlValidator()
        {
            m_schemaSet = new XmlSchemaSet();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds an XML schema to this validator instance.
        /// </summary>
        /// <param name="schema">Specifies an XmlSchema object.</param>
        public void AddSchema(XmlSchema schema)
        {
            m_schemaSet.Add(schema);
        }

        /// <summary>
        /// Adds an XML schema to this validator instance.
        /// </summary>
        /// <param name="schema">Specifies an XML schema string.</param>
        public void AddSchema(string schema)
        {
            m_schemaSet.Add(XmlSchema.Read(new StringReader(schema), null));
        }

        /// <summary>
        /// Adds an XML schema to this validator instance.
        /// </summary>
        /// <param name="schemaUri">Specifies the URI of a schema to be added to the instance.</param>
        public void AddSchema(Uri schemaUri)
        {
            m_schemaSet.Add(null, schemaUri.AbsoluteUri);
        }

        /// <summary>
        /// Validates a specified XML document.
        /// </summary>
        /// <param name="document">Specifies an XmlDocument object.</param>
        /// <remarks>If the XML document contains validation errors, the function collects the corresponding exceptions
        /// to an exception chain of the type XmlSchemaValidationException. This exception chain will be thrown after
        /// the validation process is finished.</remarks>
        public void Validate(XmlDocument document)
        {
            if (!m_schemaSet.Contains(document.DocumentElement.NamespaceURI))
                throw new XmlSchemaValidationException(LibraryMessages.Error032.FormatMessage(document.DocumentElement.NamespaceURI));

            document.Schemas = m_schemaSet;

            m_validationException = null;

            document.Validate(ValidationEventHandler);

            document.Schemas = null;

            if (m_validationException != null)
                throw m_validationException;
        }

        /// <summary>
        /// Validates a specified XML document against a specified XML schema.
        /// </summary>
        /// <param name="document">Specifies an XmlDocument object.</param>
        /// <param name="schema">Specifies an XML schema as a string.</param>
        /// <remarks>If the XML document contains validation errors, the function collects the corresponding exceptions
        /// to an exception chain of the type XmlSchemaValidationException. This exception chain will be thrown after
        /// the validation process is finished.</remarks>
        public static void Validate(XmlDocument document, string schema)
        {
            XmlValidator validator = new XmlValidator();

            validator.AddSchema(schema);

            validator.Validate(document);
        }

        /// <summary>
        /// Validates a specified XML document against a specified XML schema.
        /// </summary>
        /// <param name="document">Specifies an XmlDocument object.</param>
        /// <param name="schema">Specifies an XmlSchema object.</param>
        /// <remarks>If the XML document contains validation errors, the function collects the corresponding exceptions
        /// to an exception chain of the type XmlSchemaValidationException. This exception chain will be thrown after
        /// the validation process is finished.</remarks>
        public static void Validate(XmlDocument document, XmlSchema schema)
        {
            XmlValidator validator = new XmlValidator();

            validator.AddSchema(schema);

            validator.Validate(document);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Implements a validation event handler.
        /// </summary>
        private void ValidationEventHandler(object sender, ValidationEventArgs eventArgs)
        {
            if (eventArgs.Exception != null)
                m_validationException = new XmlSchemaValidationException(eventArgs.Message, m_validationException);
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Specifies a set of XML schemas added to this instance.
        /// </summary>
        private XmlSchemaSet m_schemaSet;

        /// <summary>
        /// Specifies the latest validation exception chain.
        /// </summary>
        private XmlSchemaValidationException m_validationException;

        #endregion
    }
}
