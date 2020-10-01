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

        public new TChannel Channel => base.Channel;
        public void Dispose() => this.SafeDispose();
    }
}
