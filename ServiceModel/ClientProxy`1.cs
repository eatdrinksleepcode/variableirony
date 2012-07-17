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
        public ClientProxy(InstanceContext callbackInstance) : base(callbackInstance) { }
        public ClientProxy(InstanceContext callbackInstance, string endpointConfigurationName) : base(callbackInstance, endpointConfigurationName) { }
        public ClientProxy(InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : base(callbackInstance, endpointConfigurationName, remoteAddress) { }
        public ClientProxy(InstanceContext callbackInstance, string endpointConfigurationName, EndpointAddress remoteAddress) : base(callbackInstance, endpointConfigurationName, remoteAddress) { }
        public ClientProxy(InstanceContext callbackInstance, Binding binding, EndpointAddress remoteAddress) : base(callbackInstance, binding, remoteAddress) { }
        public ClientProxy(InstanceContext callbackInstance, ServiceEndpoint endpoint) : base(callbackInstance, endpoint) { }

        public new TChannel Channel {
            get { return base.Channel; }
        }

        public void Dispose() {
            this.SafeDispose();
        }
    }
}
