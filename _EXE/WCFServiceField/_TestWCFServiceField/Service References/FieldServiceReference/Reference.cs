﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _TestWCFServiceField.FieldServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Varoff", Namespace="http://schemas.datacontract.org/2004/07/SOV.SGMO")]
    [System.SerializableAttribute()]
    public partial class Varoff : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int OffsetTypeIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double OffsetValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int VariableIdField;
        
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
        public int OffsetTypeId {
            get {
                return this.OffsetTypeIdField;
            }
            set {
                if ((this.OffsetTypeIdField.Equals(value) != true)) {
                    this.OffsetTypeIdField = value;
                    this.RaisePropertyChanged("OffsetTypeId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double OffsetValue {
            get {
                return this.OffsetValueField;
            }
            set {
                if ((this.OffsetValueField.Equals(value) != true)) {
                    this.OffsetValueField = value;
                    this.RaisePropertyChanged("OffsetValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int VariableId {
            get {
                return this.VariableIdField;
            }
            set {
                if ((this.VariableIdField.Equals(value) != true)) {
                    this.VariableIdField = value;
                    this.RaisePropertyChanged("VariableId");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GeoRectangle", Namespace="http://schemas.datacontract.org/2004/07/SOV.Geo")]
    [System.SerializableAttribute()]
    public partial class GeoRectangle : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private _TestWCFServiceField.FieldServiceReference.GeoPoint NorthWestField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private _TestWCFServiceField.FieldServiceReference.GeoPoint SouthEastField;
        
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
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
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
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public _TestWCFServiceField.FieldServiceReference.GeoPoint NorthWest {
            get {
                return this.NorthWestField;
            }
            set {
                if ((object.ReferenceEquals(this.NorthWestField, value) != true)) {
                    this.NorthWestField = value;
                    this.RaisePropertyChanged("NorthWest");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public _TestWCFServiceField.FieldServiceReference.GeoPoint SouthEast {
            get {
                return this.SouthEastField;
            }
            set {
                if ((object.ReferenceEquals(this.SouthEastField, value) != true)) {
                    this.SouthEastField = value;
                    this.RaisePropertyChanged("SouthEast");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GeoPoint", Namespace="http://schemas.datacontract.org/2004/07/SOV.Geo")]
    [System.SerializableAttribute()]
    public partial class GeoPoint : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double LatGrdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double LonGrdField;
        
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
        public double LatGrd {
            get {
                return this.LatGrdField;
            }
            set {
                if ((this.LatGrdField.Equals(value) != true)) {
                    this.LatGrdField = value;
                    this.RaisePropertyChanged("LatGrd");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double LonGrd {
            get {
                return this.LonGrdField;
            }
            set {
                if ((this.LonGrdField.Equals(value) != true)) {
                    this.LonGrdField = value;
                    this.RaisePropertyChanged("LonGrd");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Field", Namespace="http://schemas.datacontract.org/2004/07/SOV")]
    [System.SerializableAttribute()]
    public partial class Field : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private _TestWCFServiceField.FieldServiceReference.EnumFieldFormat FieldFormatField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private _TestWCFServiceField.FieldServiceReference.Grid GridField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double PredictTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double[] ValueField;
        
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
        public _TestWCFServiceField.FieldServiceReference.EnumFieldFormat FieldFormat {
            get {
                return this.FieldFormatField;
            }
            set {
                if ((this.FieldFormatField.Equals(value) != true)) {
                    this.FieldFormatField = value;
                    this.RaisePropertyChanged("FieldFormat");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public _TestWCFServiceField.FieldServiceReference.Grid Grid {
            get {
                return this.GridField;
            }
            set {
                if ((object.ReferenceEquals(this.GridField, value) != true)) {
                    this.GridField = value;
                    this.RaisePropertyChanged("Grid");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double PredictTime {
            get {
                return this.PredictTimeField;
            }
            set {
                if ((this.PredictTimeField.Equals(value) != true)) {
                    this.PredictTimeField = value;
                    this.RaisePropertyChanged("PredictTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double[] Value {
            get {
                return this.ValueField;
            }
            set {
                if ((object.ReferenceEquals(this.ValueField, value) != true)) {
                    this.ValueField = value;
                    this.RaisePropertyChanged("Value");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Grid", Namespace="http://schemas.datacontract.org/2004/07/SOV.Geo")]
    [System.SerializableAttribute()]
    public partial class Grid : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double LatStartMinField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double LatStepMinField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double[] LatsMinField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double LonStartMinField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double LonStepMinField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double[] LonsMinField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int TypeIdField;
        
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
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double LatStartMin {
            get {
                return this.LatStartMinField;
            }
            set {
                if ((this.LatStartMinField.Equals(value) != true)) {
                    this.LatStartMinField = value;
                    this.RaisePropertyChanged("LatStartMin");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double LatStepMin {
            get {
                return this.LatStepMinField;
            }
            set {
                if ((this.LatStepMinField.Equals(value) != true)) {
                    this.LatStepMinField = value;
                    this.RaisePropertyChanged("LatStepMin");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double[] LatsMin {
            get {
                return this.LatsMinField;
            }
            set {
                if ((object.ReferenceEquals(this.LatsMinField, value) != true)) {
                    this.LatsMinField = value;
                    this.RaisePropertyChanged("LatsMin");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double LonStartMin {
            get {
                return this.LonStartMinField;
            }
            set {
                if ((this.LonStartMinField.Equals(value) != true)) {
                    this.LonStartMinField = value;
                    this.RaisePropertyChanged("LonStartMin");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double LonStepMin {
            get {
                return this.LonStepMinField;
            }
            set {
                if ((this.LonStepMinField.Equals(value) != true)) {
                    this.LonStepMinField = value;
                    this.RaisePropertyChanged("LonStepMin");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double[] LonsMin {
            get {
                return this.LonsMinField;
            }
            set {
                if ((object.ReferenceEquals(this.LonsMinField, value) != true)) {
                    this.LonsMinField = value;
                    this.RaisePropertyChanged("LonsMin");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int TypeId {
            get {
                return this.TypeIdField;
            }
            set {
                if ((this.TypeIdField.Equals(value) != true)) {
                    this.TypeIdField = value;
                    this.RaisePropertyChanged("TypeId");
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EnumFieldFormat", Namespace="http://schemas.datacontract.org/2004/07/SOV")]
    public enum EnumFieldFormat : long {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        GRID = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        XYZ = 2,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Method", Namespace="http://schemas.datacontract.org/2004/07/SOV.Amur.Meta")]
    [System.SerializableAttribute()]
    public partial class Method : _TestWCFServiceField.FieldServiceReference.IdName {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.Dictionary<string, string> MethodOutputStoreParametersField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private short OrderField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> ParentIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> SourceLegalEntityIdField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.Dictionary<string, string> MethodOutputStoreParameters {
            get {
                return this.MethodOutputStoreParametersField;
            }
            set {
                if ((object.ReferenceEquals(this.MethodOutputStoreParametersField, value) != true)) {
                    this.MethodOutputStoreParametersField = value;
                    this.RaisePropertyChanged("MethodOutputStoreParameters");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public short Order {
            get {
                return this.OrderField;
            }
            set {
                if ((this.OrderField.Equals(value) != true)) {
                    this.OrderField = value;
                    this.RaisePropertyChanged("Order");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> ParentId {
            get {
                return this.ParentIdField;
            }
            set {
                if ((this.ParentIdField.Equals(value) != true)) {
                    this.ParentIdField = value;
                    this.RaisePropertyChanged("ParentId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> SourceLegalEntityId {
            get {
                return this.SourceLegalEntityIdField;
            }
            set {
                if ((this.SourceLegalEntityIdField.Equals(value) != true)) {
                    this.SourceLegalEntityIdField = value;
                    this.RaisePropertyChanged("SourceLegalEntityId");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="IdClass", Namespace="http://schemas.datacontract.org/2004/07/SOV.Common")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(_TestWCFServiceField.FieldServiceReference.IdName))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(_TestWCFServiceField.FieldServiceReference.Method))]
    public partial class IdClass : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
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
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="IdName", Namespace="http://schemas.datacontract.org/2004/07/SOV.Common")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(_TestWCFServiceField.FieldServiceReference.Method))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(_TestWCFServiceField.FieldServiceReference.Method[]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(_TestWCFServiceField.FieldServiceReference.IdClass))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(_TestWCFServiceField.FieldServiceReference.GeoRectangle[]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(_TestWCFServiceField.FieldServiceReference.GeoRectangle))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(_TestWCFServiceField.FieldServiceReference.GeoPoint))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(_TestWCFServiceField.FieldServiceReference.Grid))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(_TestWCFServiceField.FieldServiceReference.GeoPoint[]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(_TestWCFServiceField.FieldServiceReference.Field[][][]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(_TestWCFServiceField.FieldServiceReference.Field[][]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(_TestWCFServiceField.FieldServiceReference.Field[]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(_TestWCFServiceField.FieldServiceReference.Field))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(_TestWCFServiceField.FieldServiceReference.EnumFieldFormat))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(double[]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(int[]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(System.Collections.Generic.Dictionary<double, double[]>))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(System.Collections.Generic.Dictionary<string, string>))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(_TestWCFServiceField.FieldServiceReference.Varoff[]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(_TestWCFServiceField.FieldServiceReference.Varoff))]
    public partial class IdName : _TestWCFServiceField.FieldServiceReference.IdClass {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private object EntityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public object Entity {
            get {
                return this.EntityField;
            }
            set {
                if ((object.ReferenceEquals(this.EntityField, value) != true)) {
                    this.EntityField = value;
                    this.RaisePropertyChanged("Entity");
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
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="FieldServiceReference.IService")]
    public interface IService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/Open", ReplyAction="http://tempuri.org/IService/OpenResponse")]
        long Open(string userName, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/Open", ReplyAction="http://tempuri.org/IService/OpenResponse")]
        System.Threading.Tasks.Task<long> OpenAsync(string userName, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/Close", ReplyAction="http://tempuri.org/IService/CloseResponse")]
        void Close(long hSvc);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/Close", ReplyAction="http://tempuri.org/IService/CloseResponse")]
        System.Threading.Tasks.Task CloseAsync(long hSvc);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/CloseByUserName", ReplyAction="http://tempuri.org/IService/CloseByUserNameResponse")]
        void CloseByUserName(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/CloseByUserName", ReplyAction="http://tempuri.org/IService/CloseByUserNameResponse")]
        System.Threading.Tasks.Task CloseByUserNameAsync(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetExtentForecast", ReplyAction="http://tempuri.org/IService/GetExtentForecastResponse")]
        _TestWCFServiceField.FieldServiceReference.Field[][][] GetExtentForecast(long hSvc, System.DateTime dateIni, double[] leadTimes, int methodId, _TestWCFServiceField.FieldServiceReference.Varoff[] varoffs, _TestWCFServiceField.FieldServiceReference.GeoRectangle[] grs);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetExtentForecast", ReplyAction="http://tempuri.org/IService/GetExtentForecastResponse")]
        System.Threading.Tasks.Task<_TestWCFServiceField.FieldServiceReference.Field[][][]> GetExtentForecastAsync(long hSvc, System.DateTime dateIni, double[] leadTimes, int methodId, _TestWCFServiceField.FieldServiceReference.Varoff[] varoffs, _TestWCFServiceField.FieldServiceReference.GeoRectangle[] grs);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetSitesForecast", ReplyAction="http://tempuri.org/IService/GetSitesForecastResponse")]
        System.Collections.Generic.Dictionary<double, double[]> GetSitesForecast(long hSvc, System.DateTime dateIni, double[] leadTimes, int[] pointCatalogsId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetSitesForecast", ReplyAction="http://tempuri.org/IService/GetSitesForecastResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<double, double[]>> GetSitesForecastAsync(long hSvc, System.DateTime dateIni, double[] leadTimes, int[] pointCatalogsId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetMethods", ReplyAction="http://tempuri.org/IService/GetMethodsResponse")]
        _TestWCFServiceField.FieldServiceReference.Method[] GetMethods(long hSvc);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetMethods", ReplyAction="http://tempuri.org/IService/GetMethodsResponse")]
        System.Threading.Tasks.Task<_TestWCFServiceField.FieldServiceReference.Method[]> GetMethodsAsync(long hSvc);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetTrackForecast", ReplyAction="http://tempuri.org/IService/GetTrackForecastResponse")]
        System.Collections.Generic.Dictionary<double, double[]> GetTrackForecast(long hSvc, System.DateTime dateIni, _TestWCFServiceField.FieldServiceReference.GeoPoint[] track, int pointMethodId, _TestWCFServiceField.FieldServiceReference.Varoff[] pointVaroffs);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetTrackForecast", ReplyAction="http://tempuri.org/IService/GetTrackForecastResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<double, double[]>> GetTrackForecastAsync(long hSvc, System.DateTime dateIni, _TestWCFServiceField.FieldServiceReference.GeoPoint[] track, int pointMethodId, _TestWCFServiceField.FieldServiceReference.Varoff[] pointVaroffs);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceChannel : _TestWCFServiceField.FieldServiceReference.IService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceClient : System.ServiceModel.ClientBase<_TestWCFServiceField.FieldServiceReference.IService>, _TestWCFServiceField.FieldServiceReference.IService {
        
        public ServiceClient() {
        }
        
        public ServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public long Open(string userName, string password) {
            return base.Channel.Open(userName, password);
        }
        
        public System.Threading.Tasks.Task<long> OpenAsync(string userName, string password) {
            return base.Channel.OpenAsync(userName, password);
        }
        
        public void Close(long hSvc) {
            base.Channel.Close(hSvc);
        }
        
        public System.Threading.Tasks.Task CloseAsync(long hSvc) {
            return base.Channel.CloseAsync(hSvc);
        }
        
        public void CloseByUserName(string userName) {
            base.Channel.CloseByUserName(userName);
        }
        
        public System.Threading.Tasks.Task CloseByUserNameAsync(string userName) {
            return base.Channel.CloseByUserNameAsync(userName);
        }
        
        public _TestWCFServiceField.FieldServiceReference.Field[][][] GetExtentForecast(long hSvc, System.DateTime dateIni, double[] leadTimes, int methodId, _TestWCFServiceField.FieldServiceReference.Varoff[] varoffs, _TestWCFServiceField.FieldServiceReference.GeoRectangle[] grs) {
            return base.Channel.GetExtentForecast(hSvc, dateIni, leadTimes, methodId, varoffs, grs);
        }
        
        public System.Threading.Tasks.Task<_TestWCFServiceField.FieldServiceReference.Field[][][]> GetExtentForecastAsync(long hSvc, System.DateTime dateIni, double[] leadTimes, int methodId, _TestWCFServiceField.FieldServiceReference.Varoff[] varoffs, _TestWCFServiceField.FieldServiceReference.GeoRectangle[] grs) {
            return base.Channel.GetExtentForecastAsync(hSvc, dateIni, leadTimes, methodId, varoffs, grs);
        }
        
        public System.Collections.Generic.Dictionary<double, double[]> GetSitesForecast(long hSvc, System.DateTime dateIni, double[] leadTimes, int[] pointCatalogsId) {
            return base.Channel.GetSitesForecast(hSvc, dateIni, leadTimes, pointCatalogsId);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<double, double[]>> GetSitesForecastAsync(long hSvc, System.DateTime dateIni, double[] leadTimes, int[] pointCatalogsId) {
            return base.Channel.GetSitesForecastAsync(hSvc, dateIni, leadTimes, pointCatalogsId);
        }
        
        public _TestWCFServiceField.FieldServiceReference.Method[] GetMethods(long hSvc) {
            return base.Channel.GetMethods(hSvc);
        }
        
        public System.Threading.Tasks.Task<_TestWCFServiceField.FieldServiceReference.Method[]> GetMethodsAsync(long hSvc) {
            return base.Channel.GetMethodsAsync(hSvc);
        }
        
        public System.Collections.Generic.Dictionary<double, double[]> GetTrackForecast(long hSvc, System.DateTime dateIni, _TestWCFServiceField.FieldServiceReference.GeoPoint[] track, int pointMethodId, _TestWCFServiceField.FieldServiceReference.Varoff[] pointVaroffs) {
            return base.Channel.GetTrackForecast(hSvc, dateIni, track, pointMethodId, pointVaroffs);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<double, double[]>> GetTrackForecastAsync(long hSvc, System.DateTime dateIni, _TestWCFServiceField.FieldServiceReference.GeoPoint[] track, int pointMethodId, _TestWCFServiceField.FieldServiceReference.Varoff[] pointVaroffs) {
            return base.Channel.GetTrackForecastAsync(hSvc, dateIni, track, pointMethodId, pointVaroffs);
        }
    }
}
