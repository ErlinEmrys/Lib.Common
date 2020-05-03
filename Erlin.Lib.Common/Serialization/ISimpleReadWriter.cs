using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erlin.Lib.Common.Serialization
{
    /// <summary>
    /// Interface of any simple (non-object) reader/writer
    /// </summary>
    public interface ISimpleReadWriter : IDisposable
    {
        /// <summary>
        /// Optional parameters of read/write
        /// </summary>
        Dictionary<string, string> Params { get; }

        /// <summary>
        /// Inner writer
        /// </summary>
        IWriter? Writer { get; }

        /// <summary>
        /// Inner reader
        /// </summary>
        IReader? Reader { get; }

        /// <summary>
        /// If true, (de)serializer is currently reading, otherwise is writing
        /// </summary>
        bool IsRead { get; }

        /// <summary>
        /// If true, (de)serializer is currently writing, otherwise is reading
        /// </summary>
        bool IsWrite { get; }

        /// <summary>
        /// Read/Write object serialization version
        /// </summary>
        /// <param name="version">Serialization version</param>
        /// <returns>Readed serialization version</returns>
        byte ReadWriteVersion(byte version);

        /// <summary>
        /// Read/Write bool value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        bool ReadWriteBool(string fieldName, bool value);

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        byte ReadWriteByte(string fieldName, byte value);

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        sbyte ReadWriteSByte(string fieldName, sbyte value);

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        byte[] ReadWriteByteArr(string fieldName, byte[] value);

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        sbyte[] ReadWriteSByteArr(string fieldName, sbyte[] value);

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        ushort ReadWriteUInt16(string fieldName, ushort value);

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        short ReadWriteInt16(string fieldName, short value);

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        short[] ReadWriteInt16Arr(string fieldName, short[] value);

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        float ReadWriteFloat(string fieldName, float value);

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        int ReadWriteInt32(string fieldName, int value);

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        long ReadWriteInt64(string fieldName, long value);

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        decimal ReadWriteDecimal(string fieldName, decimal value);

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        string ReadWriteString(string fieldName, string value);

        /// <summary>
        /// Read/Write DateTime value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        DateTime ReadWriteDateTime(string fieldName, DateTime value);

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        Guid ReadWriteGuid(string fieldName, Guid value);

        /// <summary>
        /// Read/Write enum value
        /// </summary>
        /// <typeparam name="TEnum">Runtime type of Enum</typeparam>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        TEnum ReadWriteEnum<TEnum>(string fieldName, TEnum value)
            where TEnum : Enum;

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        List<string> ReadWriteStringList(string fieldName, List<string> value);

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        List<byte> ReadWriteByteList(string fieldName, List<byte> value);

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        List<short> ReadWriteInt16List(string fieldName, List<short> value);

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        List<int> ReadWriteInt32List(string fieldName, List<int> value);

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        List<float> ReadWriteFloatList(string fieldName, List<float> value);

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        List<decimal> ReadWriteDecimalList(string fieldName, List<decimal> value);

        /// <summary>
        /// Read/Write nullable bool value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        bool? ReadWriteBoolN(string fieldName, bool? value);

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        byte? ReadWriteByteN(string fieldName, byte? value);

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        sbyte? ReadWriteSByteN(string fieldName, sbyte? value);

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        byte[]? ReadWriteByteArrN(string fieldName, byte[]? value);

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        sbyte[]? ReadWriteSByteArrN(string fieldName, sbyte[]? value);

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        ushort? ReadWriteUInt16N(string fieldName, ushort? value);

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        short? ReadWriteInt16N(string fieldName, short? value);

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        short[]? ReadWriteInt16ArrN(string fieldName, short[]? value);

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        float? ReadWriteFloatN(string fieldName, float? value);

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        int? ReadWriteInt32N(string fieldName, int? value);

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        long? ReadWriteInt64N(string fieldName, long? value);

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        decimal? ReadWriteDecimalN(string fieldName, decimal? value);

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        string? ReadWriteStringN(string fieldName, string? value);

        /// <summary>
        /// Read/Write nullable DateTime value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        DateTime? ReadWriteDateTimeN(string fieldName, DateTime? value);

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        Guid? ReadWriteGuidN(string fieldName, Guid? value);

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        List<string>? ReadWriteStringListN(string fieldName, List<string>? value);

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        List<byte>? ReadWriteByteListN(string fieldName, List<byte>? value);

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        List<short>? ReadWriteInt16ListN(string fieldName, List<short>? value);

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        List<int>? ReadWriteInt32ListN(string fieldName, List<int>? value);

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        List<float>? ReadWriteFloatListN(string fieldName, List<float>? value);

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        List<decimal>? ReadWriteDecimalListN(string fieldName, List<decimal>? value);
    }
}