namespace Panelak.Utils
{
    public abstract class StatusResult<T> : ServiceResult where T : struct
    {
        public StatusResult(T status) => Status = status;
        public T Status { get; }
    }
}
