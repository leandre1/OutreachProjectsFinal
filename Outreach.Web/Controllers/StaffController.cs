using Outreach.Data.Interface;
using Outreach.Data.Repository;
using Outreach.Entities.StaffEmployee;
using Outreach.Entities.StaffFormViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Outreach.Web.Controllers
{
    public class StaffController : Controller
    {
        private readonly IRepository<Staff> _staffRepository;
        private readonly IRepository<CategoryStaff> _categoryStaffRepository;

        public StaffController(IRepository<Staff> staffRepository, IRepository<CategoryStaff> categoryStaffRepository)
        {
            _staffRepository = staffRepository;
            _categoryStaffRepository = categoryStaffRepository;
        }
        // GET: Staff
        public ActionResult Index()
        {

            var query = (from sr in _staffRepository.GetAll().ToList()
                        join cr in _categoryStaffRepository.GetAll().ToList()
                        on sr.CategoryId equals cr.CategoryId into g
                        from vt in g.DefaultIfEmpty()
                        select new StaffVM { Category = vt, Staff = sr }).ToList();

            return View(query);
        }
        public ActionResult NewStaff()
        {
            var categoryRepo = _categoryStaffRepository.GetAll().ToList();
            var staffVM = new StaffFormViewModel {  Staff= new Staff(), Categories = categoryRepo};
            return View("StaffForm", staffVM);
        }
        public ActionResult New()
        {
           
            return View("StaffForm1");
        }
        [HttpGet]
        public ActionResult GetCategories(string query)
        {
            //query = Request.QueryString["term"];
            var categories = _categoryStaffRepository.GetAll()
                .Where(x => x.Category.StartsWith(query))
                .ToList();

            return Json(categories, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Save(Staff staff)
        {
            var staffRepo = new StaffRepository();
            if(!ModelState.IsValid)
            {
                var viewModel = new StaffFormViewModel { Categories = new CategoryStaffRepository().GetAll().ToList() };
            
            return View("StaffForm", viewModel);
            }
            if(staff.Id == 0)
            {
                _staffRepository.Create(staff);
            }
            else
            {
                _staffRepository.Update(staff);
            }
            return RedirectToAction("Index", "Staff");
        }
        public ActionResult Edit(int id)
        {
            var allStaff = new StaffRepository().GetAll().ToList();

            var staff = _staffRepository.GetAll()
                .ToList()
                .SingleOrDefault(s => s.Id == id);
            if (staff == null)
                return HttpNotFound();
            var viewModel = new StaffFormViewModel { Staff = staff, Categories = _categoryStaffRepository.GetAll().ToList() };
              
            return View("StaffForm", viewModel);
        }
        
    }
}