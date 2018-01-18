using System;
using System.Collections.Generic;
using System.Text;

namespace NetPipe.Pipes
{
    public sealed class DelegateGenericPipe<T> : GenericPipe<T>
    {
        private readonly Action<T> _process;

        public DelegateGenericPipe(Action<T> process) => _process = process;

        protected override void Process(T parameter) => _process(parameter);
    }
}
