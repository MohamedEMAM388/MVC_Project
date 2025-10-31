using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberShipViewModel;
using GymManagementDAL.Data.Repositories.Interfaces;
using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.classes
{
    public class MemberShipServies : IMemberShipServies
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public MemberShipServies(IUnitOfWork unitOfWork , IMapper mapper) {
            this._unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public bool CreateMemberShip(CreateMemberShipViewModel createMemberShipViewModel)
        {

            var memberRepo = _unitOfWork.GetRepository<Member>();
            var planRepo = _unitOfWork.GetRepository<Plan>();
            var memberShipRepo = _unitOfWork.GetRepository<MemberShip>();

            // check if member exist
            var member = memberRepo.GetById(createMemberShipViewModel.MemberId);
            if (member is null) return false;

            // check if plan exist
            var plan = planRepo.GetById(createMemberShipViewModel.PlanId);
            if (plan is null) return false;

            // check if member has active membership
            bool hasActiveMembership = memberShipRepo.GetAll(X => X.MemebreId == member.Id && X.EndDate > DateTime.Now).Any();
            if (hasActiveMembership) return false;

            if (!plan.IsActive) return false;

            var membership = mapper.Map<MemberShip>(createMemberShipViewModel);
            _unitOfWork.GetRepository<MemberShip>().Add(membership);
            return _unitOfWork.SaveChanges() > 0;



        }

        public IEnumerable<MemberShipViewModel> GetAll()
        {
            var MemberShips = _unitOfWork.MemberShipRepository.GetMemberShipsWithLodedData();

            if (MemberShips is null || !MemberShips.Any()) return [];

           var membershipviewmodel = mapper.Map<IEnumerable<MemberShipViewModel>>(MemberShips);
            return membershipviewmodel;


        }

        public IEnumerable<MemberSelectViewModel> GetMembersFroSelect()
        {
            var members = _unitOfWork.GetRepository<Member>().GetAll();
            return mapper.Map<IEnumerable<MemberSelectViewModel>>(members);
        }

        public IEnumerable<PlanSelectViewModel> GetPlansForSelect()
        {
            var plans = _unitOfWork.GetRepository<Plan>().GetAll();
            return mapper.Map<IEnumerable<PlanSelectViewModel>>(plans);
        }




        public bool RemoveMemberShip(int membershipid)
        {
            throw new NotImplementedException();
        }

 
    }
}
