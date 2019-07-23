namespace Masha.Foundation
{
    using System;

    public class Return
    {
        internal readonly Error error;

        public bool HasValue {get;}
        public bool HasError => !this.HasValue; 

        public Return(Error error)
        {
            this.error = error;
            this.HasValue = error == Error.None;
        }

        public static implicit operator Return(Error error) => new Return(error);

        // override object.Equals
        public override bool Equals(object obj)
        {            
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            var tryOther = (Return) obj;
            if(HasValue && tryOther.HasValue)
            {
                return true;
            }else if(HasError && tryOther.HasError)
            {
                return this.error.Equals(tryOther.error);
            }
            return false;
        }
        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            return error.GetHashCode();
        }
    }
}