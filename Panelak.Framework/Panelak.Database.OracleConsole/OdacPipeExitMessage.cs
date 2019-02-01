namespace Panelak.Database.OracleConsole
{
    using Newtonsoft.Json;

    /// <summary>
    /// Sends exit message to the named pipe to kill the process
    /// </summary>
    public class OdacPipeExitMessage : OdacPipe
    {
        /// <summary>
        /// Sends exit message to the named pipe to kill the process
        /// </summary>
        public void Exit()
        {
            var msg = new Message() { IsExit = true };
            string serialized = JsonConvert.SerializeObject(msg);
            Writer.WriteLine(serialized);
            Writer.Flush();
        }
    }
}
