using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.IService;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using log4net;

namespace BusinessLogicLayer.Service
{
    public class BugService : IBugService
    {
        private readonly IBugRepo _bugRepo;
        private readonly ILog _log = LogManager.GetLogger(typeof(BugService));
        public BugService(IBugRepo bugRepo)
        {
            _bugRepo = bugRepo;
        }
        public void AddBug(int id, BugVM bugVM)
        {
            try
            {
                var bug = new Bug
                {
                    Name = bugVM.Name,
                    Descripton = bugVM.Descripton,
                    BugStatus = bugVM.BugStatus,
                    TaskId = bugVM.TaskId,
                    UserStoryId = id,
                    AssignMembersId = bugVM.AssignMembersId,
                    QaRemarks = bugVM.QaRemarks??"",
                    Priority = bugVM.Priority,
                };
                _bugRepo.AddBug(bug);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw ;
            }
        }

        public void DeleteBug(Bug bug)
        {
            try
            {
                _bugRepo.DeleteBug(bug);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw;
            }
        }

        public List<Bug> GetAllBug()
        {
            try
            {
                var bugs = _bugRepo.GetAllBug();
                return bugs;

            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw;
            }
        }
        public List<Bug> GetAllBugByMember(List<Member> member)
        {
            try
            {
                var bugs = _bugRepo.GetAllBug().Join(member,b=>b.AssignMembersId , m=>m.MemberId , (b, m) =>
                new Bug{
                    Id = b.Id,
                    Name = b.Name,
                    Descripton = b.Descripton,
                    BugStatus = b.BugStatus,
                    UserStoryId = b.UserStoryId,
                    Priority = b.Priority,
                    QaRemarks = b.QaRemarks,
                    AssignMembersId = b.AssignMembersId,
                }).ToList();
                return bugs;

            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw;
            }
        }

        public List<Bug> GetAllBugOfStory(int id)
        {
            try
            {
                var bugs = _bugRepo.GetAllBugOfStory(id);
                return bugs;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw;
            }
        }

        public Bug GetBug(int id)
        {
            try
            {
                var bug = _bugRepo.GetBug(id);
                return bug;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                throw;
            }
        }

        public void UpdateBug(int id, BugVM  bug)
        {
            try
            {
                var existingBug = GetBug(id);

                existingBug.BugStatus = bug.BugStatus;
                existingBug.QaRemarks = bug.QaRemarks;
                existingBug.Descripton = bug.Descripton;
                existingBug.Name = bug.Name;
                existingBug.Priority = bug.Priority;
                existingBug.AssignMembersId = bug.AssignMembersId;
                existingBug.TaskId = bug.TaskId;

                _bugRepo.UpdateBug(existingBug);
            }
            catch(Exception ex)
            {
                _log.Error(ex.Message);
                throw;
            }
        }

        public void UpdateBug(Bug bug)
        {
            try
            {
                _bugRepo.UpdateBug(bug);
            }
            catch(Exception ex)
            {
                _log.Error(ex.Message);
                throw;
            }
        }
    }
}
