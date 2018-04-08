using System;
using System.Collections.Generic;

namespace NetPipe
{
    public class PipeErrorEventArgs : PipeEventArgs
    {
        public Exception Error { get; }

        public ErrorAction ErrorAction { get; set; } = ErrorAction.Rethrow;

        public PipeErrorEventArgs(IPipe pipe, IDictionary<string, object> load, Exception exception) : base(pipe, load)
            => Error = exception;
    }
}