
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

namespace Juhta.Net.Services
{
    #region Public Types

    /// <summary>
    /// Defines an enumeration for the parameter types used in constructors of depencendy injection services.
    /// </summary>
    public enum ConstructorParamType
    {
        /// <summary>
        /// Specifies the <see cref="System.Boolean"/> type.
        /// </summary>
        Boolean,

        /// <summary>
        /// Specifies the <see cref="System.Byte"/> type.
        /// </summary>
        Byte,

        /// <summary>
        /// Specifies the <see cref="System.Char"/> type.
        /// </summary>
        Char,

        /// <summary>
        /// Maps to the <see cref="System.DateTime"/> type so that the time part is 00:00:00.
        /// </summary>
        Date,

        /// <summary>
        /// Specifies the <see cref="System.DateTime"/> type.
        /// </summary>
        DateTime,

        /// <summary>
        /// Specifies the <see cref="System.Decimal"/> type.
        /// </summary>
        Decimal,

        /// <summary>
        /// Specifies the <see cref="System.Double"/> type.
        /// </summary>
        Double,

        /// <summary>
        /// Maps to the <see cref="System.Single"/> type.
        /// </summary>
        Float,

        /// <summary>
        /// Maps to the <see cref="System.Int32"/> type.
        /// </summary>
        Int,

        /// <summary>
        /// Specifies the <see cref="System.Int16"/> type.
        /// </summary>
        Int16,

        /// <summary>
        /// Specifies the <see cref="System.Int32"/> type.
        /// </summary>
        Int32,

        /// <summary>
        /// Specifies the <see cref="System.Int64"/> type.
        /// </summary>
        Int64,

        /// <summary>
        /// Maps to the <see cref="System.Int64"/> type.
        /// </summary>
        Long,

        /// <summary>
        /// Specifies the <see cref="System.SByte"/> type.
        /// </summary>
        SByte,

        /// <summary>
        /// Maps to the <see cref="System.Int16"/> type.
        /// </summary>
        Short,

        /// <summary>
        /// Specifies the <see cref="System.Single"/> type.
        /// </summary>
        Single,

        /// <summary>
        /// Specifies the <see cref="System.String"/> type.
        /// </summary>
        String,

        /// <summary>
        /// Maps to the <see cref="System.DateTime"/> type so that the date part is uninitialized.
        /// </summary>
        Time,

        /// <summary>
        /// Specifies the <see cref="System.TimeSpan"/> type.
        /// </summary>
        TimeSpan,

        /// <summary>
        /// Maps to the <see cref="System.UInt32"/> type.
        /// </summary>
        UInt,

        /// <summary>
        /// Specifies the <see cref="System.UInt16"/> type.
        /// </summary>
        UInt16,

        /// <summary>
        /// Specifies the <see cref="System.UInt32"/> type.
        /// </summary>
        UInt32,

        /// <summary>
        /// Specifies the <see cref="System.UInt64"/> type.
        /// </summary>
        UInt64,

        /// <summary>
        /// Maps to the <see cref="System.UInt64"/> type.
        /// </summary>
        ULong,

        /// <summary>
        /// Maps to the <see cref="System.UInt16"/> type.
        /// </summary>
        UShort
    }

    #endregion
}
