
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using Juhta.Net.Common;
using Juhta.Net.Extensions;
using System;
using System.Xml;

namespace Juhta.Net.Services
{
    /// <summary>
    /// Defines a class that represents parameters used in consctructors of dependency injections services.
    /// </summary>
    internal class Param
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
        /// Gets the type name of the parameter.
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

            SetTypeAndValue(paramNode);
        }

        #endregion

        #region Internal Methods
        #endregion

        #region Internal Properties
        #endregion

        #region Internal Types
        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes the type and value fields based on a specified XML node.
        /// </summary>
        /// <param name="paramNode">Specifies an XML node.</param>
        private void SetTypeAndValue(XmlNode paramNode)
        {
            switch (paramNode.LocalName)
            {
                case "booleanParam":
                    m_type = ParamType.Boolean;

                    m_value = Convert.ToBoolean(paramNode.InnerText);

                    break;

                case "byteParam":
                    m_type = ParamType.Byte;

                    m_value = Convert.ToByte(paramNode.InnerText);

                    break;

                case "charParam":
                    m_type = ParamType.Char;

                    m_value = Convert.ToChar(paramNode.InnerText);

                    break;

                case "dateTimeParam":
                    m_type = ParamType.DateTime;

                    m_value = Convert.ToDateTime(paramNode.InnerText);

                    break;

                case "decimalParam":
                    m_type = ParamType.Decimal;

                    m_value = Convert.ToDecimal(paramNode.InnerText);

                    break;

                case "doubleParam":
                    m_type = ParamType.Double;

                    m_value = Convert.ToDouble(paramNode.InnerText);

                    break;

                case "int16Param":
                    m_type = ParamType.Int16;

                    m_value = Convert.ToInt16(paramNode.InnerText);

                    break;

                case "int32Param":
                    m_type = ParamType.Int32;

                    m_value = Convert.ToInt32(paramNode.InnerText);

                    break;

                case "int64Param":
                    m_type = ParamType.Int64;

                    m_value = Convert.ToInt64(paramNode.InnerText);

                    break;

                case "sbyteParam":
                    m_type = ParamType.SByte;

                    m_value = Convert.ToSByte(paramNode.InnerText);

                    break;

                case "singleParam":
                    m_type = ParamType.Single;

                    m_value = Convert.ToSingle(paramNode.InnerText);

                    break;

                case "stringParam":
                    m_type = ParamType.String;

                    m_value = Convert.ToString(paramNode.InnerText);

                    break;

                case "timeSpanParam":
                    m_type = ParamType.TimeSpan;

                    m_value = TimeSpan.Parse(paramNode.InnerText);

                    break;

                case "uint16Param":
                    m_type = ParamType.UInt16;

                    m_value = Convert.ToUInt16(paramNode.InnerText);

                    break;

                case "uint32Param":
                    m_type = ParamType.UInt32;

                    m_value = Convert.ToUInt32(paramNode.InnerText);

                    break;

                case "uint64Param":
                    m_type = ParamType.UInt64;

                    m_value = Convert.ToUInt64(paramNode.InnerText);

                    break;

                default:
                    throw new UnimplementedCodeBranchException(paramNode.LocalName);
            }
        }

        #endregion

        #region Private Types
        #endregion

        #region Private Constants
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

        #region Destructor
        #endregion
    }
}
