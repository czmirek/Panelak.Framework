namespace odac.client.x86.Console
{
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.IO.Pipes;
    using System.Threading;

    /// <summary>
    /// Program instance of the ODAC console listener
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Runs the main console listener
        /// </summary>
        /// <param name="args">Console arguments</param>
        [STAThread]
        public static void Main(string[] args)
        {
            //run only once
            bool createdNew = true;
            using (var mutex = new Mutex(true, "odac.client.x86.mutex", out createdNew))
            {
                if (!createdNew)
                    return;

                RunServer();
            }
        }

        /// <summary>
        /// Starts listening on the named pipe
        /// </summary>
        private static void RunServer()
        {
            var server = new NamedPipeServerStream("odac.client.x86.pipe");
            server.WaitForConnection();

            var reader = new StreamReader(server);
            var writer = new StreamWriter(server);

            while (true)
            {
                var str = reader.ReadLine();

                if (String.IsNullOrEmpty(str))
                    continue;

                Message msg = JsonConvert.DeserializeObject<Message>(str);

                if (msg.IsExit)
                    return;

                var oracleQuery = new OracleQuery();
                string result = oracleQuery.GetResult(msg.ConnectionString, msg.Query, msg.Parameters);
                writer.WriteLine(result);
                writer.Flush();
            }
        }
    }
}
