using System;
using System.Collections.Generic;
using System.Text;

namespace NetPipe.Pipes
{
    public sealed class DelegateGenericPipeWithOutput<T, TResult> : GenericPipeWithOutput<T, TResult>
    {
        private readonly Func<T, TResult> _process;

        public DelegateGenericPipeWithOutput(Func<T, TResult> process) => _process = process;

        protected override TResult Process(T parameter) => _process(parameter);
    }
}
