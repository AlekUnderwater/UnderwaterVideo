using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace UnderwaterVideo
{
    public class ConcurrentQueue<T> : IProducerConsumerCollection<T>, IEnumerable<T>, ICollection, IEnumerable
    {
        #region Properties & util classes

        class Node
        {
            public T Value;
            public Node Next;
        }

        Node head = new Node();
        Node tail;
        int count;

        public const int defaultMaxSize = 32;
        int maxSize = defaultMaxSize;
        public int MaxSize
        {
            get
            {
                return maxSize;
            }
            set
            {
                if ((value > 0) && (value >= count))
                {
                    maxSize = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("MaxSize");
                }
            }
        }

        #endregion

        #region Constructor

        public ConcurrentQueue()
            : this(defaultMaxSize)
        {
        }

        public ConcurrentQueue(int maxQueueSize)
        {
            MaxSize = maxQueueSize;
            tail = head;
        }

        public ConcurrentQueue(IEnumerable<T> enumerable)
            : this(defaultMaxSize)
        {
            foreach (T item in enumerable)
                Enqueue(item);
        }

        #endregion

        #region Methods & interfaces

        public void Enqueue(T item)
        {
            if (Interlocked.CompareExchange(ref count, maxSize, maxSize) == maxSize)
            {
                T head;
                if (TryDequeue(out head))
                {
                    if (QueueOverrun != null)
                        QueueOverrun.BeginInvoke(head, null, null);
                }
            }

            Interlocked.Increment(ref count);

            Node node = new Node();
            node.Value = item;

            Node oldTail = null;
            Node oldNext = null;

            bool update = false;
            while (!update)
            {
                oldTail = tail;
                oldNext = oldTail.Next;

                // Did tail was already updated ?
                if (tail == oldTail)
                {
                    if (oldNext == null)
                    {
                        // The place is for us
                        update = Interlocked.CompareExchange(ref tail.Next, node, null) == null;
                    }
                    else
                    {
                        // another Thread already used the place so give him a hand by putting tail where it should be
                        Interlocked.CompareExchange(ref tail, oldNext, oldTail);
                    }
                }
            }
            // At this point we added correctly our node, now we have to update tail. If it fails then it will be done by another thread
            Interlocked.CompareExchange(ref tail, node, oldTail);

            if (ElementEnquequed != null)
                ElementEnquequed.BeginInvoke(null, null);
        }

        bool IProducerConsumerCollection<T>.TryAdd(T item)
        {
            Enqueue(item);
            return true;
        }

        public bool TryDequeue(out T value)
        {
            value = default(T);
            bool advanced = false;
            while (!advanced)
            {
                Node oldHead = head;
                Node oldTail = tail;
                Node oldNext = oldHead.Next;

                if (oldHead == head)
                {
                    // Empty case ?
                    if (oldHead == oldTail)
                    {
                        // This should be false then
                        if (oldNext != null)
                        {
                            // If not then the linked list is mal formed, update tail
                            Interlocked.CompareExchange(ref tail, oldNext, oldTail);
                        }
                        value = default(T);
                        return false;
                    }
                    else
                    {
                        value = oldNext.Value;
                        advanced = Interlocked.CompareExchange(ref head, oldNext, oldHead) == oldHead;
                    }
                }
            }

            Interlocked.Decrement(ref count);
            return true;
        }

        public bool TryPeek(out T value)
        {
            if (IsEmpty)
            {
                value = default(T);
                return false;
            }

            Node first = head.Next;
            value = first.Value;
            return true;
        }

        internal void InternalClear()
        {
            count = 0;
            tail = head = new Node();
        }

        public void Clear()
        {
            InternalClear();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)InternalGetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return InternalGetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return InternalGetEnumerator();
        }

        IEnumerator<T> InternalGetEnumerator()
        {
            Node my_head = head;
            while ((my_head = my_head.Next) != null)
            {
                yield return my_head.Value;
            }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            T[] dest = array as T[];
            if (dest == null)
                return;
            CopyTo(dest, index);
        }

        public void CopyTo(T[] dest, int index)
        {
            IEnumerator<T> e = InternalGetEnumerator();
            int i = index;
            while (e.MoveNext())
            {
                dest[i++] = e.Current;
            }
        }

        public T[] ToArray()
        {
            T[] dest = new T[count];
            CopyTo(dest, 0);
            return dest;
        }

        bool ICollection.IsSynchronized
        {
            get { return true; }
        }

        bool IProducerConsumerCollection<T>.TryTake(out T item)
        {
            return TryDequeue(out item);
        }

        object syncRoot = new object();
        object ICollection.SyncRoot
        {
            get { return syncRoot; }
        }

        public int Count
        {
            get
            {
                return count;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return count == 0;
            }
        }

        #endregion

        #region Events

        public delegate void ElementEquequedHandler();
        public ElementEquequedHandler ElementEnquequed;

        public delegate void QueueOverrunHandler(T queueHead);
        public QueueOverrunHandler QueueOverrun;

        #endregion
    }
}

