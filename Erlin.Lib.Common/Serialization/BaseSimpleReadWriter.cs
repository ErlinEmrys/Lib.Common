using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Erlin.Lib.Common.Exceptions;

namespace Erlin.Lib.Common.Serialization
{
    /// <summary>
    /// Base simple type serialization reader/writer
    /// </summary>
    public abstract class BaseSimpleReadWriter : ISimpleReadWriter
    {
        /// <summary>
        /// Release all resources
        /// </summary>
        public virtual void Dispose()
        {
            Reader?.Dispose();
            Writer?.Flush();
            Writer?.Dispose();
        }

        /// <summary>
        /// Optional parameters of read/write
        /// </summary>
        public Dictionary<string, string> Params { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Current writer
        /// </summary>
        public IWriter? Writer { get; protected set; }

        /// <summary>
        /// Current reader
        /// </summary>
        public IReader? Reader { get; protected set; }

        /// <summary>
        /// If true, (de)serializer is currently reading, otherwise is writing
        /// </summary>
        public bool IsRead { get { return !IsWrite; } }

        /// <summary>
        /// If true, (de)serializer is currently writing, otherwise is reading
        /// </summary>
        public bool IsWrite { get { return Writer != null; } }

        /// <summary>
        /// Read/Write bool value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public bool ReadWriteBool(string fieldName, bool value)
        {
            if (IsWrite)
            {
                Writer!.WriteBool(fieldName, value);
            }
            else
            {
                value = Reader!.ReadBool(fieldName);
            }

            return value;
        }

        /// <summary>
        /// Read/Write nullable bool value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public bool? ReadWriteBoolN(string fieldName, bool? value)
        {
            if (IsWrite)
            {
                Writer!.WriteBoolN(fieldName, value);
            }
            else
            {
                value = Reader!.ReadBoolN(fieldName);
            }

            return value;
        }

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public byte ReadWriteByte(string fieldName, byte value)
        {
            if (IsWrite)
            {
                Writer!.WriteByte(fieldName, value);
            }
            else
            {
                value = Reader!.ReadByte(fieldName);
            }

            return value;
        }

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public byte? ReadWriteByteN(string fieldName, byte? value)
        {
            if (IsWrite)
            {
                Writer!.WriteByteN(fieldName, value);
            }
            else
            {
                value = Reader!.ReadByteN(fieldName);
            }

            return value;
        }

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public sbyte ReadWriteSByte(string fieldName, sbyte value)
        {
            if (IsWrite)
            {
                Writer!.WriteSByte(fieldName, value);
            }
            else
            {
                value = Reader!.ReadSByte(fieldName);
            }

            return value;
        }

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public sbyte? ReadWriteSByteN(string fieldName, sbyte? value)
        {
            if (IsWrite)
            {
                Writer!.WriteSByteN(fieldName, value);
            }
            else
            {
                value = Reader!.ReadSByteN(fieldName);
            }

            return value;
        }

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public byte[] ReadWriteByteArr(string fieldName, byte[] value)
        {
            if (IsWrite)
            {
                Writer!.WriteByteArr(fieldName, value);
            }
            else
            {
                value = Reader!.ReadByteArr(fieldName);
            }

            return value;
        }

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public byte[]? ReadWriteByteArrN(string fieldName, byte[]? value)
        {
            if (IsWrite)
            {
                Writer!.WriteByteArrN(fieldName, value);
            }
            else
            {
                value = Reader!.ReadByteArrN(fieldName);
            }

            return value;
        }

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public sbyte[] ReadWriteSByteArr(string fieldName, sbyte[] value)
        {
            if (IsWrite)
            {
                Writer!.WriteSByteArr(fieldName, value);
            }
            else
            {
                value = Reader!.ReadSByteArr(fieldName);
            }

            return value;
        }

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public sbyte[]? ReadWriteSByteArrN(string fieldName, sbyte[]? value)
        {
            if (IsWrite)
            {
                Writer!.WriteSByteArrN(fieldName, value);
            }
            else
            {
                value = Reader!.ReadSByteArrN(fieldName);
            }

            return value;
        }

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public ushort ReadWriteUInt16(string fieldName, ushort value)
        {
            if (IsWrite)
            {
                Writer!.WriteUInt16(fieldName, value);
            }
            else
            {
                value = Reader!.ReadUInt16(fieldName);
            }

            return value;
        }

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public ushort? ReadWriteUInt16N(string fieldName, ushort? value)
        {
            if (IsWrite)
            {
                Writer!.WriteUInt16N(fieldName, value);
            }
            else
            {
                value = Reader!.ReadUInt16N(fieldName);
            }

            return value;
        }

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public short ReadWriteInt16(string fieldName, short value)
        {
            if (IsWrite)
            {
                Writer!.WriteInt16(fieldName, value);
            }
            else
            {
                value = Reader!.ReadInt16(fieldName);
            }

            return value;
        }

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public short? ReadWriteInt16N(string fieldName, short? value)
        {
            if (IsWrite)
            {
                Writer!.WriteInt16N(fieldName, value);
            }
            else
            {
                value = Reader!.ReadInt16N(fieldName);
            }

            return value;
        }

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public short[] ReadWriteInt16Arr(string fieldName, short[] value)
        {
            if (IsWrite)
            {
                Writer!.WriteInt16Arr(fieldName, value);
            }
            else
            {
                value = Reader!.ReadInt16Arr(fieldName);
            }

            return value;
        }

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public short[]? ReadWriteInt16ArrN(string fieldName, short[]? value)
        {
            if (IsWrite)
            {
                Writer!.WriteInt16ArrN(fieldName, value);
            }
            else
            {
                value = Reader!.ReadInt16ArrN(fieldName);
            }

            return value;
        }

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public float ReadWriteFloat(string fieldName, float value)
        {
            if (IsWrite)
            {
                Writer!.WriteFloat(fieldName, value);
            }
            else
            {
                value = Reader!.ReadFloat(fieldName);
            }

            return value;
        }

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public float? ReadWriteFloatN(string fieldName, float? value)
        {
            if (IsWrite)
            {
                Writer!.WriteFloatN(fieldName, value);
            }
            else
            {
                value = Reader!.ReadFloatN(fieldName);
            }

            return value;
        }

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public int ReadWriteInt32(string fieldName, int value)
        {
            if (IsWrite)
            {
                Writer!.WriteInt32(fieldName, value);
            }
            else
            {
                value = Reader!.ReadInt32(fieldName);
            }

            return value;
        }

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public int? ReadWriteInt32N(string fieldName, int? value)
        {
            if (IsWrite)
            {
                Writer!.WriteInt32N(fieldName, value);
            }
            else
            {
                value = Reader!.ReadInt32N(fieldName);
            }

            return value;
        }

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public long ReadWriteInt64(string fieldName, long value)
        {
            if (IsWrite)
            {
                Writer!.WriteInt64(fieldName, value);
            }
            else
            {
                value = Reader!.ReadInt64(fieldName);
            }

            return value;
        }

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public long? ReadWriteInt64N(string fieldName, long? value)
        {
            if (IsWrite)
            {
                Writer!.WriteInt64N(fieldName, value);
            }
            else
            {
                value = Reader!.ReadInt64N(fieldName);
            }

            return value;
        }

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public decimal ReadWriteDecimal(string fieldName, decimal value)
        {
            if (IsWrite)
            {
                Writer!.WriteDecimal(fieldName, value);
            }
            else
            {
                value = Reader!.ReadDecimal(fieldName);
            }

            return value;
        }

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public decimal? ReadWriteDecimalN(string fieldName, decimal? value)
        {
            if (IsWrite)
            {
                Writer!.WriteDecimalN(fieldName, value);
            }
            else
            {
                value = Reader!.ReadDecimalN(fieldName);
            }

            return value;
        }

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public string ReadWriteString(string fieldName, string value)
        {
            if (IsWrite)
            {
                Writer!.WriteString(fieldName, value);
            }
            else
            {
                value = Reader!.ReadString(fieldName);
            }

            return value;
        }

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public string? ReadWriteStringN(string fieldName, string? value)
        {
            if (IsWrite)
            {
                Writer!.WriteStringN(fieldName, value);
            }
            else
            {
                value = Reader!.ReadStringN(fieldName);
            }

            return value;
        }

        /// <summary>
        /// Read/Write DateTime value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public DateTime ReadWriteDateTime(string fieldName, DateTime value)
        {
            if (IsWrite)
            {
                Writer!.WriteInt64(fieldName, value.Ticks);
            }
            else
            {
                long ticks = Reader!.ReadInt64(fieldName);
                value = new DateTime(ticks);
            }

            return value;
        }

        /// <summary>
        /// Read/Write nullable DateTime value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public DateTime? ReadWriteDateTimeN(string fieldName, DateTime? value)
        {
            if (IsWrite)
            {
                Writer!.WriteInt64N(fieldName, value?.Ticks);
            }
            else
            {
                long? ticks = Reader!.ReadInt64N(fieldName);
                value = null;
                if (ticks.HasValue)
                {
                    value = new DateTime(ticks.Value);
                }
            }

            return value;
        }

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public Guid ReadWriteGuid(string fieldName, Guid value)
        {
            if (IsWrite)
            {
                Writer!.WriteGuid(fieldName, value);
            }
            else
            {
                value = Reader!.ReadGuid(fieldName);
            }

            return value;
        }

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public Guid? ReadWriteGuidN(string fieldName, Guid? value)
        {
            if (IsWrite)
            {
                Writer!.WriteGuidN(fieldName, value);
            }
            else
            {
                value = Reader!.ReadGuidN(fieldName);
            }

            return value;
        }

        /// <summary>
        /// Read/Write enum value
        /// </summary>
        /// <typeparam name="TEnum">Runtime type of Enum</typeparam>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public TEnum ReadWriteEnum<TEnum>(string fieldName, TEnum value)
            where TEnum : Enum
        {
            if (IsWrite)
            {
                int numberValue = SimpleConvert.Convert<int>(value);
                Writer!.WriteInt32(fieldName, numberValue);
            }
            else
            {
                int numberValue = Reader!.ReadInt32(fieldName);
                value = SimpleConvert.Convert<TEnum>(numberValue);
            }

            return value;
        }

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public List<string> ReadWriteStringList(string fieldName, List<string> value)
        {
            List<string>? readed = ReadWriteStringListN(fieldName, value);
            if (readed == null)
            {
                throw new DeSerializationException("Readed NULL value on expected object!");
            }

            return readed;
        }

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public List<string>? ReadWriteStringListN(string fieldName, List<string>? value)
        {
            if (IsWrite)
            {
                List<string>? backup = null;
                if (value != null)
                {
                    backup = new List<string>(value);
                }

                int count = backup?.Count ?? -1;
                Writer!.WriteCollectionStart(fieldName, count);
                if (backup != null)
                {
                    for (int i = 0; i < count; i++)
                    {
                        ReadWriteString(GetItemFieldName(i), backup[i]);
                        if (i < count - 1)
                        {
                            Writer!.WriteCollectionObjectSeparator();
                        }
                    }
                }

                Writer!.WriteCollectionEnd(fieldName);

                return value;
            }
            else
            {
                int count = Reader!.ReadCollectionStart(fieldName);
                if (count >= 0)
                {
                    value = new List<string>();
                    int index = 0;
                    while (Reader!.ReadCollectionNextObjExist(count))
                    {
                        count--;
                        string item = ReadWriteString(GetItemFieldName(index++), string.Empty);
                        value.Add(item);
                    }

                    Reader!.ReadCollectionEnd(fieldName);
                }
                else
                {
                    value = null;
                }

                return value;
            }
        }

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public List<byte> ReadWriteByteList(string fieldName, List<byte> value)
        {
            return ReadWriteList(fieldName, value, c => ReadWriteByte(GetItemFieldName(c.ItemIndex), c.Item));
        }

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public List<byte>? ReadWriteByteListN(string fieldName, List<byte>? value)
        {
            return ReadWriteListN(fieldName, value, c => ReadWriteByte(GetItemFieldName(c.ItemIndex), c.Item));
        }

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public List<short> ReadWriteInt16List(string fieldName, List<short> value)
        {
            return ReadWriteList(fieldName, value, c => ReadWriteInt16(GetItemFieldName(c.ItemIndex), c.Item));
        }

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public List<short>? ReadWriteInt16ListN(string fieldName, List<short>? value)
        {
            return ReadWriteListN(fieldName, value, c => ReadWriteInt16(GetItemFieldName(c.ItemIndex), c.Item));
        }

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public List<int> ReadWriteInt32List(string fieldName, List<int> value)
        {
            return ReadWriteList(fieldName, value, c => ReadWriteInt32(GetItemFieldName(c.ItemIndex), c.Item));
        }

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public List<int>? ReadWriteInt32ListN(string fieldName, List<int>? value)
        {
            return ReadWriteListN(fieldName, value, c => ReadWriteInt32(GetItemFieldName(c.ItemIndex), c.Item));
        }

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public List<float> ReadWriteFloatList(string fieldName, List<float> value)
        {
            return ReadWriteList(fieldName, value, c => ReadWriteFloat(GetItemFieldName(c.ItemIndex), c.Item));
        }

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public List<float>? ReadWriteFloatListN(string fieldName, List<float>? value)
        {
            return ReadWriteListN(fieldName, value, c => ReadWriteFloat(GetItemFieldName(c.ItemIndex), c.Item));
        }

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public List<decimal> ReadWriteDecimalList(string fieldName, List<decimal> value)
        {
            return ReadWriteList(fieldName, value, c => ReadWriteDecimal(GetItemFieldName(c.ItemIndex), c.Item));
        }

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public List<decimal>? ReadWriteDecimalListN(string fieldName, List<decimal>? value)
        {
            return ReadWriteListN(fieldName, value, c => ReadWriteDecimal(GetItemFieldName(c.ItemIndex), c.Item));
        }

        /// <summary>
        /// Read/Write object serialization version
        /// </summary>
        /// <param name="version">Serialization version</param>
        /// <returns>Readed serialization version</returns>
        public byte ReadWriteVersion(byte version)
        {
            const string FIELD_NAME = "#serializationVersion";
            if (IsWrite)
            {
                Writer!.WriteByte(FIELD_NAME, version);
            }
            else
            {
                version = Reader!.ReadByte(FIELD_NAME);
            }

            return version;
        }

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <typeparam name="T">Runtime type of de/serializable object</typeparam>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <param name="objectDeSerialization">Method that DeSerialize object</param>
        /// <returns>Readed value</returns>
        public List<T> ReadWriteList<T>(string fieldName, List<T> value, Func<ObjectDeSerializationContext<T>, T> objectDeSerialization)
            where T : new()
        {
            List<T>? readed = ReadWriteListN(fieldName, value, objectDeSerialization);
            if (readed == null)
            {
                throw new DeSerializationException("Readed NULL value on expected object!");
            }

            return readed;
        }

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <typeparam name="T">Runtime type of de/serializable object</typeparam>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <param name="objectDeSerialization">Method that DeSerialize object</param>
        /// <returns>Readed value</returns>
        public List<T>? ReadWriteListN<T>(string fieldName, List<T>? value, Func<ObjectDeSerializationContext<T>, T> objectDeSerialization)
            where T : new()
        {
            if (IsWrite)
            {
                List<T>? backup = null;
                if (value != null)
                {
                    backup = new List<T>(value);
                }

                int count = backup?.Count ?? -1;
                Writer!.WriteCollectionStart(fieldName, count);
                if (backup != null)
                {
                    for (int i = 0; i < count; i++)
                    {
                        objectDeSerialization(new ObjectDeSerializationContext<T>(backup[i], i));
                        if (i < count - 1)
                        {
                            Writer!.WriteCollectionObjectSeparator();
                        }
                    }
                }

                Writer!.WriteCollectionEnd(fieldName);

                return value;
            }
            else
            {
                int count = Reader!.ReadCollectionStart(fieldName);
                if (count >= 0)
                {
                    value = new List<T>();
                    int index = 0;
                    while (Reader!.ReadCollectionNextObjExist(count))
                    {
                        count--;
                        T item = objectDeSerialization(new ObjectDeSerializationContext<T>(new T(), index++));
                        value.Add(item);
                    }

                    Reader!.ReadCollectionEnd(fieldName);
                }
                else
                {
                    value = null;
                }

                return value;
            }
        }

        /// <summary>
        /// Creates field name for collection item
        /// </summary>
        /// <param name="itemIndex">Index of item</param>
        /// <returns>Item field name</returns>
        protected string GetItemFieldName(int itemIndex)
        {
            return $"Item#{itemIndex}";
        }
    }
}