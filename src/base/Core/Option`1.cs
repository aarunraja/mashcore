namespace Masha.Foundation
{
    using System;
    using System.Collections.Generic;
    using static Core;

    public class Option<T> : IEquatable<NoneObject>
    {
        private bool hasSome;

        public T Value { get; }

        internal Option(T value, bool hasSome)
        {
            this.Value = value;
            this.hasSome = hasSome;
        }
        internal Option(T value)
            : this(value, value != null) { }

        internal Option()
            : this(default(T)) { }

        //TODO: Ensure - this can be a private field
        public bool HasSome
        {
            get { return hasSome; }
        }
        public static readonly Option<T> None = new Option<T>(default(T), false);

        public static implicit operator Option<T>(T value) => Some<T>(value);
        public static implicit operator Option<T>(NoneObject none) => None;

        public bool Equals(NoneObject other)
        {            
            return !hasSome;
        }

        public IEnumerable<T> AsEnumerable()
        {
            if (hasSome) yield return Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Option<T>)) return false;
            var optOther = (Option<T>)obj;
            if (hasSome && optOther.hasSome)
            {
                return this.Value.Equals(optOther.Value);
            }
            else if (!hasSome && !optOther.hasSome)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            if (hasSome) return Value.GetHashCode();
            else return base.GetHashCode();
        }
    }
}