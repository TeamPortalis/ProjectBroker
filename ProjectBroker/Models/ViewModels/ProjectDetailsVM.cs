using ProjectBroker.Models.DBModel;
using System.Collections;
using System.Collections.Generic;

namespace ProjectBroker.Models.ViewModels
{
    public class ProjectDetailsVM
    {
        public ProjectDetailsVM()
        {
        }

        public IEnumerable<s_student> TeamMembers { get; set; }
        public t_teacher Teacher { get; set; }
        public pr_project Project { get; set; }
    }
}