﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=4.8.3928.0.
// 
namespace TP.InformationComputation.PartialDefinitions {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://Viculu34.org/Catalog.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://Viculu34.org/Catalog.xsd", IsNullable=false)]
    public partial class Catalog {
        
        private CatalogCD[] cdField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CD")]
        public CatalogCD[] CD {
            get {
                return this.cdField;
            }
            set {
                this.cdField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://Viculu34.org/Catalog.xsd")]
    public partial class CatalogCD {
        
        private string titleField;
        
        private string artistField;
        
        private string countryField;
        
        private string companyField;
        
        private decimal priceField;
        
        private ushort yearField;
        
        /// <remarks/>
        public string Title {
            get {
                return this.titleField;
            }
            set {
                this.titleField = value;
            }
        }
        
        /// <remarks/>
        public string Artist {
            get {
                return this.artistField;
            }
            set {
                this.artistField = value;
            }
        }
        
        /// <remarks/>
        public string Country {
            get {
                return this.countryField;
            }
            set {
                this.countryField = value;
            }
        }
        
        /// <remarks/>
        public string Company {
            get {
                return this.companyField;
            }
            set {
                this.companyField = value;
            }
        }
        
        /// <remarks/>
        public decimal Price {
            get {
                return this.priceField;
            }
            set {
                this.priceField = value;
            }
        }
        
        /// <remarks/>
        public ushort Year {
            get {
                return this.yearField;
            }
            set {
                this.yearField = value;
            }
        }
    }
}
