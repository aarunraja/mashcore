namespace Masha.Foundation
{
    using System;

    public class Result<T>
    {
        internal readonly Error error;
        internal readonly T value;

        public bool HasValue {get;}
        public bool HasError => !this.HasValue; 

        public Result(T value)
        {
            this.value = value;
            this.error = Error.None;
            this.HasValue = true;
        }

        public Result(Error error)
        {
            this.error = error;
            this.value = default(T);
            this.HasValue = false;
        }

        public static implicit operator Result<T>(T value) => new Result<T>(value);
        public static implicit operator Result<T>(Error error) => new Result<T>(error);
        public static implicit operator Result<T>(Option<T> option) =>
            option.Match(Some: v => new Result<T>(v), None: () => new Result<T>(Error.SomeError));

        // override object.Equals
        public override bool Equals(object obj)
        {            
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            var tryOther = (Result<T>) obj;
            if(HasValue && tryOther.HasValue)
            {
                return this.value.Equals(tryOther.value);
            }else if(HasError && tryOther.HasError)
            {
                return this.error.Equals(tryOther.error);
            }
            return false;
        }
        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            if(HasValue) return value.GetHashCode();
            else return error.GetHashCode();
        }
    }
}