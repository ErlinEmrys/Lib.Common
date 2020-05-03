using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Erlin.Lib.Common.Exceptions;

namespace Erlin.Lib.Common.Serialization
{
    /// <summary>
    /// Base object-able serialization reader/writer
    /// </summary>
    public abstract class BaseObjectReadWriter : BaseSimpleReadWriter, IObjectReadWriter
    {
        /// <summary>
        /// Cache for stored de/serializable types
        /// </summary>
        protected Dictionary<string, int> TypeCache { get; } = new Dictionary<string, int>();

        /// <summary>
        /// Backward cache for stored de/serializable types
        /// </summary>
        protected Dictionary<int, string> TypeCacheBack { get; } = new Dictionary<int, string>();

        /// <summary>
        /// ID generator for stored de/serialized types
        /// </summary>
        protected int TypeGenID { get; set; }

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <typeparam name="T">Runtime type of de/serializable object</typeparam>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public T ReadWrite<T>(string fieldName, T value)
            where T : class, IDeSerializable, new()
        {
            return ReadWrite(fieldName, value, c =>
                                               {
                                                   if (IsRead)
                                                   {
                                                       c.Instance = new T();
                                                   }
                                               });
        }

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <typeparam name="T">Runtime type of de/serializable object</typeparam>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public T? ReadWriteN<T>(string fieldName, T? value)
            where T : class, IDeSerializable, new()
        {
            return ReadWriteN(fieldName, value, c =>
                                                {
                                                    if (IsRead)
                                                    {
                                                        c.Instance = new T();
                                                    }
                                                });
        }

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <typeparam name="T">Runtime type of de/serializable object</typeparam>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <param name="objectCreation">Function of constructing of readed object</param>
        /// <returns>Readed value</returns>
        public T ReadWrite<T>(string fieldName, T value, Action<CreatorContex<T>> objectCreation)
            where T : class, IDeSerializable
        {
            return ReadWriteObject(fieldName, value, objectCreation);
        }

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <typeparam name="T">Runtime type of de/serializable object</typeparam>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <param name="objectCreation">Function of constructing of readed object</param>
        /// <returns>Readed value</returns>
        public T? ReadWriteN<T>(string fieldName, T? value, Action<CreatorContex<T>> objectCreation)
            where T : class, IDeSerializable
        {
            return ReadWriteObjectN(fieldName, value, objectCreation);
        }

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <typeparam name="T">Runtime type of de/serializable object</typeparam>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public List<T> ReadWriteList<T>(string fieldName, List<T> value)
            where T : class, IDeSerializable, new()
        {
            return ReadWriteList(fieldName, value, c =>
                                                   {
                                                       if (IsRead)
                                                       {
                                                           c.Instance = new T();
                                                       }
                                                   });
        }

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <typeparam name="T">Runtime type of de/serializable object</typeparam>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <returns>Readed value</returns>
        public List<T>? ReadWriteListN<T>(string fieldName, List<T>? value)
            where T : class, IDeSerializable, new()
        {
            return ReadWriteListN(fieldName, value, c =>
                                                    {
                                                        if (IsRead)
                                                        {
                                                            c.Instance = new T();
                                                        }
                                                    });
        }

        /// <summary>
        /// Read/Write value
        /// </summary>
        /// <typeparam name="T">Runtime type of de/serializable object</typeparam>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <param name="objectCreator">Function of constructing of readed object</param>
        /// <returns>Readed value</returns>
        public List<T> ReadWriteList<T>(string fieldName, List<T> value, Action<CreatorContex<T>> objectCreator)
            where T : class, IDeSerializable
        {
            List<T>? readed = ReadWriteListN(fieldName, value, objectCreator);
            if (readed == null)
            {
                throw new DeSerializationException("Readed NULL on expected instance!");
            }

            return readed;
        }

        /// <summary>
        /// Read/Write nullable value
        /// </summary>
        /// <typeparam name="T">Runtime type of de/serializable object</typeparam>
        /// <param name="fieldName">Name of the De/Serialized field</param>
        /// <param name="value">Value to Serialize</param>
        /// <param name="objectCreator">Function of constructing of readed object</param>
        /// <returns>Readed value</returns>
        public List<T>? ReadWriteListN<T>(string fieldName, List<T>? value, Action<CreatorContex<T>> objectCreator)
            where T : class, IDeSerializable
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
                        WriteObjectN(GetItemFieldName(i), backup[i]);
                        if (i < count - 1)
                        {
                            Writer!.WriteCollectionObjectSeparator();
                        }
                    }

