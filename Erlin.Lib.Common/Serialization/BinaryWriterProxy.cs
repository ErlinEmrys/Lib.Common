using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erlin.Lib.Common.Serialization
{
    /// <summary>
    /// Proxy for classic BinaryWriter
    /// </summary>
    public class BinaryWriterProxy : IWriter
    {
        /// <summary>
        /// Postfix for value existence check
        /// </summary>
        public const string NOT_NULL_FIELD_NAME_POSTFIX = "#NotNull";
        private readonly BinaryWriter _writer;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="stream">Output stream</param>
        public BinaryWriterProxy(Stream stream)
        {
            _writer = new BinaryWriter(stream, Encoding.UTF8);
        }

        /// <summary>
        /// Release all resources
        /// </summary>
        public void Dispose()
        {
            _writer.Dispose();
        }

        /// <summary>
        /// Flush all data
        /// </summary>
        public void Flush()
        {
            _writer.Flush();
        }

        /// <summary>
        /// Write boolean
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        public void WriteBool(string fieldName, bool value)
        {
            _writer.Write(value);
        }

        /// <summary>
        /// Write nullable boolean
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        public void WriteBoolN(string fieldName, bool? value)
        {
            WriteValueExist(fieldName, value.HasValue);
            if (value.HasValue)
            {
                WriteBool(fieldName, value.Value);
            }
        }

        /// <summary>
        /// Write nullable byte
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        public void WriteByteN(string fieldName, byte? value)
        {
            WriteValueExist(fieldName, value.HasValue);
            if (value.HasValue)
            {
                WriteByte(fieldName, value.Value);
            }
        }

        /// <summary>
        /// Write nullable sbyte
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        public void WriteSByteN(string fieldName, sbyte? value)
        {
            WriteValueExist(fieldName, value.HasValue);
            if (value.HasValue)
            {
                WriteSByte(fieldName, value.Value);
            }
        }

        /// <summary>
        /// Write nullable UInt16 (ushort?)
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        public void WriteUInt16N(string fieldName, ushort? value)
        {
            WriteValueExist(fieldName, value.HasValue);
            if (value.HasValue)
            {
                WriteUInt16(fieldName, value.Value);
            }
        }

        /// <summary>
        /// Write nullable Int16 (short?)
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        public void WriteInt16N(string fieldName, short? value)
        {
            WriteValueExist(fieldName, value.HasValue);
            if (value.HasValue)
            {
                WriteInt16(fieldName, value.Value);
            }
        }

        /// <summary>
        /// Write nullable float
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        public void WriteFloatN(string fieldName, float? value)
        {
            WriteValueExist(fieldName, value.HasValue);
            if (value.HasValue)
            {
                WriteFloat(fieldName, value.Value);
            }
        }

        /// <summary>
        /// Write nullable int32
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        public void WriteInt32N(string fieldName, int? value)
        {
            WriteValueExist(fieldName, value.HasValue);
            if (value.HasValue)
            {
                WriteInt32(fieldName, value.Value);
            }
        }

        /// <summary>
        /// Write nullable int64
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        public void WriteInt64N(string fieldName, long? value)
        {
            WriteValueExist(fieldName, value.HasValue);
            if (value.HasValue)
            {
                WriteInt64(fieldName, value.Value);
            }
        }

        /// <summary>
        /// Write nullable decimal
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        public void WriteDecimalN(string fieldName, decimal? value)
        {
            WriteValueExist(fieldName, value.HasValue);
            if (value.HasValue)
            {
                WriteDecimal(fieldName, value.Value);
            }
        }

        /// <summary>
        /// Write nullable Guid
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        public void WriteGuidN(string fieldName, Guid? value)
        {
            WriteValueExist(fieldName, value.HasValue);
            if (value.HasValue)
            {
                WriteGuid(fieldName, value.Value);
            }
        }

        /// <summary>
        /// Write byte
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        public void WriteByte(string fieldName, byte value)
        {
            _writer.Write(value);
        }

        /// <summary>
        /// Write sbyte
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        public void WriteSByte(string fieldName, sbyte value)
        {
            _writer.Write(value);
        }

        /// <summary>
        /// Write byte array
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        public void WriteByteArr(string fieldName, byte[] value)
        {
            WriteByteArrN(fieldName, value);
        }

        /// <summary>
        /// Write nullable byte array
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        public void WriteByteArrN(string fieldName, byte[]? value)
        {
            int length = value?.Length ?? -1;
            _writer.Write(length);
            if (value != null)
            {
                _writer.Write(value);
            }
        }

        /// <summary>
        /// Write sbyte array
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        public void WriteSByteArr(string fieldName, sbyte[] value)
        {
            WriteSByteArrN(fieldName, value);
        }

        /// <summary>
        /// Write nullable sbyte array
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        public void WriteSByteArrN(string fieldName, sbyte[]? value)
        {
            int length = value?.Length ?? -1;
            _writer.Write(length);
            if (value != null)
            {
                for (int i = 0; i < length; i++)
                {
                    _writer.Write(value[i]);
                }
            }
        }

        /// <summary>
        /// Write UInt16 (ushort)
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        public void WriteUInt16(string fieldName, ushort value)
        {
            _writer.Write(value);
        }

        /// <summary>
        /// Write Int16 (short)
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        public void WriteInt16(string fieldName, short value)
        {
            _writer.Write(value);
        }

        /// <summary>
        /// Write Int16 (short) array
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        public void WriteInt16Arr(string fieldName, short[] value)
        {
            WriteInt16ArrN(fieldName, value);
        }

        /// <summary>
        /// Write nullable Int16 (short) array
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        public void WriteInt16ArrN(string fieldName, short[]? value)
        {
            int length = value?.Length ?? -1;
            _writer.Write(length);
            if (value != null)
            {
                for (int i = 0; i < length; i++)
                {
                    _writer.Write(value[i]);
                }
            }
        }

        /// <summary>
        /// Write float
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        public void WriteFloat(string fieldName, float value)
        {
            _writer.Write(value);
        }

        /// <summary>
        /// Write int32
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        public void WriteInt32(string fieldName, int value)
        {
            _writer.Write(value);
        }

        /// <summary>
        /// Write int64
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        public void WriteInt64(string fieldName, long value)
        {
            _writer.Write(value);
        }

        /// <summary>
        /// Write decimal
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        public void WriteDecimal(string fieldName, decimal value)
        {
            _writer.Write(value);
        }

        /// <summary>
        /// Write string
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        public void WriteString(string fieldName, string value)
        {
            WriteStringN(fieldName, value);
        }

        /// <summary>
        /// Write nullable string
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        public void WriteStringN(string fieldName, string? value)
        {
            if (value == null)
            {
                _writer.Write(-1);
            }
            else if (value == string.Empty)
            {
                _writer.Write(0);
            }
            else
            {
                byte[] array = Encoding.UTF8.GetBytes(value);
                _writer.Write(array.Length);
                _writer.Write(array);
            }
        }

        /// <summary>
        /// Write Guid
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="value">Write value</param>
        public void WriteGuid(string fieldName, Guid value)
        {
            WriteByteArr(fieldName, value.ToByteArray());
        }

        /// <summary>
        /// Write null object
        /// </summary>
        /// <param name="fieldName">Field name</param>
        public void WriteObjectEmpty(string fieldName)
        {
            _writer.Write((byte)0);
        }

        /// <summary>
        /// Write start of a object
        /// </summary>
        /// <param name="fieldName">Field name</param>
        public void WriteObjectStart(string fieldName)
        {
            _writer.Write((byte)1);
        }

        /// <summary>
        /// Write end of a object
        /// </summary>
        /// <param name="fieldName">Field name</param>
        public void WriteObjectEnd(string fieldName)
        {
            _writer.Write(byte.MaxValue);
        }

        /// <summary>
        /// Write start of collection
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="count">Count of objects in collection</param>
        public void WriteCollectionStart(string fieldName, int count)
        {
            _writer.Write(count);
        }

        /// <summary>
        /// Write separator of objects in collection - indicates, that another object follow
        /// </summary>
        public void WriteCollectionObjectSeparator()
        {
        }

        /// <summary>
        /// Write end of collection
        /// </summary>
        /// <param name="fieldName">Field name</param>
        public void WriteCollectionEnd(string fieldName)
        {
        }

        /// <summary>
        /// Write existence of value
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="hasValue">Value exist</param>
        private void WriteValueExist(string fieldName, bool hasValue)
        {
            WriteBool($"{fieldName}{NOT_NULL_FIELD_NAME_POSTFIX}", hasValue);
        }
    }
}