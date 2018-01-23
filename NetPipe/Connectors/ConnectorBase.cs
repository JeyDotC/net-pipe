using System;
using System.Collections.Generic;
using System.Text;

namespace NetPipe.Connectors
{
    public abstract class ConnectorBase : IConnector
    {
        public event EventHandler<BeforePipeRunEventArgs> BeforePipeRun;
        public event EventHandler<PipeSuccessEventArgs> PipeSuccess;
        public event EventHandler<PipeErrorEventArgs> PipeError;

        protected IConnector RunPipe(IPipe pipe, IDictionary<string, object> dictionary)
        {
            BeforePipeRun?.Invoke(this, new BeforePipeRunEventArgs(pipe));

            try
            {
                pipe.Process(dictionary);
                PipeSuccess?.Invoke(this, new PipeSuccessEventArgs(pipe));
            }
            catch (Exception ex)
            {
                var errorEventArgs = new PipeErrorEventArgs(pipe, ex);

                PipeError?.Invoke(this, errorEventArgs);

                if (errorEventArgs.ErrorAction == ErrorAction.Rethrow)
                {
                    throw;
                }
            }

            return pipe.OutputConnector;
        }

        public abstract void ReceivePipe(IPipe previous, IPipe next);

        public abstract IConnector RunPipes(IDictionary<string, object> load);
    }
}
