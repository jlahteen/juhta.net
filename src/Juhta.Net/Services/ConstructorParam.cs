
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Common;
using Juhta.Net.Extensions;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml;

namespace Juhta.Net.Services
{
    /// <summary>
    /// Defines a class that represents a single parameter used in constructors of dependency injection services.
    /// </summary>
    public class ConstructorParam
    {
        #region Public Properties

        /// <summary>
        /// Gets the name of the constructor parameter.
        /// </summary>
        public string Name
        {
            get {return(m_name);}
        }

        /// <summary>
        /// Gets the type of the constructor parameter.
        /// </summary>
        public ConstructorParamType Type
        {
            get {return(m_type);}
        }

        /// <summary>
        /// Gets the value of the constructor parameter.
        /// </summary>
        public object Value
        {
            get {return(m_value);}
        }

        #endregion

        #region Internal Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="constructorParamNode">Specifies an XML node based on which to initialize the instance.</param>
        internal ConstructorParam(XmlNode constructorParamNode)
        {
            m_name = constructorParamNode.GetAttribute("name");

            try
            {
                SetTypeAndValue(constructorParamNode);
            }

            catch (Exception ex)
            {
                throw new ConstructorParamException(LibraryMessages.Error062.FormatMessage(m_name), ex);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Runs a regex pattern validation for a value.
        /// </summary>
        /// <param name="value">Specifies a value.</param>
        /// <param name="pattern">Specifies a regex pattern.</param>
        void RunRegexValidation(string value, string pattern)
        {
            if (!Regex.IsMatch(value, pattern))
                throw new InvalidConfigValueException(LibraryMessages.Error060.FormatMessage(value, m_name, m_type));
        }

        /// <summary>
        /// Initializes the type and value fields based on a specified XML node.
        /// </summary>
        /// <param name="paramNode">Specifies an XML node.</param>
        private void SetTypeAndValue(XmlNode paramNode)
        {
            CultureInfo culture = CultureInfo.InvariantCulture;

            switch (paramNode.LocalName)
            {
                case "boolean":
                    m_type = ConstructorParamType.Boolean;

                    m_value = Convert.ToBoolean(paramNode.InnerText);

                    break;

                case "byte":
                    m_type = ConstructorParamType.Byte;

                    m_value = Convert.ToByte(paramNode.InnerText);

                    break;

                case "char":
                    m_type = ConstructorParamType.Char;

                    m_value = Convert.ToChar(paramNode.InnerText);

                    break;

                case "date":
                    m_type = ConstructorParamType.Date;

                    m_value = DateTime.Parse(paramNode.InnerText, culture);

                    break;

                case "dateTime":
                    m_type = ConstructorParamType.DateTime;

                    RunRegexValidation(paramNode.InnerText, @"^\d\d\d\d\-\d\d\-\d\dT\d\d:\d\d:\d\d$");

                    m_value = Convert.ToDateTime(paramNode.InnerText, culture);

                    break;

                case "decimal":
                    m_type = ConstructorParamType.Decimal;

                    m_value = Convert.ToDecimal(paramNode.InnerText, culture);

                    break;

                case "double":
                    m_type = ConstructorParamType.Double;

                    m_value = Convert.ToDouble(paramNode.InnerText, culture);

                    break;

                case "float":
                    m_type = ConstructorParamType.Float;

                    m_value = Convert.ToSingle(paramNode.InnerText, culture);

                    break;

                case "int":
                    m_type = ConstructorParamType.Int;

                    m_value = Convert.ToInt32(paramNode.InnerText);

                    break;

                case "int16":
                    m_type = ConstructorParamType.Int16;

                    m_value = Convert.ToInt16(paramNode.InnerText);

                    break;

                case "int32":
                    m_type = ConstructorParamType.Int32;

                    m_value = Convert.ToInt32(paramNode.InnerText);

                    break;

                case "int64":
                    m_type = ConstructorParamType.Int64;

                    m_value = Convert.ToInt64(paramNode.InnerText);

                    break;

                case "long":
                    m_type = ConstructorParamType.Long;

                    m_value = Convert.ToInt64(paramNode.InnerText);

                    break;

                case "sbyte":
                    m_type = ConstructorParamType.SByte;

                    m_value = Convert.ToSByte(paramNode.InnerText);

                    break;

                case "serviceRef":
                    m_type = ConstructorParamType.ServiceRef;

                    m_value = new ServiceId(paramNode.InnerText);

                    break;

                case "short":
                    m_type = ConstructorParamType.Int16;

                    m_value = Convert.ToInt16(paramNode.InnerText);

                    break;

                case "single":
                    m_type = ConstructorParamType.Single;

                    m_value = Convert.ToSingle(paramNode.InnerText, culture);

                    break;

                case "string":
                    m_type = ConstructorParamType.String;

                    m_value = Convert.ToString(paramNode.InnerText);

                    break;

                case "time":
                    m_type = ConstructorParamType.Time;

                    RunRegexValidation(paramNode.InnerText, @"^\d\d:\d\d:\d\d$");

                    m_value = Convert.ToDateTime("0001-01-01T" + paramNode.InnerText, culture);

                    break;

                case "timeSpan":
                    m_type = ConstructorParamType.TimeSpan;

                    m_value = TimeSpan.Parse(paramNode.InnerText, culture);

                    break;

                case "uint":
                    m_type = ConstructorParamType.UInt;

                    m_value = Convert.ToUInt32(paramNode.InnerText);

                    break;

                case "uint16":
                    m_type = ConstructorParamType.UInt16;

                    m_value = Convert.ToUInt16(paramNode.InnerText);

                    break;

                case "uint32":
                    m_type = ConstructorParamType.UInt32;

                    m_value = Convert.ToUInt32(paramNode.InnerText);

                    break;

                case "uint64":
                    m_type = ConstructorParamType.UInt64;

                    m_value = Convert.ToUInt64(paramNode.InnerText);

                    break;

                case "ulong":
                    m_type = ConstructorParamType.ULong;

                    m_value = Convert.ToUInt64(paramNode.InnerText);

                    break;

                case "ushort":
                    m_type = ConstructorParamType.UShort;

                    m_value = Convert.ToUInt16(paramNode.InnerText);

                    break;

                default:
                    throw new UnimplementedCodeBranchException(paramNode.LocalName);
            }
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Stores the <see cref="Name"/> property.
        /// </summary>
        private string m_name;

        /// <summary>
        /// Stores the <see cref="Type"/> property.
        /// </summary>
        private ConstructorParamType m_type;

        /// <summary>
        /// Stores the <see cref="Value"/> property.
        /// </summary>
        private object m_value;

        #endregion
    }
}
