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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="RecasterService.IWCFService")]
    public interface IWCFService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFService/StartEndpoint", ReplyAction="http://tempuri.org/IWCFService/StartEndpointResponse")]
        void StartEndpoint(Recaster.Common.EndpointType endpointType);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFService/StartEndpoint", ReplyAction="http://tempuri.org/IWCFService/StartEndpointResponse")]
        System.Threading.Tasks.Task StartEndpointAsync(Recaster.Common.EndpointType endpointType);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFService/StopEndpoint", ReplyAction="http://tempuri.org/IWCFService/StopEndpointResponse")]
        void StopEndpoint();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFService/StopEndpoint", ReplyAction="http://tempuri.org/IWCFService/StopEndpointResponse")]
        System.Threading.Tasks.Task StopEndpointAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFService/SetMulticastRcvSettings", ReplyAction="http://tempuri.org/IWCFService/SetMulticastRcvSettingsResponse")]
        void SetMulticastRcvSettings(System.Collections.Generic.List<Recaster.Common.MulticastGroupSettings> settings);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFService/SetMulticastRcvSettings", ReplyAction="http://tempuri.org/IWCFService/SetMulticastRcvSettingsResponse")]
        System.Threading.Tasks.Task SetMulticastRcvSettingsAsync(System.Collections.Generic.List<Recaster.Common.MulticastGroupSettings> settings);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFService/GetMulticastRcvSettings", ReplyAction="http://tempuri.org/IWCFService/GetMulticastRcvSettingsResponse")]
        System.Collections.Generic.List<Recaster.Common.MulticastGroupSettings> GetMulticastRcvSettings();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFService/GetMulticastRcvSettings", ReplyAction="http://tempuri.org/IWCFService/GetMulticastRcvSettingsResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Recaster.Common.MulticastGroupSettings>> GetMulticastRcvSettingsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFService/SetUnicastServerSettings", ReplyAction="http://tempuri.org/IWCFService/SetUnicastServerSettingsResponse")]
        void SetUnicastServerSettings(Recaster.Common.UnicastSettings settings);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFService/SetUnicastServerSettings", ReplyAction="http://tempuri.org/IWCFService/SetUnicastServerSettingsResponse")]
        System.Threading.Tasks.Task SetUnicastServerSettingsAsync(Recaster.Common.UnicastSettings settings);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFService/GetUnicastServerSettings", ReplyAction="http://tempuri.org/IWCFService/GetUnicastServerSettingsResponse")]
        Recaster.Common.UnicastSettings GetUnicastServerSettings();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFService/GetUnicastServerSettings", ReplyAction="http://tempuri.org/IWCFService/GetUnicastServerSettingsResponse")]
        System.Threading.Tasks.Task<Recaster.Common.UnicastSettings> GetUnicastServerSettingsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFService/SetUnicastClientSettings", ReplyAction="http://tempuri.org/IWCFService/SetUnicastClientSettingsResponse")]
        void SetUnicastClientSettings(Recaster.Common.UnicastSettings settings);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFService/SetUnicastClientSettings", ReplyAction="http://tempuri.org/IWCFService/SetUnicastClientSettingsResponse")]
        System.Threading.Tasks.Task SetUnicastClientSettingsAsync(Recaster.Common.UnicastSettings settings);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFService/GetUnicastClientSettings", ReplyAction="http://tempuri.org/IWCFService/GetUnicastClientSettingsResponse")]
        Recaster.Common.UnicastSettings GetUnicastClientSettings();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFService/GetUnicastClientSettings", ReplyAction="http://tempuri.org/IWCFService/GetUnicastClientSettingsResponse")]
        System.Threading.Tasks.Task<Recaster.Common.UnicastSettings> GetUnicastClientSettingsAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWCFServiceChannel : Recaster.Client.RecasterService.IWCFService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WCFServiceClient : System.ServiceModel.ClientBase<Recaster.Client.RecasterService.IWCFService>, Recaster.Client.RecasterService.IWCFService {
        
        public WCFServiceClient() {
        }
        
        public WCFServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WCFServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WCFServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WCFServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
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
