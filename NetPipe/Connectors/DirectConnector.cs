using System;
using System.Collections.Generic;
using System.Text;

namespace NetPipe.Connectors
{
    public class DirectConnector : ConnectorBase
    {
        // The only pipe this connector will care about.
        private IPipe _pipe;

        public override void ReceivePipe(IPipe previous, IPipe next)
        {
            if(_pipe != null)
            {
                throw new InvalidOperationException("This connector can only receive one pipe.");
            }

            _pipe = next ?? throw new ArgumentNullException(nameof(next));

            if(previous != null)
            {
                previous.OutputConnector = this;
            }

            next.InputConnector = this;
        }

        public override IConnector RunPipes(IDictionary<string, object> load)
        {
            if (_pipe != null)
            {
                return RunPipe(_pipe, load);
            }
            
            return null;
        }

        public override string ToString() => $" {(_pipe?.ToString())} -> {(_pipe?.OutputConnector?.ToString())}";
    }
}
