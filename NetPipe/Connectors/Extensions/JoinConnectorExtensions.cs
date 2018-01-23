using System;
using System.Collections.Generic;
using System.Text;

namespace NetPipe.Connectors.Extensions
{
    public static class JoinConnectorExtensions
    {
        public static PipeLine Join(this PipeLine pipeLine)
        {
            pipeLine.Connector(new JoinConnector());

            return pipeLine;
        }
    }
}
