using Outreach.Data.Interface;
using Outreach.Data.Repository;
using Outreach.Entities.House;
using Outreach.Entities.HouseBranchViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Outreach.Web.Controllers
{
    public class HouseController : Controller
    {
        private readonly IRepository<Branch> _branchRepository;
        private readonly IRepository<RootHouse> _houseRepository;

        public HouseController(IRepository<Branch> branchRepository, IRepository<RootHouse> houseRepository)
        {
            _branchRepository = branchRepository;
            _houseRepository = houseRepository;
        }

        // GET: House
        public ActionResult Index()
        {
            List<Branch> branches = _branchRepository.GetAll().ToList();
            List<RootHouse> houses = _houseRepository.GetAll().ToList();
           
            var query = (from h in houses
                         join v in branches on h.HouseId equals v.HouseId into g
                         from vt in g.DefaultIfEmpty()
                         select new HouseBranchViewModel() { House =  h, Branch = vt} ).ToList();

            return View(query);
        }
        public ActionResult Edit(int id)
        {

            return View();
        }
    }

}