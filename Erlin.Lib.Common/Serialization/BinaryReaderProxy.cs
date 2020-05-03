using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Erlin.Lib.Common.Exceptions;

namespace Erlin.Lib.Common.Serialization
{
    /// <summary>
    /// Proxy for classic BinaryReader
    /// </summary>
    public class BinaryReaderProxy : IReader
    {
        private readonly BinaryReader _reader;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="stream">Input stream</param>
        public BinaryReaderProxy(Stream stream)
        {
            _reader = new BinaryReader(stream, Encoding.UTF8, true);
        }

        /// <summary>
        /// Release all resources
        /// </summary>
        public void Dispose()
        {
            _reader.Dispose();
        }

        /// <summary>
        /// Read boolean
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        public bool ReadBool(string fieldName)
        {
            return _reader.ReadBoolean();
        }

        /// <summary>
        /// Read byte
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        public byte ReadByte(string fieldName)
        {
            return _reader.ReadByte();
        }

        /// <summary>
        /// Read sbyte
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        public sbyte ReadSByte(string fieldName)
        {
            return _reader.ReadSByte();
        }

        /// <summary>
        /// Read byte array
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        public byte[] ReadByteArr(string fieldName)
        {
            byte[]? readed = ReadByteArrN(fieldName);
            if (readed == null)
            {
                throw new DeSerializationException("Readed NULL on expected value!");
            }

            return readed;
        }

        /// <summary>
        /// Read nullable byte array
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        public byte[]? ReadByteArrN(string fieldName)
        {
            int length = _reader.ReadInt32();
            if (length >= 0)
            {
                return _reader.ReadBytes(length);
            }

            return null;
        }

        /// <summary>
        /// Read sbyte array
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        public sbyte[] ReadSByteArr(string fieldName)
        {
            sbyte[]? readed = ReadSByteArrN(fieldName);
            if (readed == null)
            {
                throw new DeSerializationException("Readed NULL on expected value!");
            }

            return readed;
        }

        /// <summary>
        /// Read nullable sbyte array
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        public sbyte[]? ReadSByteArrN(string fieldName)
        {
            int length = _reader.ReadInt32();
            if (length >= 0)
            {
                sbyte[] result = new sbyte[length];
                for (int i = 0; i < length; i++)
                {
                    result[i] = _reader.ReadSByte();
                }

                return result;
            }

            return null;
        }

        /// <summary>
        /// Read UInt16 (ushort)
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        public ushort ReadUInt16(string fieldName)
        {
            return _reader.ReadUInt16();
        }

        /// <summary>
        /// Read Int16 (short)
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        public short ReadInt16(string fieldName)
        {
            return _reader.ReadInt16();
        }

        /// <summary>
        /// Read Int16 (short) array
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        public short[] ReadInt16Arr(string fieldName)
        {
            short[]? readed = ReadInt16ArrN(fieldName);
            if (readed == null)
            {
                throw new DeSerializationException("Readed NULL on expected value!");
            }

            return readed;
        }

        /// <summary>
        /// Read nullable Int16 (short) array
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        public short[]? ReadInt16ArrN(string fieldName)
        {
            int length = _reader.ReadInt32();
            if (length >= 0)
            {
                short[] arr = new short[length];
                for (int i = 0; i < length; i++)
                {
                    arr[i] = _reader.ReadInt16();
                }

                return arr;
            }

            return null;
        }

        /// <summary>
        /// Read float
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        public float ReadFloat(string fieldName)
        {
            return _reader.ReadSingle();
        }

        /// <summary>
        /// Read int32
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        public int ReadInt32(string fieldName)
        {
            return _reader.ReadInt32();
        }

        /// <summary>
        /// Read int64
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        public long ReadInt64(string fieldName)
        {
            return _reader.ReadInt64();
        }

        /// <summary>
        /// Read decimal
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        public decimal ReadDecimal(string fieldName)
        {
            return _reader.ReadDecimal();
        }

        /// <summary>
        /// Read string
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        public string ReadString(string fieldName)
        {
            string? readed = ReadStringN(fieldName);
            if (readed == null)
            {
                throw new DeSerializationException("Readed NULL on expected value!");
            }

            return readed;
        }

        /// <summary>
        /// Read nullable string
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        public string? ReadStringN(string fieldName)
        {
            int length = _reader.ReadInt32();
            if (length < 0)
            {
                return null;
            }

            if (length == 0)
            {
                return string.Empty;
            }

            byte[] array = _reader.ReadBytes(length);
            return Encoding.UTF8.GetString(array);
        }

