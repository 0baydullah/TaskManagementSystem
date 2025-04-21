using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;

namespace BusinessLogicLayer.IService
{
    public interface ISubTaskService
    {
        public void AddSubTask(SubTask subTask);
        public void UpdateSubTask(SubTask subTask);
        public void DeleteSubTask(SubTask subTask);     
        public SubTask GetSubTask(int id);
        public List<SubTask> GetAllSubTask();
        public List<SubTask> GetAllSubTaskByMember(List<Member>? member);
        public List<SubTask> GetAllSubTaskByReviewr(List<Member>? member);
        public List<SubTask> GetAllSubTaskByQA(List<Member>? member);
        public List<SubTaskVM> GetAllSubTaskByTask(int id);
    }
}
