using System;

namespace NetPipe
{
    public class PipeErrorEventArgs : PipeEventArgs
    {
        public Exception Error { get; }

        public ErrorAction ErrorAction { get; set; } = ErrorAction.Rethrow;

        public PipeErrorEventArgs(IPipe pipe, Exception exception) : base(pipe)
            => Error = exception;
    }
}