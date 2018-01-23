
using System;

namespace AppXLibrary.Services
{
    public class AllParamTypeService : IAllParamTypeService
    {
        #region Public Constructors

        public AllParamTypeService(string stringValue)
        {
            this.StringValue = stringValue;
        }

        public AllParamTypeService(
            bool boolValue,
            byte byteValue,
            char charValue,
            DateTime dateValue,
            DateTime dateTimeValue,
            decimal decimalValue,
            double doubleValue,
            float floatValue,
            int intValue,
            short int16Value,
            int int32Value,
            long int64Value,
            long longValue,
            sbyte sbyteValue,
            short shortValue,
            float singleValue,
            string stringValue,
            DateTime timeValue,
            TimeSpan timeSpanValue,
            uint uintValue,
            ushort uint16Value,
            uint uint32Value,
            ulong uint64Value,
            ulong ulongValue,
            ushort ushortValue
        )
        {
            this.BoolValue = boolValue;

            this.ByteValue = byteValue;

            this.CharValue = charValue;

            this.DateValue = dateValue;

            this.DateTimeValue = dateTimeValue;

            this.DecimalValue = decimalValue;

            this.DoubleValue = doubleValue;

            this.FloatValue = floatValue;

            this.IntValue = intValue;

            this.Int16Value = int16Value;

            this.Int32Value = int32Value;

            this.Int64Value = int64Value;

            this.LongValue = longValue;

            this.SByteValue = sbyteValue;

            this.ShortValue = shortValue;

            this.SingleValue = singleValue;

            this.StringValue = stringValue;

            this.TimeValue = timeValue;

            this.TimeSpanValue = timeSpanValue;

            this.UintValue = uintValue;

            this.Uint16Value = uint16Value;

            this.Uint32Value = uint32Value;

            this.Uint64Value = uint64Value;

            this.UlongValue = ulongValue;

            this.UshortValue = ushortValue;
        }

        #endregion

        #region Public Properties

        public bool BoolValue {get; set;}

        public byte ByteValue {get; set;}

        public char CharValue {get; set;}

        public DateTime DateValue {get; set;}

        public DateTime DateTimeValue {get; set;}

        public decimal DecimalValue {get; set;}

        public double DoubleValue {get; set;}

        public float FloatValue {get; set;}

        public int IntValue {get; set;}

        public short Int16Value {get; set;}

        public int Int32Value {get; set;}

        public long Int64Value {get; set;}

        public long LongValue {get; set;}

        public sbyte SByteValue {get; set;}

        public short ShortValue {get; set;}

        public float SingleValue {get; set;}

        public string StringValue {get; set;}

        public DateTime TimeValue {get; set;}

        public TimeSpan TimeSpanValue {get; set;}

        public uint UintValue {get; set;}

        public ushort Uint16Value {get; set;}

        public uint Uint32Value {get; set;}

        public ulong Uint64Value {get; set;}

        public ulong UlongValue {get; set;}

        public ushort UshortValue {get; set;}

        #endregion
    }
}
