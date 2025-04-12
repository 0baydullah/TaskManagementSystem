using DataAccessLayer.Models.Entity;

namespace DataAccessLayer.Models.ViewModel
{
    public class MemberDetailsVM
    {
        public int MemberId { get; set; }
        public int ProjectId { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; }
        public List<Tasks>? Tasks { get; set; }
        public List<Bug>? Bugs { get; set; }
        public Dictionary<int, string>? RoleList { get; set; }
        public Dictionary<int, string>? StatusList { get; set; }
        public Dictionary<int, string>? PriorityList { get; set; }
        public int TaskAll { get; set; }
        public int BugAll { get; set; }
        public Dictionary<int, int>? TaskDict { get; set; }
        public Dictionary<int, int>? BugDict { get; set; }

    }
}
