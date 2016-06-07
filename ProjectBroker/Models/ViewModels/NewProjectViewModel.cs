using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectBroker.Models.DBModel;

namespace ProjectBroker.Models.ViewModels
{
    public class NewProjectViewModel
    {
        
        public IEnumerable<phs_projhostingenv> AllProjectHostingEnvs { get; set; }
        public IEnumerable<pms_projmanagementenv> AllProjectManagementEnvs { get; set; }

        public IEnumerable<tm_team> AllTeams { get; set; }
        public IEnumerable<s_student> AllPossibleTeamMembers { get; set; }
    }
}