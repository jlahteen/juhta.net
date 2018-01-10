
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml;
using Juhta.Net.Extensions;
using Juhta.Net.Common;

namespace Juhta.Net.Services
{
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
        public string Type
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

        #region Public Indexers
        #endregion

        #region Public Types
        #endregion

        #region Public Constants
        #endregion

        #region Protected Constructors
        #endregion

        #region Protected Methods
        #endregion

        #region Protected Properties
        #endregion

        #region Protected Types
        #endregion

        #region Protected Fields
        #endregion

        #region Internal Constructors

        internal Param(XmlNode paramNode)
        {
            m_name = paramNode.GetAttribute("name");

            m_type = paramNode.GetAttribute("type");

            SetValue(paramNode.GetAttribute("value"));
        }

        #endregion

        #region Internal Methods
        #endregion

        #region Internal Properties
        #endregion

        #region Internal Types
        #endregion

        #region Private Methods

        private void SetValue(string value)
        {
            switch (m_type)
            {
                case "System.Boolean":
                    m_value = Convert.ToBoolean(value);

                    break;

                case "System.Byte":
                    m_value = Convert.ToByte(value);

                    break;

                case "System.Char":
                    m_value = Convert.ToChar(value);

                    break;

                case "System.DateTime":
                    m_value = Convert.ToDateTime(value.Replace(',', '.'));

                    break;

                case "System.Decimal":
                    m_value = Convert.ToDecimal(value.Replace('.', ','));

                    break;

                case "System.Double":
                    m_value = Convert.ToDouble(value.Replace('.', ','));

                    break;

                case "System.Int16":
                    m_value = Convert.ToInt16(value);

                    break;

                case "System.Int32":
                    m_value = Convert.ToInt32(value);

                    break;

                case "System.Int64":
                    m_value = Convert.ToInt64(value);

                    break;

                case "System.SByte":
                    m_value = Convert.ToSByte(value);

                    break;

                case "System.Single":
                    m_value = Convert.ToSingle(value.Replace('.', ','));

                    break;

                case "System.String":
                    m_value = Convert.ToString(value);

                    break;

                case "System.TimeSpan":
                    m_value = TimeSpan.Parse(value.Replace(',', '.'));

                    break;

                case "System.UInt16":
                    m_value = Convert.ToUInt16(value);

                    break;

                case "System.UInt32":
                    m_value = Convert.ToUInt32(value);

                    break;

                case "System.UInt64":
                    m_value = Convert.ToUInt64(value);

                    break;

                default:
                    throw new UnimplementedCodeBranchException(m_type);
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
        private string m_type;

        /// <summary>
        /// Stores the <see cref="Value"/> property.
        /// </summary>
        private object m_value;

        #endregion

        #region Destructor
        #endregion
    }
}
