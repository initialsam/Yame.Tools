﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Yame.Tools.NetCore.SafeCollections
{
    public class SafeList<T> : IList<T>
    {
        private readonly List<T> _list;
        private readonly ReaderWriterLockSlim _locker;
        public int Count => _list.Count;
        public bool IsReadOnly => false;

        public SafeList(List<T> list = null, ReaderWriterLockSlim locker = null)
        {
            _list = list ?? new List<T>();
            _locker = locker ?? new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        }

        public T this[int index]
        {
            get
            {
                _locker.EnterReadLock();
                try
                {
                    var value = _list[index];
                    return value;
                }
                finally
                {
                    _locker.ExitReadLock();
                }
            }
            set
            {
                _locker.EnterWriteLock();
                try
                {
                    _list[index] = value;
                }
                finally
                {
                    _locker.ExitWriteLock();
                }
            }
        }

        public void Add(T item)
        {
            _locker.EnterWriteLock();
            try
            {
                _list.Add(item);
            }
            finally
            {
                _locker.ExitWriteLock();
            }
        }

        public void AddRange(IEnumerable<T> collection)
        {
            _locker.EnterWriteLock();
            try
            {
                _list.AddRange(collection);
            }
            finally
            {
                _locker.ExitWriteLock();
            }
        }

        public void Clear()
        {
            _locker.EnterWriteLock();
            try
            {
                _list.Clear();
            }
            finally
            {
                _locker.ExitWriteLock();
            }
        }

        public bool Contains(T item)
        {
            _locker.EnterReadLock();
            try
            {
                var isContain = _list.Contains(item);
                return isContain;
            }
            finally
            {
                _locker.ExitReadLock();
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _locker.EnterReadLock();
            try
            {
                _list.CopyTo(array, arrayIndex);
            }
            finally
            {
                _locker.ExitReadLock();
            }
        }

        public bool Exists(Predicate<T> match)
        {
            _locker.EnterReadLock();
            try
            {
                return _list.Exists(match);
            }
            finally
            {
                _locker.ExitReadLock();
            }
        }

        public T Find(Predicate<T> match)
        {
            _locker.EnterReadLock();
            try
            {
                return _list.Find(match);
            }
            finally
            {
                _locker.ExitReadLock();
            }
        }

        public SafeList<T> FindAll(Predicate<T> match)
        {
            _locker.EnterReadLock();
            try
            {
                return _list.FindAll(match).ToSafeList();
            }
            finally
            {
                _locker.ExitReadLock();
            }
        }

        public void ForEach(Action<T> action)
        {
            _locker.EnterReadLock();
            try
            {
                _list.ForEach(action);
            }
            finally
            {
                _locker.ExitReadLock();
            }
        }

        public IEnumerator<T> GetEnumerator() => new SafeEnumerator<T>(_list, _locker);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int IndexOf(T item)
        {
            _locker.EnterReadLock();
            try
            {
                var index = _list.IndexOf(item);
                return index;
            }
            finally
            {
                _locker.ExitReadLock();
            }
        }

        public void Insert(int index, T item)
        {
            _locker.EnterWriteLock();
            try
            {
                _list.Insert(index, item);
            }
            finally
            {
                _locker.ExitWriteLock();
            }
        }

        public bool Remove(T item)
        {
            _locker.EnterWriteLock();
            try
            {
                var isSuccuessful = _list.Remove(item);
                return isSuccuessful;
            }
            finally
            {
                _locker.ExitWriteLock();
            }
        }

        public int RemoveAll(Predicate<T> match)
        {
            _locker.EnterWriteLock();
            try
            {
                var numberOfDeletion = _list.RemoveAll(match);
                return numberOfDeletion;
            }
            finally
            {
                _locker.ExitWriteLock();
            }
        }

        public void RemoveAt(int index)
        {
            _locker.EnterWriteLock();
            try
            {
                _list.RemoveAt(index);
            }
            finally
            {
                _locker.ExitWriteLock();
            }
        }
    }
}