using Autofac;
using Outreach.Data.Interface;
using Outreach.Data.Repository;
using Outreach.Entities.House;
using Outreach.Entities.Models.Doctor;
using Outreach.Entities.Models.HelthCenter;
using Outreach.Entities.Models.ServiceUser;
using Outreach.Entities.Models.ServiceUser.MedicalAppointment;
using Outreach.Entities.Models.ServiceUser.Menu;
using Outreach.Entities.StaffEmployee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Outreach.Web.IoC
{
    public class DataModule:Module
    {
        private string con;
        public DataModule(string conStr)
        {
            con = conStr;

        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BranchRepository>().As<IRepository<Branch>>().InstancePerRequest();
            builder.RegisterType<CategoryStaffRepository>().As<IRepository<CategoryStaff>>().InstancePerRequest();
            builder.RegisterType<DoctorRepository>().As<IRepository<Doctor>>().InstancePerRequest();
            builder.RegisterType<HealthCenterRepository>().As<IRepository<HealthCenter>>().InstancePerRequest();
            builder.RegisterType<HouseRepository>().As<IRepository<RootHouse>>().InstancePerRequest();
            builder.RegisterType<MedicalAppointmentRepository>().As<IRepository<MedicalAppointment>>().InstancePerRequest();
            builder.RegisterType<MenuRepository>().As<IRepository<Menu>>().InstancePerRequest();
            builder.RegisterType<ServiceUserMenuRepository>().As<IRepository<ServiceUserMenu>>().InstancePerRequest();
            builder.RegisterType<ServiceUserRepository>().As<IRepository<ServiceUser>>().InstancePerRequest();
            builder.RegisterType<StaffRepository>().As<IRepository<Staff>>().InstancePerRequest();

            base.Load(builder);
        }

    }
}