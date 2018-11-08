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
    public class CategoryStaffRepository: IRepository<CategoryStaff>
    {
        private IDbConnection db;

        public void Create(CategoryStaff t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CategoryStaff> GetAll()
        {
            using (db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                return db.Query<CategoryStaff>("Select * from CategoryStaff").ToList();
            }
        }

        public void Update(CategoryStaff t)
        {
            throw new NotImplementedException();
        }
    }
}