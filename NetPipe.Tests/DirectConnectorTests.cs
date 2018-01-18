using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetPipe.Connectors;
using NetPipe.Pipes;
using System;
using System.Collections.Generic;

namespace NetPipe.Tests
{
    [TestClass]
    public class DirectConnectorTests
    {
        [TestMethod]
        public void DirectConnectorTests_BasicConnectPipes()
        {
            var pipe1 = new DelegatePipe(load => load["pipe1"] = true);
            var pipe2 = new DelegatePipe(load => load["pipe2"] = true);
            var pipe3 = new DelegatePipe(load => load["pipe3"] = true);

            var startConnector = new DirectConnector();
            var connector1 = new DirectConnector();
            var connector2 = new DirectConnector();

            var testLoad = new Dictionary<string, object>();

            startConnector.ReceivePipe(null, pipe1);
            connector1.ReceivePipe(pipe1, pipe2);
            connector2.ReceivePipe(pipe2, pipe3);

            Assert.AreEqual(startConnector, pipe1.InputConnector);
            Assert.AreEqual(connector1, pipe1.OutputConnector);
            Assert.AreEqual(connector1, pipe2.InputConnector);
            Assert.AreEqual(connector2, pipe2.OutputConnector);
            Assert.AreEqual(connector2, pipe3.InputConnector);
            Assert.IsNull(pipe3.OutputConnector);

            startConnector
                .RunPipes(testLoad)  // pipe1
                .RunPipes(testLoad)  // pipe2
                .RunPipes(testLoad); // pipe3

            Assert.AreEqual(testLoad["pipe1"], true);
            Assert.AreEqual(testLoad["pipe2"], true);
            Assert.AreEqual(testLoad["pipe3"], true);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DirectConnectorTests_ThrowIfAttemptingToConnectMoreThanOnePipeToThisConnector()
        {
            var pipe1 = new DelegatePipe(load => load["pipe1"] = true);
            var pipe2 = new DelegatePipe(load => load["pipe2"] = true);
            var pipe3 = new DelegatePipe(load => load["pipe3"] = true);

            var connector = new DirectConnector();

            connector.ReceivePipe(pipe1, pipe2);
            connector.ReceivePipe(pipe1, pipe3);
        }
    }
}