                    Writer!.WriteCollectionEnd(fieldName);
                }

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
                        T? item = ReadObjectN(GetItemFieldName(index++), objectCreator);
                        if (item == null)
                        {
                            throw new DeSerializationException("Readed NULL on expected object!");
                        }

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
        /// Write table of used runtime types to writer
        /// </summary>
        /// <param name="typeTableWriter">Writer</param>
        public void WriteTypeTable(IWriter typeTableWriter)
        {
            typeTableWriter.WriteByte("#typeTableSerializationVersion", 0);
            typeTableWriter.WriteInt32("#typeTableCount", TypeCache.Count);
            int itemNumber = 0;
            foreach ((string fTypeName, int fTypeID) in TypeCache)
            {
                typeTableWriter.WriteString($"#typeTable_Item_{itemNumber}", fTypeName);
                typeTableWriter.WriteInt32($"#typeTable_Item_ID_{itemNumber++}", fTypeID);
            }
        }

        /// <summary>
        /// Reads table of used runtime types from reader
        /// </summary>
        /// <param name="typeTableReader">Reader</param>
        public void ReadTypeTable(IReader typeTableReader)
        {
            TypeCache.Clear();
            TypeCacheBack.Clear();

            // ReSharper disable UnusedVariable
            byte typeTableVersion = typeTableReader.ReadByte("#typeTableSerializationVersion");

            // ReSharper restore UnusedVariable
            int typeTableCount = typeTableReader.ReadInt32("#typeTableCount");
            for (int i = 0; i < typeTableCount; i++)
            {
                string key = $"#typeTable_Item_{i}";
                string typeFullName = typeTableReader.ReadString(key);
                int typeID = typeTableReader.ReadInt32($"#typeTable_Item_ID_{i}");
                TypeCache.Add(typeFullName, typeID);
                TypeCacheBack.Add(typeID, typeFullName);
            }
        }

        /// <summary>
        /// Read/Write object
        /// </summary>
        /// <typeparam name="T">Runtime type of object</typeparam>
        /// <param name="fieldName">Serialization field name</param>
        /// <param name="value">Object to Serialize</param>
        /// <param name="objectCreator">Function of constructing of readed object</param>
        /// <returns>Readed value</returns>
        private T ReadWriteObject<T>(string fieldName, T value, Action<CreatorContex<T>> objectCreator)
            where T : class, IDeSerializable
        {
            T? readed = ReadWriteObjectN(fieldName, value, objectCreator);
            if (readed == null)
            {
                throw new DeSerializationException("Readed NULL on expected instance!");
            }

            return readed;
        }

        /// <summary>
        /// Read/Write nullable object
        /// </summary>
        /// <typeparam name="T">Runtime type of object</typeparam>
        /// <param name="fieldName">Serialization field name</param>
        /// <param name="value">Object to Serialize</param>
        /// <param name="objectCreator">Function of constructing of readed object</param>
        /// <returns>Readed value</returns>
        private T? ReadWriteObjectN<T>(string fieldName, T? value, Action<CreatorContex<T>> objectCreator)
            where T : class, IDeSerializable
        {
            if (IsWrite)
            {
                WriteObjectN(fieldName, value);
            }
            else
            {
                value = ReadObjectN(fieldName, objectCreator);
            }

            return value;
        }

        /// <summary>
        /// Write object
        /// </summary>
        /// <typeparam name="T">Runtime type of object</typeparam>
        /// <param name="fieldName">Serialization field name</param>
        /// <param name="value">Value to write</param>
        private void WriteObjectN<T>(string fieldName, T? value)
            where T : class, IDeSerializable
        {
            if (value == null)
            {
                Writer!.WriteObjectEmpty(fieldName);
            }
            else
            {
                Writer!.WriteObjectStart(fieldName);
                int typeID = GetObjectTypeID(value.GetType().FullName);
                Writer!.WriteInt32("#obj_type", typeID);
                value.DeSerialize(this);
                Writer!.WriteObjectEnd(fieldName);
            }
        }

        /// <summary>
        /// Read object
        /// </summary>
        /// <typeparam name="T">Runtime type of object</typeparam>
        /// <param name="fieldName">Serialization field name</param>
        /// <param name="objectCreator">Function of constructing of readed object</param>
        /// <returns>Readed object</returns>
        private T? ReadObjectN<T>(string fieldName, Action<CreatorContex<T>> objectCreator)
            where T : class, IDeSerializable
        {
            bool objectExist = Reader!.ReadObjectStart(fieldName);
            if (objectExist)
            {
                int typeID = Reader!.ReadInt32("#obj_type");
                CreatorContex<T> context = new CreatorContex<T>(GetObjectTypeByID(typeID));
                objectCreator(context);
                T? value = context.Instance;
                if (value == null)
                {
                    throw new DeSerializationException("Object creator did not returned instance of a deserialization object!");
                }

                value.DeSerialize(this);
                Reader!.ReadObjectEnd(fieldName, context.TypeName);
                return value;
            }

            return null;
        }

        /// <summary>
        /// Converts runtime type full name to numeric ID
        /// </summary>
        /// <param name="typeFullName">Runtime type full name</param>
        /// <returns>Object type ID</returns>
        private int GetObjectTypeID(string? typeFullName)
        {
            if (typeFullName == null)
            {
                throw new ArgumentNullException(nameof(typeFullName));
            }

            if (!TypeCache.ContainsKey(typeFullName))
            {
                TypeCache.Add(typeFullName, ++TypeGenID);
                TypeCacheBack.Add(TypeGenID, typeFullName);
            }

            return TypeCache[typeFullName];
        }

        /// <summary>
        /// Converts object type ID to runtime type full name
        /// </summary>
        /// <param name="objectTypeID">Object type ID</param>
        /// <returns>Runtime type full name</returns>
        private string GetObjectTypeByID(int objectTypeID)
        {
            if (!TypeCacheBack.ContainsKey(objectTypeID))
            {
                throw new InvalidOperationException($"Unknown object type ID: {objectTypeID}");
            }

            return TypeCacheBack[objectTypeID];
        }
    }
}