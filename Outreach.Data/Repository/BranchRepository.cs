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
    public class BranchRepository : IRepository<Branch>
    {
        private IDbConnection db;
        public IEnumerable<Branch> GetAll()
        {
            using (db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                return db.Query<Branch>("Select * from Branch").ToList();
            }
        }
        public void Create(Branch branch)
        {
            DynamicParameters p = PopulateParams(branch);

            using (db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                db.Execute("spr_InsertBranch", p, commandType: CommandType.StoredProcedure);
            }
        }
        public void Update(Branch branch)
        {
            DynamicParameters p = PopulateParams(branch);
            p.Add("@Id", branch.Id);

            using (db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                db.Execute("spr_UpdateBranch", p, commandType: CommandType.StoredProcedure);
            }

        }
        private DynamicParameters PopulateParams(Branch b)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@Name", b.Name);
            p.Add("@Location", b.Location);
            p.Add("@HouseId", b.HouseId);
            return p;
        }
    }
}