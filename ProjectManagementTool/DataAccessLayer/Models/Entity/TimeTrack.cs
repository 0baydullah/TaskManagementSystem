using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Entity
{
    public class TimeTrack
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TaskId { get; set; }
        [Required]
        public int SubTaskId { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
        public DateTime TodaysTime { get; set; }
        [Required]
        public long TotalTime { get; set; }
        [Required]
        public string TrackingStatus { get; set; } = "Stopped";
    }
}
