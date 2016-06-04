using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectBroker.Models.DBModel;

namespace ProjectBroker.Models.ViewModels
{
    public class MainViewViewModel
    {

        public MainViewViewModel() { }

        public p_person ActiveUser { get; set; }
        public IEnumerable<pr_project> CurrentProjects { get; set; }
        public IEnumerable<pr_project> RecommendedProjects { get; set; }

    }
}