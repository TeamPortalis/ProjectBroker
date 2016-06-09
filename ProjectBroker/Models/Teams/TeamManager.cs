using ProjectBroker.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectBroker.Models.Teams
{
    public class TeamManager
    {

        public static string NextTeamID
        {
            get
            {
                var teams = DBManager.db.tm_team.ToList();
                var teamId = teams.Max(x => int.Parse(x.tm_id.Substring(2)));
                return "TM" + (teamId+1);
            }
        }

    }
}