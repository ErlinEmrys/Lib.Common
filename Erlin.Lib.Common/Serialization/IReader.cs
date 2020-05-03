using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erlin.Lib.Common.Serialization
{
    /// <summary>
    /// Any serializable reader
    /// </summary>
    public interface IReader : IDisposable
    {
        /// <summary>
        /// Read boolean
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        bool ReadBool(string fieldName);

        /// <summary>
        /// Read nullable boolean
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        bool? ReadBoolN(string fieldName);

        /// <summary>
        /// Read byte
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        byte ReadByte(string fieldName);

        /// <summary>
        /// Read nullable byte
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        byte? ReadByteN(string fieldName);

        /// <summary>
        /// Read sbyte
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        sbyte ReadSByte(string fieldName);

        /// <summary>
        /// Read nullable sbyte
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        sbyte? ReadSByteN(string fieldName);

        /// <summary>
        /// Read byte array
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        byte[] ReadByteArr(string fieldName);

        /// <summary>
        /// Read nullable byte array
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        byte[]? ReadByteArrN(string fieldName);

        /// <summary>
        /// Read sbyte array
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        sbyte[] ReadSByteArr(string fieldName);

        /// <summary>
        /// Read nullable sbyte array
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        sbyte[]? ReadSByteArrN(string fieldName);

        /// <summary>
        /// Read UInt16 (ushort)
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        ushort ReadUInt16(string fieldName);

        /// <summary>
        /// Read nullable UInt16 (ushort?)
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        ushort? ReadUInt16N(string fieldName);

        /// <summary>
        /// Read Int16 (short)
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        short ReadInt16(string fieldName);

        /// <summary>
        /// Read nullable Int16 (short)
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        short? ReadInt16N(string fieldName);

        /// <summary>
        /// Read Int16 (short) array
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        short[] ReadInt16Arr(string fieldName);

        /// <summary>
        /// Read nullable Int16 (short) array
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        short[]? ReadInt16ArrN(string fieldName);

        /// <summary>
        /// Read float
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        float ReadFloat(string fieldName);

        /// <summary>
        /// Read nullable float
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        float? ReadFloatN(string fieldName);

        /// <summary>
        /// Read int32
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        int ReadInt32(string fieldName);

        /// <summary>
        /// Read nullable int32
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        int? ReadInt32N(string fieldName);

        /// <summary>
        /// Read int64
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        long ReadInt64(string fieldName);

        /// <summary>
        /// Read nullable int64
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        long? ReadInt64N(string fieldName);

        /// <summary>
        /// Read decimal
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        decimal ReadDecimal(string fieldName);

        /// <summary>
        /// Read nullable decimal
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        decimal? ReadDecimalN(string fieldName);

        /// <summary>
        /// Read string
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        string ReadString(string fieldName);

        /// <summary>
        /// Read nullable string
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        string? ReadStringN(string fieldName);

        /// <summary>
        /// Read Guid
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        Guid ReadGuid(string fieldName);

        /// <summary>
        /// Read nullable Guid
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Readed value</returns>
        Guid? ReadGuidN(string fieldName);

        /// <summary>
        /// Read start of a object
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>True - object exist</returns>
        bool ReadObjectStart(string fieldName);

        /// <summary>
        /// Read end of a object
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <param name="readedObjectType">Runtime typ čteného objektu</param>
        void ReadObjectEnd(string fieldName, string readedObjectType);

        /// <summary>
        /// Read start of a collection
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns>Collection counts of objects: -1 for no collection</returns>
        int ReadCollectionStart(string fieldName);

        /// <summary>
        /// Reads if there exist another object in collection to read
        /// </summary>
        /// <param name="count">Expected objects to read</param>
        /// <returns>True - object exist</returns>
        bool ReadCollectionNextObjExist(int count);

        /// <summary>
        /// Read end of a collection
        /// </summary>
        /// <param name="fieldName">Field name</param>
        void ReadCollectionEnd(string fieldName);
    }
}