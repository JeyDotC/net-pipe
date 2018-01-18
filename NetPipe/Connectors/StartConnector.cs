using System;
using System.Collections.Generic;
using System.Text;

namespace NetPipe.Connectors
{
    public class StartConnector : IConnector
    {
        // The only pipe this connector will care about.
        private IPipe _pipe;

        public void ReceivePipe(IPipe previous, IPipe next)
        {
            if (_pipe != null)
            {
                throw new InvalidOperationException("This connector can only receive one pipe.");
            }

            _pipe = next ?? throw new ArgumentNullException(nameof(next));

            previous.OutputConnector = this;
            next.InputConnector = this;
        }

        public IDictionary<string, object> RunPipes(IDictionary<string, object> load)
            => _pipe?.Process(load) ?? load;
    }
}
