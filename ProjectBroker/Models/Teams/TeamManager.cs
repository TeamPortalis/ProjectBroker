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
                var teamId = DBManager.db.tm_team.Max(x => int.Parse(x.tm_id.Substring(2)));
                return "TM" + teamId;
            }
        }

    }
}