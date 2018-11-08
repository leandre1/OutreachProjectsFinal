using Dapper;
using Outreach.Data.Interface;
using Outreach.Entities.Models.HelthCenter;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Outreach.Data.Repository
{
    public class HealthCenterRepository:IRepository<HealthCenter>
    {
        private IDbConnection db;
        public IEnumerable<HealthCenter> GetAll()
        {
            using (db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                return db.Query<HealthCenter>("Select * from HealthCenter").ToList();
            }
        }
        public void Create(HealthCenter healthCenter)
        {
            DynamicParameters p = PopulateParams(healthCenter);

            using (db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                db.Execute("spr_InsertHealthCenter", p, commandType: CommandType.StoredProcedure);
            }

        }
        public void Update(HealthCenter healthCenter)
        {
            DynamicParameters p = PopulateParams(healthCenter);
            p.Add("@Id", healthCenter.Id);

            using (db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                db.Execute("spr_UpdateHealthCenter", p, commandType: CommandType.StoredProcedure);
            }

        }
        private DynamicParameters PopulateParams(HealthCenter b)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@City", b.City);
            p.Add("@PostCode", b.PostCode);
            p.Add("@CenterName", b.HealthCenterName);
          
            return p;
        }
    }
}