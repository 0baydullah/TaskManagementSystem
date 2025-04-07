using BusinessLogicLayer.IService;
using DataAccessLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Service
{
    public class FilterService : IFilterService
    {
        private readonly IFilterRepo _filterRepo;

        public FilterService(IFilterRepo filterRepo)
        {
            _filterRepo = filterRepo;
        }

        public void InProgress(string status, int projectId)
        {
            _filterRepo.InProgress(status, projectId);
            throw new NotImplementedException();
        }
    }
}
