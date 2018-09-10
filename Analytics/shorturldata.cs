//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Analytics
{
    using System;
    using System.Collections.Generic;
    
    public partial class shorturldata
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public shorturldata()
        {
            this.cookietables = new HashSet<cookietable>();
        }
    
        public int PK_Shorturl { get; set; }
        public string Ipv4 { get; set; }
        public string Ipv6 { get; set; }
        public Nullable<long> ip_num { get; set; }
        public string Browser { get; set; }
        public string Browser_version { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string PostalCode { get; set; }
        public string City_Latitude { get; set; }
        public string City_Longitude { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string MetroCode { get; set; }
        public string Req_url { get; set; }
        public string UserAgent { get; set; }
        public string Hostname { get; set; }
        public string DeviceName { get; set; }
        public string DeviceBrand { get; set; }
        public string OS_Name { get; set; }
        public string OS_Version { get; set; }
        public string IsMobileDevice { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> FK_Uid { get; set; }
        public Nullable<int> FK_RID { get; set; }
        public Nullable<int> FK_ClientID { get; set; }
        public Nullable<int> FK_City_Master_id { get; set; }
        public string ACK { get; set; }
        public Nullable<System.DateTime> ACKDATE { get; set; }
    
        public virtual client client { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cookietable> cookietables { get; set; }
        public virtual riddata riddata { get; set; }
        public virtual uiddata uiddata { get; set; }
    }
}
