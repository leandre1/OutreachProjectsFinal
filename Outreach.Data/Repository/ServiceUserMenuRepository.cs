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
    public class ServiceUserMenuRepository:IRepository<ServiceUserMenu>
    {
        private IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        public IEnumerable<ServiceUserMenu> GetAll()
        {
            using (db)
            {
                return db.Query<ServiceUserMenu>("Select * from ServiceUserMenus").ToList();
            }
        }
        public void Create(ServiceUserMenu userMenu)
        {
            DynamicParameters p = PopulateParams(userMenu);

            using (db)
            {
                db.Execute("spr_InsertUserMenu", p, commandType: CommandType.StoredProcedure);
            }

        }
        public void Update(ServiceUserMenu userMenu)
        {
            DynamicParameters p = PopulateParams(userMenu);
            p.Add("@id", userMenu.Id);
            using (db)
            {
                db.Execute("spr_UpdateUserMenu", p, commandType: CommandType.StoredProcedure);
            }
        }
        private DynamicParameters PopulateParams(ServiceUserMenu userMenu)
        {
            DynamicParameters p = new DynamicParameters();

            p.Add("@UserId", userMenu.ServiceUserId);
            p.Add("@MenuId", userMenu.MenuId);
            p.Add("@Description", userMenu.Description);

            return p;
        }
    }
}
    
