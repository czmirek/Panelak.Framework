namespace Panelak.Database.OracleConsole
{
    using System.Diagnostics;

    /// <summary>
    /// Instance of the executable process
    /// </summary>
    public class OdacProcess
    {
        /// <summary>
        /// Pipe name
        /// </summary>
        public const string PipeName = "odac.client.x86.pipe";

        /// <summary>
        /// Executable path
        /// </summary>
        public const string Executable = "odac.client.x86.Console.exe";

        /// <summary>
        /// Process properties
        /// </summary>
        private static readonly ProcessStartInfo ProcessStartInfo = new ProcessStartInfo()
        {
            FileName = Executable,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };

        /// <summary>
        /// Process instance
        /// </summary>
        private static Process odacProcess;

        /// <summary>
        /// Starts the executable which immediately starts listening to the named pipe.
        /// </summary>
        public void Start() => odacProcess = Process.Start(ProcessStartInfo);

        /// <summary>
        /// Sends the exit message to the executable.
        /// </summary>
        public void End()
        {
            var exitMsg = new OdacPipeExitMessage();
            exitMsg.Exit();
        }
    }
}
