using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectBroker.Models.DBModel;

namespace ProjectBroker.Models
{
    public class MainViewViewModel
    {

        public MainViewViewModel() { }

        public string ActiveUser { get; set; }
        public IEnumerable<pr_project> Projects { get; set; }


    }
}