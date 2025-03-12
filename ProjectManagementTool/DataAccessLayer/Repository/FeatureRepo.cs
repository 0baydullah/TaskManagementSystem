using DataAccessLayer.Data;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class FeatureRepo : IFeatureRepo
    {
        private readonly PMSDBContext _context;

        public FeatureRepo(PMSDBContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(Feature feature)
        {
            try
            {
                var existFeature = await _context.Features.FirstOrDefaultAsync(f => f.Name == feature.Name);

                if (existFeature == null)
                {
                    _context.Features.Add(feature);
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
