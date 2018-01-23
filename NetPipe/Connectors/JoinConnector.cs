using System;
using System.Collections.Generic;
using System.Text;

namespace NetPipe.Connectors
{
    public class JoinConnector : ConnectorBase
    {
        // The only pipe this connector will care about.
        private IPipe _pipe;

        public void ConnectPrevious(IPipe previous) => ReceivePipe(previous, null);

        public void JoinInto(IPipe next) => ReceivePipe(null, next);

        public override void ReceivePipe(IPipe previous, IPipe next)
        {
            if (_pipe != null && next != null && next != _pipe)
            {
                throw new InvalidOperationException("This connector can only receive one pipe.");
            }

            if (_pipe == null && next != null)
            {
                _pipe = next;
                next.InputConnector = this;
            }

            if (previous != null)
            {
                previous.OutputConnector = this;
            }
        }

        public override IConnector RunPipes(IDictionary<string, object> load)
        {
            if (_pipe != null)
            {
                return RunPipe(_pipe, load);
            }
            return _pipe?.OutputConnector;
        }
        public override string ToString() => $" {(_pipe?.ToString())} -> {(_pipe?.OutputConnector?.ToString())}";
    }
}
