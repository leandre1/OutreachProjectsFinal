using Dapper;
using Outreach.Data.Interface;
using Outreach.Entities.Models.ServiceUser.Menu;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Outreach.Data.Repository
{
    public class MenuRepository:IRepository<Menu>
    {
        private IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        public IEnumerable<Menu> GetAll()
        {
            using (db)
            {
                return db.Query<Menu>("Select * from Menu").ToList();
            }
        }
        public void Create(Menu menu)
        {
            DynamicParameters p = PopulateParams(menu);

            using (db)
            {
                db.Execute("spr_InsertMenu", p, commandType: CommandType.StoredProcedure);
            }

        }
        public void Update(Menu menu)
        {
            DynamicParameters p = PopulateParams(menu);
            p.Add("@id", menu.Id);
            using (db)
            {
                db.Execute("spr_UpdateMenu", p, commandType: CommandType.StoredProcedure);
            }
        }
        private DynamicParameters PopulateParams(Menu menu)
        {
            DynamicParameters p = new DynamicParameters();

            p.Add("@Name", menu.Name);
            
            return p;
        }
    }
}
