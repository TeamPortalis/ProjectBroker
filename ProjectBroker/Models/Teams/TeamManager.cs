using ProjectBroker.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectBroker.Models.Teams
{
    /// <summary>
    /// A helper Class for Team Creation and intended for unification of data access code in the future.
    /// </summary>
    public class TeamManager
    {

        /// <summary>
        /// Provided the next Team ID
        /// </summary>
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