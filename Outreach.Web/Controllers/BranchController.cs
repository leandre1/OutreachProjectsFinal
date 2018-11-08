using Outreach.Data.Interface;
using Outreach.Data.Repository;
using Outreach.Entities.House;
using Outreach.Entities.HouseBranchViewModel;
using Outreach.Entities.ViewModels.BranchFormViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Outreach.Web.Controllers
{
    public class BranchController : Controller
    {
        private readonly IRepository<Branch> _branchRepository;
        private readonly IRepository<RootHouse> _houseRepository;

        public BranchController(IRepository<Branch> branchRepository, IRepository<RootHouse> houseRepository)
        {
            _branchRepository = branchRepository;
            _houseRepository = houseRepository;
        }
        // GET: Branch
        public ActionResult Index()
        {
            List<Branch> branches = _branchRepository.GetAll().ToList();
            List<RootHouse> houses = _houseRepository.GetAll().ToList();

            var query = (from h in houses
                         join v in branches on h.HouseId equals v.HouseId into g
                         from vt in g.DefaultIfEmpty()
                         select new HouseBranchViewModel() { House = h, Branch = vt }).ToList();

            return View(query);
        }
        public ActionResult NewBranch()
        {
            var houses = _houseRepository.GetAll().ToList();
            var viewModel = new BranchFormViewModel
            {
                Branch = new Branch(),
                Houses = houses
            };
            return View("BranchForm",viewModel);
        }
        [HttpPost]
        public ActionResult Save(Branch branch)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new BranchFormViewModel
                {
                    Branch = branch,
                    Houses = _houseRepository.GetAll().ToList()
                };
                return View("BranchForm", viewModel);
            }
            
            if (branch.Id == 0)
            {
                _branchRepository.Create(branch);
            }
            else
            {
                _branchRepository.Update(branch);
            }
                return RedirectToAction("Index", "Branch");
        }
        public ActionResult Edit(int id)
        {
            var allBranches = _branchRepository.GetAll().ToList();
            var viewModel = new BranchFormViewModel();
            Branch b = allBranches.SingleOrDefault(v => v.Id == id);
            if (b == null)
                return HttpNotFound();
            else
            {
                viewModel.Branch = b;
                viewModel.Houses = _houseRepository.GetAll().ToList();
            }
                
            return View("BranchForm",viewModel);
        }
    }
}