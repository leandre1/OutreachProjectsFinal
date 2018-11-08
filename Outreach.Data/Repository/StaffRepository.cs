using Dapper;
using Outreach.Data.Interface;
using Outreach.Entities.StaffEmployee;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Outreach.Data.Repository
{
    public class StaffRepository:IRepository<Staff>
    {
        private IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        public IEnumerable<Staff> GetAll()
        {
            using(db) 
            {
                return db.Query<Staff>("Select * from Staff").ToList();
            }
        }
        public void Create(Staff staff)
        {
            DynamicParameters p = PopulateParams(staff);
            
            using (db)
            {
                db.Execute("spr_InsertStaff", p, commandType: CommandType.StoredProcedure);
            }

        }
        public void Update(Staff staff)
        {
            DynamicParameters p = PopulateParams(staff);
            p.Add("@id", staff.Id);
            using (db)
            {
                db.Execute("spr_UpdateStaff", p, commandType: CommandType.StoredProcedure);
            }
        }
        private DynamicParameters PopulateParams(Staff staff)
        {
            DynamicParameters p = new DynamicParameters();
            
            p.Add("@FirstName", staff.FirstName);
            p.Add("@LastName", staff.LastName);
            p.Add("@DateReg", staff.DateOfRegistration);
            p.Add("@Dob", staff.BirthDate);
            p.Add("@CatId", staff.CategoryId);
            return p;
        }
    }
}