using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
namespace Assets.Scripts.Datas.Utils
{
	[Serializable]
	public abstract class SerializableDictionaryBase
	{
		public abstract class Storage
		{
		}

		protected class Dictionary<TKey, TValue> : System.Collections.Generic.Dictionary<TKey, TValue>
		{
			public Dictionary()
			{
			}

			public Dictionary(IDictionary<TKey, TValue> dict)
				: base(dict)
			{
			}

			public Dictionary(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
			}
		}
	}
	[Serializable]
	public abstract class SerializableDictionaryBase<TKey, TValue, TValueStorage> : SerializableDictionaryBase, IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, ISerializationCallbackReceiver, IDeserializationCallback, ISerializable
	{
		private Dictionary<TKey, TValue> m_dict;

		[SerializeField]
		private TKey[] m_keys;

		[SerializeField]
		private TValueStorage[] m_values;

		public ICollection<TKey> Keys
		{
			get
			{
				return ((IDictionary<TKey, TValue>)m_dict).Keys;
			}
		}

		public ICollection<TValue> Values
		{
			get
			{
				return ((IDictionary<TKey, TValue>)m_dict).Values;
			}
		}

		public int Count
		{
			get
			{
				return ((ICollection<KeyValuePair<TKey, TValue>>)m_dict).Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return ((ICollection<KeyValuePair<TKey, TValue>>)m_dict).IsReadOnly;
			}
		}

		public TValue this[TKey key]
		{
			get
			{
				return ((IDictionary<TKey, TValue>)m_dict)[key];
			}
			set
			{
				((IDictionary<TKey, TValue>)m_dict)[key] = value;
			}
		}

		public bool IsFixedSize
		{
			get
			{
				return ((IDictionary)m_dict).IsFixedSize;
			}
		}

		ICollection IDictionary.Keys
		{
			get
			{
				return ((IDictionary)m_dict).Keys;
			}
		}

		ICollection IDictionary.Values
		{
			get
			{
				return ((IDictionary)m_dict).Values;
			}
		}

		public bool IsSynchronized
		{
			get
			{
				return ((ICollection)m_dict).IsSynchronized;
			}
		}

		public object SyncRoot
		{
			get
			{
				return ((ICollection)m_dict).SyncRoot;
			}
		}

		public object this[object key]
		{
			get
			{
				return ((IDictionary)m_dict)[key];
			}
			set
			{
				((IDictionary)m_dict)[key] = value;
			}
		}

		public SerializableDictionaryBase()
		{
			m_dict = new Dictionary<TKey, TValue>();
		}

		public SerializableDictionaryBase(IDictionary<TKey, TValue> dict)
		{
			m_dict = new Dictionary<TKey, TValue>(dict);
		}

		protected abstract void SetValue(TValueStorage[] storage, int i, TValue value);

		protected abstract TValue GetValue(TValueStorage[] storage, int i);

		public void CopyFrom(IDictionary<TKey, TValue> dict)
		{
			m_dict.Clear();
			foreach (KeyValuePair<TKey, TValue> item in dict)
			{
				m_dict[item.Key] = item.Value;
			}
		}

		public void OnAfterDeserialize()
		{
			if (m_keys != null && m_values != null && m_keys.Length == m_values.Length)
			{
				m_dict.Clear();
				int num = m_keys.Length;
				for (int i = 0; i < num; i++)
				{
					m_dict[m_keys[i]] = GetValue(m_values, i);
				}
				m_keys = null;
				m_values = null;
			}
		}

		public void OnBeforeSerialize()
		{
			int count = m_dict.Count;
			m_keys = new TKey[count];
			m_values = new TValueStorage[count];
			int num = 0;
			foreach (KeyValuePair<TKey, TValue> item in m_dict)
			{
				m_keys[num] = item.Key;
				SetValue(m_values, num, item.Value);
				num++;
			}
		}

		public void Add(TKey key, TValue value)
		{
			if (ContainsKey(key))
			{
				((IDictionary<TKey, TValue>)m_dict)[key] = value;
			}
			else
			{
				((IDictionary<TKey, TValue>)m_dict).Add(key, value);
			}
		}

		public bool ContainsKey(TKey key)
		{
			return ((IDictionary<TKey, TValue>)m_dict).ContainsKey(key);
		}

		public bool Remove(TKey key)
		{
			return ((IDictionary<TKey, TValue>)m_dict).Remove(key);
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			return ((IDictionary<TKey, TValue>)m_dict).TryGetValue(key, out value);
		}

		public TValue TryGet(TKey key)
		{
			TValue value = default(TValue);
			((IDictionary<TKey, TValue>)m_dict).TryGetValue(key, out value);
			return value;
		}

		public void Add(KeyValuePair<TKey, TValue> item)
		{
			((ICollection<KeyValuePair<TKey, TValue>>)m_dict).Add(item);
		}

		public void Clear()
		{
			((ICollection<KeyValuePair<TKey, TValue>>)m_dict).Clear();
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return ((ICollection<KeyValuePair<TKey, TValue>>)m_dict).Contains(item);
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<TKey, TValue>>)m_dict).CopyTo(array, arrayIndex);
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			return ((ICollection<KeyValuePair<TKey, TValue>>)m_dict).Remove(item);
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<TKey, TValue>>)m_dict).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<TKey, TValue>>)m_dict).GetEnumerator();
		}

		public void Add(object key, object value)
		{
			((IDictionary)m_dict).Add(key, value);
		}

		public bool Contains(object key)
		{
			return ((IDictionary)m_dict).Contains(key);
		}

		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return ((IDictionary)m_dict).GetEnumerator();
		}

		public void Remove(object key)
		{
			((IDictionary)m_dict).Remove(key);
		}

		public void CopyTo(Array array, int index)
		{
			((ICollection)m_dict).CopyTo(array, index);
		}

		public void OnDeserialization(object sender)
		{
			((IDeserializationCallback)m_dict).OnDeserialization(sender);
		}

		protected SerializableDictionaryBase(SerializationInfo info, StreamingContext context)
		{
			m_dict = new Dictionary<TKey, TValue>(info, context);
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			((ISerializable)m_dict).GetObjectData(info, context);
		}
	}
}