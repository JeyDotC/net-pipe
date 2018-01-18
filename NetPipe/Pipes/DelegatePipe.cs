using System;
using System.Collections.Generic;
using System.Text;

namespace NetPipe.Pipes
{
    public sealed class DelegatePipe : PipeBase
    {
        private readonly Action<IDictionary<string, object>> _process;

        public DelegatePipe(Action<IDictionary<string, object>> process) => _process = process;

        public override void Process(IDictionary<string, object> parameter) => _process(parameter);
    }
}
