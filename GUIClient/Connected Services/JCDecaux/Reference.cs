﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp1.JCDecaux {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Station", Namespace="http://schemas.datacontract.org/2004/07/WSJCDecaux")]
    [System.SerializableAttribute()]
    public partial class Station : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AddressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AvailableBikeStandsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AvailableBikesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int BikeStandsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Address {
            get {
                return this.AddressField;
            }
            set {
                if ((object.ReferenceEquals(this.AddressField, value) != true)) {
                    this.AddressField = value;
                    this.RaisePropertyChanged("Address");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int AvailableBikeStands {
            get {
                return this.AvailableBikeStandsField;
            }
            set {
                if ((this.AvailableBikeStandsField.Equals(value) != true)) {
                    this.AvailableBikeStandsField = value;
                    this.RaisePropertyChanged("AvailableBikeStands");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int AvailableBikes {
            get {
                return this.AvailableBikesField;
            }
            set {
                if ((this.AvailableBikesField.Equals(value) != true)) {
                    this.AvailableBikesField = value;
                    this.RaisePropertyChanged("AvailableBikes");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int BikeStands {
            get {
                return this.BikeStandsField;
            }
            set {
                if ((this.BikeStandsField.Equals(value) != true)) {
                    this.BikeStandsField = value;
                    this.RaisePropertyChanged("BikeStands");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="JCDecaux.IJCDecaux")]
    public interface IJCDecaux {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJCDecaux/GetStations", ReplyAction="http://tempuri.org/IJCDecaux/GetStationsResponse")]
        WpfApp1.JCDecaux.Station[] GetStations(string contract, int timeout);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJCDecaux/GetStations", ReplyAction="http://tempuri.org/IJCDecaux/GetStationsResponse")]
        System.Threading.Tasks.Task<WpfApp1.JCDecaux.Station[]> GetStationsAsync(string contract, int timeout);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJCDecaux/GetContracts", ReplyAction="http://tempuri.org/IJCDecaux/GetContractsResponse")]
        string[] GetContracts();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJCDecaux/GetContracts", ReplyAction="http://tempuri.org/IJCDecaux/GetContractsResponse")]
        System.Threading.Tasks.Task<string[]> GetContractsAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IJCDecauxChannel : WpfApp1.JCDecaux.IJCDecaux, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class JCDecauxClient : System.ServiceModel.ClientBase<WpfApp1.JCDecaux.IJCDecaux>, WpfApp1.JCDecaux.IJCDecaux {
        
        public JCDecauxClient() {
        }
        
        public JCDecauxClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public JCDecauxClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public JCDecauxClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public JCDecauxClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public WpfApp1.JCDecaux.Station[] GetStations(string contract, int timeout) {
            return base.Channel.GetStations(contract, timeout);
        }
        
        public System.Threading.Tasks.Task<WpfApp1.JCDecaux.Station[]> GetStationsAsync(string contract, int timeout) {
            return base.Channel.GetStationsAsync(contract, timeout);
        }
        
        public string[] GetContracts() {
            return base.Channel.GetContracts();
        }
        
        public System.Threading.Tasks.Task<string[]> GetContractsAsync() {
            return base.Channel.GetContractsAsync();
        }
    }
}
