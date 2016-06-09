using ProjectBroker.Models.DBModel;
using System.Collections;
using System.Collections.Generic;

namespace ProjectBroker.Models.ViewModels
{
    /// <summary>
    /// The View Model used to provide data about a project to the view.
    /// </summary>
    public class ProjectDetailsVM
    {
        public ProjectDetailsVM()
        {
        }

        /// <summary>
        /// All Team members working on a project.
        /// </summary>
        public IEnumerable<s_student> TeamMembers { get; set; }
        /// <summary>
        /// The teacher taking care of the project
        /// </summary>
        public t_teacher Teacher { get; set; }
        /// <summary>
        /// The reference containing all necessary data.
        /// </summary>
        public pr_project Project { get; set; }
    }
}