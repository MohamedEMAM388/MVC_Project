using GymManagementBLL.Services.classes;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberShipViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.WebSockets;

namespace GymManagementPL.Controllers
{
    public class MemberShipController : Controller
    {
        private readonly IMemberShipServies memberShipServies;

        public MemberShipController(IMemberShipServies memberShipServies)
        {
            this.memberShipServies = memberShipServies;
        }
        public ActionResult Index()
        {
            var memberShips = memberShipServies.GetAll();
            return View(memberShips);
        }

        public ActionResult Create() {
            LoadMemberList();
            LoadPlanList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateMemberShipViewModel createmembership) {



            //if (!ModelState.IsValid) {

            //    LoadMemberList();
            //    LoadPlanList();

            //    return View(createmembership);
            //}

            bool isCreated = memberShipServies.CreateMemberShip(createmembership);
            if (isCreated) {
                TempData["SuccessMessage"] = "Membership Created Successfully";
                return  RedirectToAction(nameof(Index));    
            }
            else {
                TempData["ErrorMessage"] = "Membership Creation Failed";
                 LoadMemberList();
                 LoadPlanList();
                return View(createmembership);
            }


        }


        #region HelperMethods

        private void LoadMemberList() {
            var members = memberShipServies.GetMembersFroSelect();
            ViewBag.members = new SelectList(members, "Id", "Name");

        }

        private void LoadPlanList() {

            var plans = memberShipServies.GetPlansForSelect();
            ViewBag.plans = new SelectList(plans, "Id", "Name");

        }

        #endregion


    }
}
