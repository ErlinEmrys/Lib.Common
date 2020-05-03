using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erlin.Lib.Common.Serialization
{
    /// <summary>
    /// Interface of any object-acceptable reader/writer
    /// </summary>
    public interface IObjectReadWriter : ISimpleReadWriter
    {
        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <typeparam name="T">Runtime type of de/serializable object</typeparam>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        T ReadWrite<T>(string fieldName, T value)
            where T : class, IDeSerializable, new();

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <typeparam name="T">Runtime type of de/serializable object</typeparam>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <param name="objectCreation">Function of constructing of readed object</param>
        /// <returns>Readed value</returns>
        T ReadWrite<T>(string fieldName, T value, Action<CreatorContex<T>> objectCreation)
            where T : class, IDeSerializable;

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <typeparam name="T">Runtime type of de/serializable object</typeparam>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <param name="objectDeSerialization">Method that DeSerialize object</param>
        /// <returns>Readed value</returns>
        List<T> ReadWriteList<T>(string fieldName, List<T> value, Func<ObjectDeSerializationContext<T>, T> objectDeSerialization)
            where T : new();

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <typeparam name="T">Runtime type of de/serializable object</typeparam>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        List<T> ReadWriteList<T>(string fieldName, List<T> value)
            where T : class, IDeSerializable, new();

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <typeparam name="T">Runtime type of de/serializable object</typeparam>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <param name="objectCreator">Function of constructing of readed object</param>
        /// <returns>Readed value</returns>
        List<T> ReadWriteList<T>(string fieldName, List<T> value, Action<CreatorContex<T>> objectCreator)
            where T : class, IDeSerializable;

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <typeparam name="T">Runtime type of de/serializable object</typeparam>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        T? ReadWriteN<T>(string fieldName, T? value)
            where T : class, IDeSerializable, new();

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <typeparam name="T">Runtime type of de/serializable object</typeparam>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <param name="objectCreation">Function of constructing of readed object</param>
        /// <returns>Readed value</returns>
        T? ReadWriteN<T>(string fieldName, T? value, Action<CreatorContex<T>> objectCreation)
            where T : class, IDeSerializable;

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <typeparam name="T">Runtime type of de/serializable object</typeparam>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <param name="objectDeSerialization">Method that DeSerialize object</param>
        /// <returns>Readed value</returns>
        List<T>? ReadWriteListN<T>(string fieldName, List<T>? value, Func<ObjectDeSerializationContext<T>, T> objectDeSerialization)
            where T : new();

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <typeparam name="T">Runtime type of de/serializable object</typeparam>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        List<T>? ReadWriteListN<T>(string fieldName, List<T>? value)
            where T : class, IDeSerializable, new();

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <typeparam name="T">Runtime type of de/serializable object</typeparam>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <param name="objectCreator">Function of constructing of readed object</param>
        /// <returns>Readed value</returns>
        List<T>? ReadWriteListN<T>(string fieldName, List<T>? value, Action<CreatorContex<T>> objectCreator)
            where T : class, IDeSerializable;
    }
}