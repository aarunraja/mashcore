namespace Masha.Foundation
{
    using System;

    public class Error
    {
        private int errCode;
        private string plainMsg;
        private bool isCodeBasedError;

        public static readonly Error None = new Error(-1);
        public static readonly Error SomeError = new Error(0);

        public Error(int errCode)
        {
            this.errCode = errCode;
            isCodeBasedError = true;
        }

        public Error(string message)
        {
            this.plainMsg = message;
            isCodeBasedError = false;
        }

        private void FromAnotherError(Error anotherError)
        {
            this.errCode = anotherError.errCode;
            this.isCodeBasedError = anotherError.isCodeBasedError;
            this.plainMsg = anotherError.plainMsg;
        }

        public int Code => errCode;
        public string Message => plainMsg;
        public bool IsNone => this.Equals(Error.None);

        public static Error Of(int errorCode) => new Error(errorCode);
        public static Error Of(string message) => new Error(message);

        public static Result As(int errCode) => new Result(Error.Of(errCode));
        public static Result As(string message) => new Result(Error.Of(message));
        public static Result As(Error error) => new Result(error);

        public static Result<T> As<T>(int errCode) => new Result<T>(Error.Of(errCode));
        public static Result<T> As<T>(string message) => new Result<T>(Error.Of(message));
        public static Result<T> As<T>(Error error) => new Result<T>(error);

        public override bool Equals(object obj)
        {
            if (obj is Error)
            {
                var errorOther = (Error)obj;

                if (this.Code == errorOther.Code) return true;
                if((!string.IsNullOrEmpty(this.Message) && !string.IsNullOrEmpty(errorOther.Message)) && 
                    this.Message.Equals(errorOther.Message, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            if (isCodeBasedError) return this.errCode.GetHashCode();
            else return this.plainMsg.GetHashCode();
        }
    }
}