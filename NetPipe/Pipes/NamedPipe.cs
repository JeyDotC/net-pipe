using System;
using System.Collections.Generic;
using System.Text;

namespace NetPipe.Pipes
{
    public sealed class NamedPipe : PipeBase
    {
        private readonly IPipe _pipe;

        public string Name { get; }

        public NamedPipe(IPipe pipe, string name)
        {
            _pipe = pipe;
            Name = name;
        }

        public override void Process(IDictionary<string, object> load)
            => _pipe.Process(load);

        public override string ToString() => $"{Name}[{_pipe}]";
    }

    public static class NamedPipeExtensions
    {
        public static IPipe Named(this IPipe pipe, string name)
            => new NamedPipe(pipe, name);
    }
}
