using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using ReleaseTracker.Core.Interfaces;

namespace ReleaseTracker.DataAccess.Repositories
{
    public class AdoNetReleaseRepository : IReleaseRepository
    {
        public IEnumerable<int> GetReleaseIds(int WorkItemId)
        {
            var ReleaseIds = new List<int>();

            var Query = SqlQuery.FindReleases + WorkItemId;
            var Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
            var SqlCommand = new SqlCommand(Query);
            SqlCommand.Connection = Connection;

            Connection.Open();

            var Reader = SqlCommand.ExecuteReader();

            while (Reader.Read())
            {
                ReleaseIds.Add(Convert.ToInt32(Reader["Id"]));
            }

            Connection.Close();

            return ReleaseIds;
        }
    }
}

