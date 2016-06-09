using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectBroker.Models.DBModel;

namespace ProjectBroker.Models.ViewModels
{

    /// <summary>
    /// The View Model used to provide structured data for the project creation wizard.
    /// </summary>
    public class NewProjectViewModel
    {

        /// <summary>
        /// All Project Hosting environments available.
        /// </summary>
        public IEnumerable<phs_projhostingenv> AllProjectHostingEnvs { get; set; }
        /// <summary>
        /// All Project Management Environments available.
        /// </summary>
        public IEnumerable<pms_projmanagementenv> AllProjectManagementEnvs { get; set; }
        /// <summary>
        /// All Teams available
        /// </summary>
        public IEnumerable<tm_team> AllTeams { get; set; }
        /// <summary>
        /// All Studens that could be added to a team.
        /// </summary>
        public IEnumerable<s_student> AllPossibleTeamMembers { get; set; }
    }
}