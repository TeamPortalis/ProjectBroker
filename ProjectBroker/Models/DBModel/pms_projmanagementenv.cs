//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectBroker.Models.DBModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class pms_projmanagementenv
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pms_projmanagementenv()
        {
            this.pr_project = new HashSet<pr_project>();
        }
    
        public string pms_id { get; set; }
        public string pms_name { get; set; }
        public string pms_desc { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pr_project> pr_project { get; set; }
    }
}
