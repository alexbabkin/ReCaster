﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Recaster.Client.RecasterService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="RecasterService.IWcfService")]
    public interface IWcfService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/StartEndpoint", ReplyAction="http://tempuri.org/IWcfService/StartEndpointResponse")]
        void StartEndpoint(Recaster.Common.EndpointType endpointType);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/StartEndpoint", ReplyAction="http://tempuri.org/IWcfService/StartEndpointResponse")]
        System.Threading.Tasks.Task StartEndpointAsync(Recaster.Common.EndpointType endpointType);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/StopEndpoint", ReplyAction="http://tempuri.org/IWcfService/StopEndpointResponse")]
        void StopEndpoint();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/StopEndpoint", ReplyAction="http://tempuri.org/IWcfService/StopEndpointResponse")]
        System.Threading.Tasks.Task StopEndpointAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/SetMulticastRcvSettings", ReplyAction="http://tempuri.org/IWcfService/SetMulticastRcvSettingsResponse")]
        void SetMulticastRcvSettings(System.Collections.Generic.List<Recaster.Common.MulticastGroupSettings> settings);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/SetMulticastRcvSettings", ReplyAction="http://tempuri.org/IWcfService/SetMulticastRcvSettingsResponse")]
        System.Threading.Tasks.Task SetMulticastRcvSettingsAsync(System.Collections.Generic.List<Recaster.Common.MulticastGroupSettings> settings);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/GetMulticastRcvSettings", ReplyAction="http://tempuri.org/IWcfService/GetMulticastRcvSettingsResponse")]
        System.Collections.Generic.List<Recaster.Common.MulticastGroupSettings> GetMulticastRcvSettings();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/GetMulticastRcvSettings", ReplyAction="http://tempuri.org/IWcfService/GetMulticastRcvSettingsResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Recaster.Common.MulticastGroupSettings>> GetMulticastRcvSettingsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/SetUnicastServerSettings", ReplyAction="http://tempuri.org/IWcfService/SetUnicastServerSettingsResponse")]
        void SetUnicastServerSettings(Recaster.Common.UnicastSettings settings);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/SetUnicastServerSettings", ReplyAction="http://tempuri.org/IWcfService/SetUnicastServerSettingsResponse")]
        System.Threading.Tasks.Task SetUnicastServerSettingsAsync(Recaster.Common.UnicastSettings settings);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/GetUnicastServerSettings", ReplyAction="http://tempuri.org/IWcfService/GetUnicastServerSettingsResponse")]
        Recaster.Common.UnicastSettings GetUnicastServerSettings();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/GetUnicastServerSettings", ReplyAction="http://tempuri.org/IWcfService/GetUnicastServerSettingsResponse")]
        System.Threading.Tasks.Task<Recaster.Common.UnicastSettings> GetUnicastServerSettingsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/SetUnicastClientSettings", ReplyAction="http://tempuri.org/IWcfService/SetUnicastClientSettingsResponse")]
        void SetUnicastClientSettings(Recaster.Common.UnicastSettings settings);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/SetUnicastClientSettings", ReplyAction="http://tempuri.org/IWcfService/SetUnicastClientSettingsResponse")]
        System.Threading.Tasks.Task SetUnicastClientSettingsAsync(Recaster.Common.UnicastSettings settings);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/GetUnicastClientSettings", ReplyAction="http://tempuri.org/IWcfService/GetUnicastClientSettingsResponse")]
        Recaster.Common.UnicastSettings GetUnicastClientSettings();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWcfService/GetUnicastClientSettings", ReplyAction="http://tempuri.org/IWcfService/GetUnicastClientSettingsResponse")]
        System.Threading.Tasks.Task<Recaster.Common.UnicastSettings> GetUnicastClientSettingsAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWcfServiceChannel : Recaster.Client.RecasterService.IWcfService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WcfServiceClient : System.ServiceModel.ClientBase<Recaster.Client.RecasterService.IWcfService>, Recaster.Client.RecasterService.IWcfService {
        
        public WcfServiceClient() {
        }
        
        public WcfServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WcfServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WcfServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WcfServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void StartEndpoint(Recaster.Common.EndpointType endpointType) {
            base.Channel.StartEndpoint(endpointType);
        }
        
        public System.Threading.Tasks.Task StartEndpointAsync(Recaster.Common.EndpointType endpointType) {
            return base.Channel.StartEndpointAsync(endpointType);
        }
        
        public void StopEndpoint() {
            base.Channel.StopEndpoint();
        }
        
        public System.Threading.Tasks.Task StopEndpointAsync() {
            return base.Channel.StopEndpointAsync();
        }
        
        public void SetMulticastRcvSettings(System.Collections.Generic.List<Recaster.Common.MulticastGroupSettings> settings) {
            base.Channel.SetMulticastRcvSettings(settings);
        }
        
        public System.Threading.Tasks.Task SetMulticastRcvSettingsAsync(System.Collections.Generic.List<Recaster.Common.MulticastGroupSettings> settings) {
            return base.Channel.SetMulticastRcvSettingsAsync(settings);
        }
        
        public System.Collections.Generic.List<Recaster.Common.MulticastGroupSettings> GetMulticastRcvSettings() {
            return base.Channel.GetMulticastRcvSettings();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Recaster.Common.MulticastGroupSettings>> GetMulticastRcvSettingsAsync() {
            return base.Channel.GetMulticastRcvSettingsAsync();
        }
        
        public void SetUnicastServerSettings(Recaster.Common.UnicastSettings settings) {
            base.Channel.SetUnicastServerSettings(settings);
        }
        
        public System.Threading.Tasks.Task SetUnicastServerSettingsAsync(Recaster.Common.UnicastSettings settings) {
            return base.Channel.SetUnicastServerSettingsAsync(settings);
        }
        
        public Recaster.Common.UnicastSettings GetUnicastServerSettings() {
            return base.Channel.GetUnicastServerSettings();
        }
        
        public System.Threading.Tasks.Task<Recaster.Common.UnicastSettings> GetUnicastServerSettingsAsync() {
            return base.Channel.GetUnicastServerSettingsAsync();
        }
        
        public void SetUnicastClientSettings(Recaster.Common.UnicastSettings settings) {
            base.Channel.SetUnicastClientSettings(settings);
        }
        
        public System.Threading.Tasks.Task SetUnicastClientSettingsAsync(Recaster.Common.UnicastSettings settings) {
            return base.Channel.SetUnicastClientSettingsAsync(settings);
        }
        
        public Recaster.Common.UnicastSettings GetUnicastClientSettings() {
            return base.Channel.GetUnicastClientSettings();
        }
        
        public System.Threading.Tasks.Task<Recaster.Common.UnicastSettings> GetUnicastClientSettingsAsync() {
            return base.Channel.GetUnicastClientSettingsAsync();
        }
    }
}
