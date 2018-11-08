using Dapper;
using Outreach.Data.Interface;
using Outreach.Entities.Models.ServiceUser.MedicalAppointment;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Outreach.Data.Repository
{
    public class MedicalAppointmentRepository:IRepository<MedicalAppointment>
    {
        private IDbConnection db;
        public IEnumerable<MedicalAppointment> GetAll()
        {
            using (db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                return db.Query<MedicalAppointment>("Select * from MedicalAppointment").ToList();
            }
        }
        public void Create(MedicalAppointment medicalAppointment)
        {
            DynamicParameters p = PopulateParams(medicalAppointment);

            using (db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                db.Execute("spr_InsertMedicalAppointment", p, commandType: CommandType.StoredProcedure);
            }

        }
        public void Update(MedicalAppointment medicalAppointment)
        {
            DynamicParameters p = PopulateParams(medicalAppointment);
            p.Add("@Id", medicalAppointment.Id);

            using (db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                db.Execute("spr_UpdateMedicalAppointment", p, commandType: CommandType.StoredProcedure);
            }

        }
        private DynamicParameters PopulateParams(MedicalAppointment b)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@UserId", b.ServiceUserId);
            p.Add("@DoctorId", b.DoctorId);
            p.Add("@AppointmentDate", b.AppointmentDate);
            p.Add("@Reason", b.Reason);
            return p;
        }
    }
}