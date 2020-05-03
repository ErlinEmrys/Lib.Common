using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erlin.Lib.Common.Serialization
{
    /// <summary>
    /// Any serializable writer
    /// </summary>
    public interface IWriter : IDisposable
    {
        /// <summary>
        /// Flush all data
        /// </summary>
        void Flush();

        /// <summary>
        /// Write boolean
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        void WriteBool(string fieldName, bool value);

        /// <summary>
        /// Write nullable boolean
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        void WriteBoolN(string fieldName, bool? value);

        /// <summary>
        /// Write byte
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        void WriteByte(string fieldName, byte value);

        /// <summary>
        /// Write nullable byte
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        void WriteByteN(string fieldName, byte? value);

        /// <summary>
        /// Write sbyte
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        void WriteSByte(string fieldName, sbyte value);

        /// <summary>
        /// Write nullable sbyte
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        void WriteSByteN(string fieldName, sbyte? value);

        /// <summary>
        /// Write byte array
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        void WriteByteArr(string fieldName, byte[] value);

        /// <summary>
        /// Write nullable byte array
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        void WriteByteArrN(string fieldName, byte[]? value);

        /// <summary>
        /// Write sbyte array
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        void WriteSByteArr(string fieldName, sbyte[] value);

        /// <summary>
        /// Write nullable sbyte array
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        void WriteSByteArrN(string fieldName, sbyte[]? value);

        /// <summary>
        /// Write UInt16 (ushort)
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        void WriteUInt16(string fieldName, ushort value);

        /// <summary>
        /// Write nullable UInt16 (ushort?)
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        void WriteUInt16N(string fieldName, ushort? value);

        /// <summary>
        /// Write Int16 (short)
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        void WriteInt16(string fieldName, short value);

        /// <summary>
        /// Write nullable Int16 (short?)
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        void WriteInt16N(string fieldName, short? value);

        /// <summary>
        /// Write Int16 (short) array
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        void WriteInt16Arr(string fieldName, short[] value);

        /// <summary>
        /// Write nullable Int16 (short) array
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        void WriteInt16ArrN(string fieldName, short[]? value);

        /// <summary>
        /// Write float
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        void WriteFloat(string fieldName, float value);

        /// <summary>
        /// Write nullable float
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        void WriteFloatN(string fieldName, float? value);

        /// <summary>
        /// Write int32
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        void WriteInt32(string fieldName, int value);

        /// <summary>
        /// Write nullable int32
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        void WriteInt32N(string fieldName, int? value);

        /// <summary>
        /// Write int64
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        void WriteInt64(string fieldName, long value);

        /// <summary>
        /// Write nullable int64
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        void WriteInt64N(string fieldName, long? value);

        /// <summary>
        /// Write decimal
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        void WriteDecimal(string fieldName, decimal value);

        /// <summary>
        /// Write nullable decimal
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        void WriteDecimalN(string fieldName, decimal? value);

        /// <summary>
        /// Write string
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        void WriteString(string fieldName, string value);

        /// <summary>
        /// Write nullable string
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        void WriteStringN(string fieldName, string? value);

        /// <summary>
        /// Write Guid
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        void WriteGuid(string fieldName, Guid value);

        /// <summary>
        /// Write nullable Guid
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        void WriteGuidN(string fieldName, Guid? value);

        /// <summary>
        /// Write null object
        /// </summary>
        /// <param name="fieldName">Field name</param>
        void WriteObjectEmpty(string fieldName);

        /// <summary>
        /// Write start of a object
        /// </summary>
        /// <param name="fieldName">Field name</param>
        void WriteObjectStart(string fieldName);

        /// <summary>
        /// Write end of a object
        /// </summary>
        /// <param name="fieldName">Field name</param>
        void WriteObjectEnd(string fieldName);

        /// <summary>
        /// Write start of collection
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="count">Count of objects in collection</param>
        void WriteCollectionStart(string fieldName, int count);

        /// <summary>
        /// Write separator of objects in collection - indicates, that another object follow
        /// </summary>
        void WriteCollectionObjectSeparator();

        /// <summary>
        /// Write end of collection
        /// </summary>
        /// <param name="fieldName">Field name</param>
        void WriteCollectionEnd(string fieldName);
    }
}