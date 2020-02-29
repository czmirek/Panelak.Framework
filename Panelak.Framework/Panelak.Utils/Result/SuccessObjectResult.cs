namespace Panelak.Utils
{
    using System;
    public class SuccessReturnObjectResult : SuccessResult
    {
        public object ReturnObject { get; }
        public SuccessReturnObjectResult(object returnObject) : this("✅", returnObject) { }
        public SuccessReturnObjectResult(string successMessage, object returnObject) : base(successMessage) 
            => ReturnObject = returnObject ?? throw new ArgumentNullException(nameof(returnObject));
    }

    public class SuccessReturnObjectResult<T> : SuccessResult
    {
        public T ReturnObject { get; }
        public SuccessReturnObjectResult(T returnObject) : this("✅", returnObject) { }
        public SuccessReturnObjectResult(string successMessage, T returnObject) : base(successMessage)
            => ReturnObject = returnObject ?? throw new ArgumentNullException(nameof(returnObject));
    }
}
