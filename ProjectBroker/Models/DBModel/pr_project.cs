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
    
    public partial class pr_project
    {
        public string pr_id { get; set; }
        public string pr_name { get; set; }
        public string pr_wiki { get; set; }
        public string pr_image { get; set; }
        public string pr_t_id { get; set; }
        public string pr_pms_id { get; set; }
        public string pr_phs_id { get; set; }
        public string pr_tm_id { get; set; }
    
        public virtual phs_projhostingenv phs_projhostingenv { get; set; }
        public virtual pms_projmanagementenv pms_projmanagementenv { get; set; }
        public virtual t_teacher t_teacher { get; set; }
        public virtual tm_team tm_team { get; set; }
    }
}