
using System;

namespace AppXLibrary.Services
{
    public interface IAllParamTypeService
    {
        #region Properties

        bool BoolValue {get; set;}

        byte ByteValue {get; set;}

        char CharValue {get; set;}

        DateTime DateValue {get; set;}

        DateTime DateTimeValue {get; set;}

        decimal DecimalValue {get; set;}

        double DoubleValue {get; set;}

        float FloatValue {get; set;}

        int IntValue {get; set;}

        Int16 Int16Value {get; set;}

        Int32 Int32Value {get; set;}

        Int64 Int64Value {get; set;}

        long LongValue {get; set;}

        sbyte SByteValue {get; set;}

        short ShortValue {get; set;}

        Single SingleValue {get; set;}

        string StringValue {get; set;}

        DateTime TimeValue {get; set;}

        TimeSpan TimeSpanValue {get; set;}

        uint UintValue {get; set;}

        UInt16 Uint16Value {get; set;}

        UInt32 Uint32Value {get; set;}

        UInt64 Uint64Value {get; set;}

        ulong UlongValue {get; set;}

        ushort UshortValue {get; set;}

        #endregion
    }
}
