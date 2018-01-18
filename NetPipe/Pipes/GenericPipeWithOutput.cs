using System;
using System.Collections.Generic;
using System.Text;

namespace NetPipe.Pipes
{
    public abstract class GenericPipeWithOutput<T, TResult> : PipeBase
    {
        protected string InputKey => typeof(T).Name;

        protected string OutputKey => typeof(TResult).Name;

        protected abstract TResult Process(T parameter);

        public override void Process(IDictionary<string, object> load)
        {
            var parameter = (T)load[InputKey];

            var output = Process(parameter);

            load[OutputKey] = output;
        }
    }
}
