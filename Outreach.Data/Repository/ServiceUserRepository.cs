using Dapper;
using Outreach.Data.Interface;
using Outreach.Entities.DTO.ServiceUserDto;
using Outreach.Entities.Models.ServiceUser;
using Outreach.Entities.SuperModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Outreach.Data.Repository
{
    public class ServiceUserRepository:IRepository<ServiceUser>
    {
        private IDbConnection db ;
        public IEnumerable<ServiceUser> GetAll()
        {
            using (db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                return db.Query<ServiceUser>("Select * from ServiceUser").ToList();
            }
        }
        public void Create(ServiceUser user)
        {
            DynamicParameters p = PopulateParams(user);

            using (db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                db.Execute("spr_InsertServiceUser", p, commandType: CommandType.StoredProcedure);
            }

        }
        public void Update(ServiceUser user)
        {
            DynamicParameters p = PopulateParams(user);
            p.Add("@id", user.Id);
            using (db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                db.Execute("spr_UpdateServiceUser", p, commandType: CommandType.StoredProcedure);
            }
        }
        private DynamicParameters PopulateParams(ServiceUser user)
        {
            DynamicParameters p = new DynamicParameters();

            p.Add("@FirstName", user.FirstName);
            p.Add("@LastName", user.LastName);
            p.Add("@Dob", user.BirthDate);
            p.Add("@ImageUrl", user.ImageUrl);
            p.Add("@DoctorId", user.DoctorId);
            p.Add("@StaffId", user.StaffId);
            return p;
        }
    }
}