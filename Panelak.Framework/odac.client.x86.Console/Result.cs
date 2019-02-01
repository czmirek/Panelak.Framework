namespace odac.client.x86.Console
{
    using System.Collections.Generic;

    public class Result
    {
        public List<string> Columns { get; } = new List<string>();
        public List<Dictionary<string, object>> Data { get; } = new List<Dictionary<string, object>>();
    }
}
