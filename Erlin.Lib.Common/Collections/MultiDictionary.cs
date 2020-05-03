using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erlin.Lib.Common.Collections
{
    /// <summary>
    /// Dictionary for storing multiplae values under same key
    /// </summary>
    /// <typeparam name="TKey">Runtime type of dictionary key</typeparam>
    /// <typeparam name="TValue">Runtime type of dictionary value</typeparam>
    public class MultiDictionary<TKey, TValue>
        where TKey : notnull
    {
        private readonly Dictionary<TKey, List<TValue>> _innerDic = new Dictionary<TKey, List<TValue>>();
        private readonly bool _onlyUniqueValues;

        /// <summary>
        /// All keys collection
        /// </summary>
        public ICollection<TKey> Keys { get { return _innerDic.Keys; } }

        /// <summary>
        /// All List values collection
        /// </summary>
        public ICollection<List<TValue>> Values { get { return _innerDic.Values; } }

        /// <summary>
        /// Key accessor - return all values stored under entered key
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>List of values</returns>
        public List<TValue> this[TKey key]
        {
            get
            {
                if (_innerDic.ContainsKey(key))
                {
                    return _innerDic[key];
                }

                return new List<TValue>();
            }
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="onlyUniqueValues">If true, list under key can contains only distinct values</param>
        public MultiDictionary(bool onlyUniqueValues)
        {
            _onlyUniqueValues = onlyUniqueValues;
        }

        /// <summary>
        /// Add value object under entered key
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        public void Add(TKey key, TValue value)
        {
            if (!_innerDic.ContainsKey(key))
            {
                _innerDic.Add(key, new List<TValue>());
            }

            List<TValue> list = _innerDic[key];
            if (!_onlyUniqueValues || !list.Contains(value))
            {
                list.Add(value);
            }
        }

        /// <summary>
        /// Check if this dictionary contains specific key
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>True - dictionray contains specified key</returns>
        public bool ContainsKey(TKey key)
        {
            return _innerDic.ContainsKey(key);
        }

        /// <summary>
        /// Remove entry by key if exist
        /// </summary>
        /// <param name="key">Key to remove</param>
        /// <returns>True - record existed and was removed</returns>
        public bool Remove(TKey key)
        {
            return _innerDic.Remove(key);
        }
    }
}