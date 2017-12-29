namespace Masha.Foundation
{
    using System;

    public class Error
    {
        private int errCode;
        private string plainMsg;
        private bool isCodeBasedError;

        public static readonly Error None = new Error(-1);

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

        public int Code => errCode;
        public string Message => plainMsg;
        public bool IsNone => this.Equals(Error.None);

        public static Error Of(int errorCode) => new Error(errorCode);
        public static Error Of(string message) => new Error(message);

        public static Result AsResult(int errCode) => new Result(Error.Of(errCode));
        public static Result AsResult(string message) => new Result(Error.Of(message));

        public override bool Equals(object obj)
        {
            if (obj is Error)
            {
                var errorOther = (Error)obj;

                if (this.Code == errorOther.Code ||
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