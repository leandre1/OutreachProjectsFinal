using Dapper;
using Outreach.Data.Interface;
using Outreach.Entities.House;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Outreach.Data.Repository
{
    
    public class HouseRepository : IRepository<RootHouse>
    {
        private IDbConnection db;

        public void Create(RootHouse t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RootHouse> GetAll()
        {
            using (db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                return db.Query<RootHouse>("Select * from House").ToList();
            }
        }

        public void Update(RootHouse t)
        {
            throw new NotImplementedException();
        }
    }
}