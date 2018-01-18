
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
    public class Param
    {
        #region Public Properties

        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        public string Name
        {
            get {return(m_name);}
        }

        /// <summary>
        /// Gets the type of the parameter.
        /// </summary>
        public ParamType Type
        {
            get {return(m_type);}
        }

        /// <summary>
        /// Gets the value of the parameter.
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
        /// <param name="paramNode">Specifies an XML node based on which to initialize the instance.</param>
        internal Param(XmlNode paramNode)
        {
            m_name = paramNode.GetAttribute("name");

            try
            {
                SetTypeAndValue(paramNode);
            }

            catch (Exception ex)
            {
                throw new ArgumentException(LibraryMessages.Error062.FormatMessage(m_name), ex);
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
                    m_type = ParamType.Boolean;

                    m_value = Convert.ToBoolean(paramNode.InnerText);

                    break;

                case "byte":
                    m_type = ParamType.Byte;

                    m_value = Convert.ToByte(paramNode.InnerText);

                    break;

                case "char":
                    m_type = ParamType.Char;

                    m_value = Convert.ToChar(paramNode.InnerText);

                    break;

                case "date":
                    m_type = ParamType.Date;

                    m_value = DateTime.Parse(paramNode.InnerText, culture);

                    break;

                case "dateTime":
                    m_type = ParamType.DateTime;

                    RunRegexValidation(paramNode.InnerText, @"^\d\d\d\d\-\d\d\-\d\dT\d\d:\d\d:\d\d$");

                    m_value = Convert.ToDateTime(paramNode.InnerText, culture);

                    break;

                case "decimal":
                    m_type = ParamType.Decimal;

                    m_value = Convert.ToDecimal(paramNode.InnerText, culture);

                    break;

                case "double":
                    m_type = ParamType.Double;

                    m_value = Convert.ToDouble(paramNode.InnerText, culture);

                    break;

                case "float":
                    m_type = ParamType.Float;

                    m_value = Convert.ToSingle(paramNode.InnerText, culture);

                    break;

                case "int":
                    m_type = ParamType.Int;

                    m_value = Convert.ToInt32(paramNode.InnerText);

                    break;

                case "int16":
                    m_type = ParamType.Int16;

                    m_value = Convert.ToInt16(paramNode.InnerText);

                    break;

                case "int32":
                    m_type = ParamType.Int32;

                    m_value = Convert.ToInt32(paramNode.InnerText);

                    break;

                case "int64":
                    m_type = ParamType.Int64;

                    m_value = Convert.ToInt64(paramNode.InnerText);

                    break;

                case "long":
                    m_type = ParamType.Long;

                    m_value = Convert.ToInt64(paramNode.InnerText);

                    break;

                case "sbyte":
                    m_type = ParamType.SByte;

                    m_value = Convert.ToSByte(paramNode.InnerText);

                    break;

                case "short":
                    m_type = ParamType.Int16;

                    m_value = Convert.ToInt16(paramNode.InnerText);

                    break;

                case "single":
                    m_type = ParamType.Single;

                    m_value = Convert.ToSingle(paramNode.InnerText, culture);

                    break;

                case "string":
                    m_type = ParamType.String;

                    m_value = Convert.ToString(paramNode.InnerText);

                    break;

                case "time":
                    m_type = ParamType.Time;

                    RunRegexValidation(paramNode.InnerText, @"^\d\d:\d\d:\d\d$");

                    m_value = Convert.ToDateTime(paramNode.InnerText, culture);

                    break;

                case "timeSpan":
                    m_type = ParamType.TimeSpan;

                    m_value = TimeSpan.Parse(paramNode.InnerText, culture);

                    break;

                case "uint":
                    m_type = ParamType.UInt;

                    m_value = Convert.ToUInt32(paramNode.InnerText);

                    break;

                case "uint16":
                    m_type = ParamType.UInt16;

                    m_value = Convert.ToUInt16(paramNode.InnerText);

                    break;

                case "uint32":
                    m_type = ParamType.UInt32;

                    m_value = Convert.ToUInt32(paramNode.InnerText);

                    break;

                case "uint64":
                    m_type = ParamType.UInt64;

                    m_value = Convert.ToUInt64(paramNode.InnerText);

                    break;

                case "ulong":
                    m_type = ParamType.ULong;

                    m_value = Convert.ToUInt64(paramNode.InnerText);

                    break;

                case "ushort":
                    m_type = ParamType.UShort;

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
        private ParamType m_type;

        /// <summary>
        /// Stores the <see cref="Value"/> property.
        /// </summary>
        private object m_value;

        #endregion
    }
}
