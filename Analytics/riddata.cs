//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Analytics
{
    using System;
    using System.Collections.Generic;
    
    public partial class riddata
    {
        public riddata()
        {
            this.shorturldatas = new HashSet<shorturldata>();
            this.uiddatas = new HashSet<uiddata>();
        }
    
        public int PK_Rid { get; set; }
        public string CampaignName { get; set; }
        public string ReferenceNumber { get; set; }
        public string Pwd { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public int FK_ClientID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
    
        public virtual client client { get; set; }
        public virtual ICollection<shorturldata> shorturldatas { get; set; }
        public virtual ICollection<uiddata> uiddatas { get; set; }
    }
}