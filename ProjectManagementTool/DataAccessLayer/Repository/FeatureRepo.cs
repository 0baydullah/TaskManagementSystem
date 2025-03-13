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
        public async Task<bool> CreateFeature(Feature feature)
        {
            var existFeature = await _context.Features.FirstOrDefaultAsync(f => f.Name.Equals(feature.Name));

            if (existFeature == null)
            {
                _context.Features.Add(feature);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }  
        }

        public async Task<List<Feature>> GetAllFeature()
        {
            var features = await _context.Features.ToListAsync();
            return features;
        }

        public async Task<Feature> GetFeatureById(int id)
        {
            var feature = await _context.Features.FirstOrDefaultAsync(f => f.FeatureId.Equals(id));
            return feature;
        }

        public async Task<Feature> GetFeatureByName(string name, int id) 
        {
            var feature = await _context.Features.FirstOrDefaultAsync(f => f.Name == name && f.FeatureId != id);
            return feature;
        }

        public async Task<bool> UpdateFeature(Feature feature)
        {
              _context.Features.Update(feature);
              await _context.SaveChangesAsync();
              return true;
        }

        public async Task<bool> DeleteFeature(Feature feature)
        {
            _context.Features.Remove(feature);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
