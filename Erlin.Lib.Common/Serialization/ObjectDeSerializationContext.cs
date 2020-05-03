using System;
using System.Collections.Generic;
using System.Text;

namespace Erlin.Lib.Common.Serialization
{
    /// <summary>
    /// Context of DeSerializing of item in collection
    /// </summary>
    public class ObjectDeSerializationContext<T>
    {
        /// <summary>
        /// DeSerialized item
        /// </summary>
        public T Item { get; }

        /// <summary>
        /// Item index in collection
        /// </summary>
        public int ItemIndex { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="item">DeSerialized item</param>
        /// <param name="itemIndex">Item index in collection</param>
        public ObjectDeSerializationContext(T item, int itemIndex)
        {
            Item = item;
            ItemIndex = itemIndex;
        }
    }
}
