﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.ViewModel
{
    public class ReleaseDetailsVM
    {
        public int ReleaseId { get; set; }
        public int ProjectId { get; set; }
        public string ReleaseName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<SprintVM>? Sprints { get; set; }

    }
}
