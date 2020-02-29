namespace Panelak.Utils
{
    using System;
    public abstract class ServiceResult
    {
        public virtual bool IsError => this is ErrorResult || GetType().IsSubclassOf(typeof(ErrorStatusResult<>));
        public virtual string Message { get; }
        public ServiceResult(string message) => Message = message ?? throw new ArgumentNullException(nameof(message));

        public ServiceResult()
        {

        }
    }
}
