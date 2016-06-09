using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectBroker.Models.DBModel;

namespace ProjectBroker.Models.ViewModels
{
    /// <summary>
    /// View Model for the Main View.
    /// </summary>
    public class MainViewViewModel
    {

        public MainViewViewModel() { }

        /// <summary>
        /// Active User
        /// </summary>
        public p_person ActiveUser { get; set; }
        /// <summary>
        /// All Projects the Current active user is working on.
        /// </summary>
        public IEnumerable<pr_project> CurrentProjects { get; set; }
        /// <summary>
        /// All Recommended Projects the user should see based on his user statistics.
        /// Has yet to be implemented sensefully.
        /// </summary>
        public IEnumerable<pr_project> RecommendedProjects { get; set; }

    }
}