        /// <summary>
        /// Read Guid
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        public Guid ReadGuid(string fieldName)
        {
            byte[] arr = ReadByteArr(fieldName);
            return new Guid(arr);
        }

        /// <summary>
        /// Read start of a object
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>True - object exist</returns>
        public bool ReadObjectStart(string fieldName)
        {
            byte value = _reader.ReadByte();
            if (value == 0)
            {
                return false;
            }

            if (value == 1)
            {
                return true;
            }

            throw new DeSerializationException("Invalid deserialized object: Start of object expected!");
        }

        /// <summary>
        /// Read end of a object
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="readedObjectType">Runtime typ čteného objektu</param>
        public void ReadObjectEnd(string fieldName, string readedObjectType)
        {
            byte value = _reader.ReadByte();
            if (value != byte.MaxValue)
            {
                throw new DeSerializationException($"Invalid deserialized object: End of object \"{readedObjectType}\" expected!");
            }
        }

        /// <summary>
        /// Read start of a collection
        /// </summary>
        /// <param name="fieldName">Field name</param>
        public int ReadCollectionStart(string fieldName)
        {
            int count = ReadInt32(fieldName);
            return count;
        }

        /// <summary>
        /// Reads if there exist another object in collection to read
        /// </summary>
        /// <param name="count">Expected objects to read</param>
        /// <returns>True - object exist</returns>
        public bool ReadCollectionNextObjExist(int count)
        {
            return count > 0;
        }

        /// <summary>
        /// Read end of a collection
        /// </summary>
        /// <param name="fieldName">Field name</param>
        public void ReadCollectionEnd(string fieldName)
        {
        }

        /// <summary>
        /// Read nullable boolean
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        public bool? ReadBoolN(string fieldName)
        {
            if (ReadValueExist(fieldName))
            {
                return ReadBool(fieldName);
            }

            return null;
        }

        /// <summary>
        /// Read nullable byte
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        public byte? ReadByteN(string fieldName)
        {
            if (ReadValueExist(fieldName))
            {
                return ReadByte(fieldName);
            }

            return null;
        }

        /// <summary>
        /// Read nullable sbyte
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        public sbyte? ReadSByteN(string fieldName)
        {
            if (ReadValueExist(fieldName))
            {
                return ReadSByte(fieldName);
            }

            return null;
        }

        /// <summary>
        /// Read nullable UInt16 (ushort?)
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        public ushort? ReadUInt16N(string fieldName)
        {
            if (ReadValueExist(fieldName))
            {
                return ReadUInt16(fieldName);
            }

            return null;
        }

        /// <summary>
        /// Read nullable Int16 (short)
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        public short? ReadInt16N(string fieldName)
        {
            if (ReadValueExist(fieldName))
            {
                return ReadInt16(fieldName);
            }

            return null;
        }

        /// <summary>
        /// Read nullable float
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        public float? ReadFloatN(string fieldName)
        {
            if (ReadValueExist(fieldName))
            {
                return ReadFloat(fieldName);
            }

            return null;
        }

        /// <summary>
        /// Read nullable int32
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        public int? ReadInt32N(string fieldName)
        {
            if (ReadValueExist(fieldName))
            {
                return ReadInt32(fieldName);
            }

            return null;
        }

        /// <summary>
        /// Read nullable int64
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        public long? ReadInt64N(string fieldName)
        {
            if (ReadValueExist(fieldName))
            {
                return ReadInt64(fieldName);
            }

            return null;
        }

        /// <summary>
        /// Read nullable decimal
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        public decimal? ReadDecimalN(string fieldName)
        {
            if (ReadValueExist(fieldName))
            {
                return ReadDecimal(fieldName);
            }

            return null;
        }

        /// <summary>
        /// Read nullable Guid
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        public Guid? ReadGuidN(string fieldName)
        {
            if (ReadValueExist(fieldName))
            {
                return ReadGuid(fieldName);
            }

            return null;
        }

        /// <summary>
        /// Write existence of value
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Value exist</returns>
        private bool ReadValueExist(string fieldName)
        {
            return ReadBool($"{fieldName}{BinaryWriterProxy.NOT_NULL_FIELD_NAME_POSTFIX}");
        }
    }
}