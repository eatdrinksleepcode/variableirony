using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace VariableIrony.ServiceModel {
    public class DuplexClientProxy<TChannel> : DuplexClientBase<TChannel>, IDisposable where TChannel : class {
        public DuplexClientProxy(InstanceContext callbackInstance) : base(callbackInstance) { }
        public DuplexClientProxy(InstanceContext callbackInstance, string endpointConfigurationName) : base(callbackInstance, endpointConfigurationName) { }
        public DuplexClientProxy(InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : base(callbackInstance, endpointConfigurationName, remoteAddress) { }
        public DuplexClientProxy(InstanceContext callbackInstance, string endpointConfigurationName, EndpointAddress remoteAddress) : base(callbackInstance, endpointConfigurationName, remoteAddress) { }
        public DuplexClientProxy(InstanceContext callbackInstance, Binding binding, EndpointAddress remoteAddress) : base(callbackInstance, binding, remoteAddress) { }
        public DuplexClientProxy(InstanceContext callbackInstance, ServiceEndpoint endpoint) : base(callbackInstance, endpoint) { }
        public DuplexClientProxy(object callbackInstance) : base(callbackInstance) { }
        public DuplexClientProxy(object callbackInstance, string endpointConfigurationName) : base(callbackInstance, endpointConfigurationName) { }
        public DuplexClientProxy(object callbackInstance, string endpointConfigurationName, string remoteAddress) : base(callbackInstance, endpointConfigurationName, remoteAddress) { }
        public DuplexClientProxy(object callbackInstance, string endpointConfigurationName, EndpointAddress remoteAddress) : base(callbackInstance, endpointConfigurationName, remoteAddress) { }
        public DuplexClientProxy(object callbackInstance, Binding binding, EndpointAddress remoteAddress) : base(callbackInstance, binding, remoteAddress) { }
        public DuplexClientProxy(object callbackInstance, ServiceEndpoint endpoint) : base(callbackInstance, endpoint) { }

        public new TChannel Channel {
            get { return base.Channel; }
        }

        public void Dispose() {
            this.SafeDispose();
        }
    }
}
