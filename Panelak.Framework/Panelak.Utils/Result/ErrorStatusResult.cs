namespace Panelak.Utils
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    public class ErrorStatusResult<T> : StatusResult<T> where T : struct
    {
        public IReadOnlyDictionary<string, T> Errors => new ReadOnlyDictionary<string, T>(errors);
        private readonly Dictionary<string, T> errors = new Dictionary<string, T>();

        public ErrorStatusResult(string key, T status) : base(status) => AddError(key, status);

        public void AddError(string key, T value) => errors[key] = value;
        public T this[string key]
        {
            get => errors[key];
            set => errors[key] = value;
        }

        public static implicit operator Dictionary<string, T>(ErrorStatusResult<T> serviceResult) => new Dictionary<string, T>(serviceResult.Errors);

        public static ErrorStatusResult<T> Create(string key, T status) => new ErrorStatusResult<T>(key, status);
    }
}
