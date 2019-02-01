namespace Panelak.Database.OracleConsole
{
    using System;
    using System.IO;
    using System.IO.Pipes;

    /// <summary>
    /// Named pipe.
    /// </summary>
    public abstract class OdacPipe : IDisposable
    {
        /// <summary>
        /// Named pipe reader
        /// </summary>
        protected readonly StreamReader Reader;

        /// <summary>
        /// Named pipe writer
        /// </summary>
        protected readonly StreamWriter Writer;

        /// <summary>
        /// Named pipe
        /// </summary>
        private readonly NamedPipeClientStream namedPipeStream;

        /// <summary>
        /// Initializes a new instance of the <see cref="OdacPipe"/> class.
        /// </summary>
        public OdacPipe()
        {
            namedPipeStream = new NamedPipeClientStream(OdacProcess.PipeName);
            namedPipeStream.Connect();

            Reader = new StreamReader(namedPipeStream);
            Writer = new StreamWriter(namedPipeStream);
        }

        /// <summary>
        /// Disposes the named pipe
        /// </summary>
        public void Dispose() => namedPipeStream.Dispose();
    }
}
