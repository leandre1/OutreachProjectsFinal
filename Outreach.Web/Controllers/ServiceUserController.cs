using Outreach.Common.DataTableServerSide;
using Outreach.Data.Interface;
using Outreach.Data.Repository;
using Outreach.Entities.DTO.ServiceUserDto;
using Outreach.Entities.House;
using Outreach.Entities.Models.Doctor;
using Outreach.Entities.Models.HelthCenter;
using Outreach.Entities.Models.ServiceUser;
using Outreach.Entities.Models.ServiceUser.MedicalAppointment;
using Outreach.Entities.Models.ServiceUser.Menu;
using Outreach.Entities.StaffEmployee;
using Outreach.Entities.ViewModels.MedicalAppointmentViewModel;
using Outreach.Entities.ViewModels.ServiceUserFormViewModel;
using Outreach.Entities.ViewModels.ServiceUserViewModel;
using Outreach.Entities.ViewModels.ServiceUserViewModel.ServiceUserBranch;
using Outreach.Entities.ViewModels.ServiceUserViewModel.ServiceUserProfileViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Outreach.Web.Controllers
{
    public class ServiceUserController : Controller
    {
        private readonly IRepository<ServiceUser> _serviceUserRepository;
        private readonly IRepository<Branch> _branchRepository;
        private readonly IRepository<Staff> _staffRepository;
        private readonly IRepository<Menu> _menuRepository;
        private readonly IRepository<ServiceUserMenu> _serviceUserMenuRepository;
        private readonly IRepository<MedicalAppointment> _medicalAppointmentRepository;
        private readonly IRepository<HealthCenter> _healthCenterRepository;
        private readonly IRepository<Doctor> _doctorRepository;

        List<ServiceUser> repo = new ServiceUserRepository().GetAll().ToList();
        List<ServiceUserIndexDto> sList = new List<ServiceUserIndexDto>();
        List<Branch> repoBranch = new BranchRepository().GetAll().ToList();

        public ServiceUserController(IRepository<ServiceUser> serviceUserRepository,
            IRepository<Branch> branchRepository, IRepository<Staff> staffRepository,
            IRepository<Menu> menuRepository,
            IRepository<MedicalAppointment> medicalAppointmentRepository, IRepository<HealthCenter> healthCenterRepository,
            IRepository<Doctor> doctorRepository, IRepository<ServiceUserMenu> serviceUserMenuRepository)
        {
            _serviceUserRepository = serviceUserRepository;
            _branchRepository = branchRepository;
            _staffRepository = staffRepository;
            _menuRepository = menuRepository;
            _serviceUserMenuRepository = serviceUserMenuRepository;
            _medicalAppointmentRepository = medicalAppointmentRepository;
            _healthCenterRepository = healthCenterRepository;
            _doctorRepository = doctorRepository;

        }

        // GET: ServiceUser
        public ActionResult Index()
        {

            return View("Index");
        }
        /// <summary>
        /// This function is used to return Data from Database in Json format.
        /// it handles Search and pagination 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetServiceUserData(DataTablesParam param)
        {
        
            int pageNo = CalculatePageNo(param);
            int totalCount = 0;

            if (param.sSearch != null)
            {
                totalCount = GetList(param).Count();

                sList = GetList(param).Select(user => new ServiceUserIndexDto
                            {
                                Id = user.Id,
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                BirthDate = user.BirthDate
                            }).ToList();
            }
            else
            {
                totalCount = _serviceUserRepository.GetAll().ToList().Count;
                sList = _serviceUserRepository.GetAll().ToList()
                    .OrderBy(x => x.Id)
                    .Skip((pageNo - 1) * param.iDisplayLength)
                    .Take(param.iDisplayLength)
                    .Select(user => new ServiceUserIndexDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    BirthDate = user.BirthDate
                }).ToList();
            }
            return Json(new
            {
                aaData = sList,
                sEcho = param.sEcho,
                iTotalDisplayRecords = totalCount,
                iTotalRecords = totalCount
            },
               JsonRequestBehavior.AllowGet);
        }
        private int CalculatePageNo(DataTablesParam param)
        {
            int pageNo = 1;

            if (param.iDisplayStart >= param.iDisplayLength)
            {
                pageNo = (param.iDisplayStart / param.iDisplayLength) + 1;
            }
            return pageNo;
        }

        private List<ServiceUser> GetList(DataTablesParam param)
        {
            return _serviceUserRepository.GetAll()
                    .ToList()
                    .Where(x => x.FirstName.ToLower().Contains(param.sSearch.ToLower())
                           || x.LastName.ToLower().Contains(param.sSearch)).ToList();
        }

        public ActionResult NewServiceUser()
        {
            //var repo = new StaffRepository().GetStaff().ToList();

            var viewModel = new ServiceUserFormVM
            {
                ServiceUser = new ServiceUser()

            };
            return View("ServiceUserForm", viewModel);
        }

        public JsonResult Populate(string prefix)
        {
            
            prefix = Request.QueryString["term"];
            //var staffGroup = (from c in staffList
            //                  where c.FirstName.StartsWith(prefix) 
            //                  select new { Name = c.FirstName + " " + c.LastName, Id = c.Id }).Distinct().ToList();
            var staffGroup = _staffRepository.GetAll()
                             .ToList()
                             .Where(m => prefix == null || m.FirstName.ToUpper()
                              .Contains(prefix.ToUpper()))
                             .Select(c => new { Name = c.FirstName + " " + c.LastName, Id = c.Id })
                             .Distinct().ToList();

            return Json(staffGroup, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Save(ServiceUser serviceUser)
        {

            
            if (!ModelState.IsValid)
            {
                var viewModel = new ServiceUserFormVM
                {
                    ServiceUser = serviceUser,
                    staff = _staffRepository
                            .GetAll()
                            .SingleOrDefault(n => n.Id == serviceUser.StaffId)

                };
                return View("ServiceUserForm", viewModel);
            }
            if (serviceUser.Id == 0)
            {
                _serviceUserRepository.Create(serviceUser);
            }
            else
            {
                _serviceUserRepository.Update(serviceUser);
            }
            return RedirectToAction("Index", "ServiceUser");
        }
        public ActionResult Edit(int id)
        {
            var viewModel = new ServiceUserFormVM();
            ServiceUser b = _serviceUserRepository
                            .GetAll()
                            .ToList()
                            .SingleOrDefault(v => v.Id == id);
            if (b == null)
                return HttpNotFound();
            else
            {
                viewModel.ServiceUser = b;
                viewModel.staff = _staffRepository.GetAll().SingleOrDefault(s => s.Id == b.staff.Id);
            }

            return View("ServiceUserForm", viewModel);
        }

        public ActionResult ShowProfile(int id)
        {
         

            ServiceUser user = GetServiceUserAndTheirBranch().FirstOrDefault(x => x.User.Id == id).User;
            Branch branch = repoBranch.FirstOrDefault(x => x.Id == user.BranchId);
            Session["user"] = user;
            Session["userBranch"] = branch;

            var userMenu = _serviceUserMenuRepository
                            .GetAll()
                            .ToList()
                            .Where(u => u.ServiceUserId == user.Id).ToList();
            
            var vm = new ServiceUserProfileVM {
                UserMenu = userMenu,
                Menus = _menuRepository.GetAll().ToList(),
                Branch  = branch,
                User = user
            };

            return View("ServiceUserProfile", vm);
        }
        public ActionResult DisplayMenuContent(int id)
        {
            ServiceUser user =  (ServiceUser)Session["user"];
            var ServiceUserMenu = _serviceUserMenuRepository
                                    .GetAll()
                                    .ToList()
                                    .FirstOrDefault(u => u.MenuId == id && u.ServiceUserId == user.Id);
            var vm = new ServiceUserMenuVM()
            {
                User = user,
                UserMenu = ServiceUserMenu

            };
            switch (id)
            {
                    
                case 1:
                 return PartialView("_Background", vm);
                case 2:
                    return PartialView("_Health", vm);
                case 3:
                    return PartialView("_Food", vm);
                case 4:
                    return PartialView("_Family", vm);
                case 5:
                    return PartialView("_Activity", vm);
            }
            
        return new EmptyResult();
        }
        private List<ServiceUserBranchVM> GetServiceUserAndTheirBranch()
        {
            var query = (from s in _serviceUserRepository.GetAll().ToList()
                         join b in _branchRepository.GetAll().ToList() on s.BranchId equals b.Id
                      select new ServiceUserBranchVM { Branch = b, User = s }).ToList();
            return query;
        }
        
        public ActionResult MedicalAppointment(int Id)
        {
            
            ServiceUser user = (ServiceUser)Session["user"];

            var data = (from d in _doctorRepository.GetAll().ToList()
                        join h in _healthCenterRepository.GetAll().ToList() on d.HealthCenterId equals h.Id
                        join m in _medicalAppointmentRepository.GetAll().ToList() on d.Id equals m.DoctorId
                        select new MedicalAppointmentVM
                        {
                            HealthCenter = h,
                            doctor = d,
                            MedicalAppointments = _medicalAppointmentRepository
                                .GetAll().FirstOrDefault(x => x.ServiceUserId == user.Id && x.DoctorId == d.Id)
                        }

                        
                
                        ).Distinct().ToList();


            return PartialView("_MedicalAppointment", data);
        }


    }
}