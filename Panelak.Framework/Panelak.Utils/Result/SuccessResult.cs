namespace Panelak.Utils
{
    using System;
    public class SuccessResult : ServiceResult
    {
        public string SuccessMessage { get; }
        public static readonly ServiceResult Success = new SuccessResult("✅");
        public SuccessResult(string successMessage) : base(successMessage) 
            => SuccessMessage = successMessage ?? throw new ArgumentNullException(nameof(successMessage));
    }
}
