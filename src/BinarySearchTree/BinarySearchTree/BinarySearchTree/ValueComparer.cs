using System;

namespace BinarySearchTree
{
    public class ValueComparer<T> where T : IComparable<T>
    {
        public ValueComparer(T newValue)
        {
            NewValue = newValue;
        }

        enum Equality
        {
            Less = -1,
            Equal = 0,
            Greater = 1
        }

        public void CompareTo(
            T oldValue,
            Action less,
            Action greater,
            Action equal = null
        )
        {
            var equals = ((Equality)NewValue.CompareTo(oldValue));
            if (equals == Equality.Less)
            {
                less?.Invoke();
            }
            else if (equals == Equality.Greater)
            {
                greater?.Invoke();
            }
            else
            {
                // Nothing to do
                equal?.Invoke();
            }
        }

        public TOutput CompareTo<TOutput>(
            T oldValue,
            Func<TOutput> less,
            Func<TOutput> greater,
            Func<TOutput> equal = null
        )
        {
            Func<TOutput> invokable = () => default;
            var equals = ((Equality)NewValue.CompareTo(oldValue));
            if (equals == Equality.Less)
            {
                invokable = less;
            }
            else if (equals == Equality.Greater)
            {
                invokable = greater;
            }
            else
            {
                invokable = equal;
            }

            return invokable != null
                ? invokable.Invoke()
                : default;
        }

        internal void CompareTo(object value, object le)
        {
            throw new NotImplementedException();
        }

        public T NewValue { get; }
    }
}
