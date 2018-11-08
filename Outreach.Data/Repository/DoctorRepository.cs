using Dapper;
using Outreach.Data.Interface;
using Outreach.Entities.Models.Doctor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Outreach.Data.Repository
{
    public class DoctorRepository:IRepository<Doctor>
    {
        private IDbConnection db;
        public IEnumerable<Doctor> GetAll()
        {
            using (db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                return db.Query<Doctor>("Select * from Doctor").ToList();
            }
        }
        public void Create(Doctor doctor)
        {
            DynamicParameters p = PopulateParams(doctor);

            using (db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                db.Execute("spr_InsertDoctor", p, commandType: CommandType.StoredProcedure);
            }

        }
        public void Update(Doctor doctor)
        {
            DynamicParameters p = PopulateParams(doctor);
            p.Add("@Id", doctor.Id);

            using (db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                db.Execute("spr_UpdateDoctor", p, commandType: CommandType.StoredProcedure);
            }

        }
        private DynamicParameters PopulateParams(Doctor b)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@FName", b.FirstName);
            p.Add("@LName", b.Lastname);
            p.Add("@Specialist", b.Specialist);
            p.Add("@HealthCenterId", b.HealthCenterId);
            return p;
        }
    }
}