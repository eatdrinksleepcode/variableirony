using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace VariableIrony.ServiceModel {
    public class ClientProxy<TChannel> : ClientBase<TChannel>, IDisposable where TChannel : class {
        public ClientProxy() {}
        public ClientProxy(string endpointConfigurationName) : base(endpointConfigurationName) {}
        public ClientProxy(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress) { }
        public ClientProxy(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress) { }
        public ClientProxy(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress) { }
        public ClientProxy(ServiceEndpoint endpoint) : base(endpoint) { }

        public new TChannel Channel => base.Channel;
        public void Dispose() => this.SafeDispose();
    }
}
