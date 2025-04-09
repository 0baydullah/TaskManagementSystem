using DataAccessLayer.Models.Entity;

namespace DataAccessLayer.Models.ViewModel
{
    public class MemberDetailsVM
    {
        public int MemberId { get; set; }
        public int ProjectId { get; set; }
        public string MemberName { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
        public List<TasksVM>? Tasks { get; set; }
        public List<Bug>? Bugs { get; set; }
    }
}
