namespace Panelak.Utils
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    public class ErrorResult : ServiceResult
    {
        public IReadOnlyDictionary<string, string> Errors => new ReadOnlyDictionary<string, string>(errors);
        private readonly Dictionary<string, string> errors = new Dictionary<string, string>();

        public ErrorResult(string key, string value) : base(value) => AddError(key, value);

        public void AddError(string key, string value) => errors[key] = value;
        public string this[string key]
        {
            get => errors[key];
            set => errors[key] = value;
        }

        public static implicit operator Dictionary<string, string>(ErrorResult serviceResult) => new Dictionary<string, string>(serviceResult.Errors);
        public static ErrorResult Create(string key, string text)
        {
            return new ErrorResult(key, text);
        }
    }
}
