using System;
using System.Collections.Generic;
using System.Text;

namespace NetPipe.Connectors.Extensions
{
    public static class DirectConnectorExtensions
    {
        public static PipeLine Connect(this PipeLine pipeLine)
        {
            pipeLine.Connector(new DirectConnector());
            return pipeLine;
        }
    }
}
