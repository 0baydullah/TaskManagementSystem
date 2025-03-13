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

        public async Task<List<FeatureWithMemberReleaseVM>> GetAllFeature()
        {
            var features = await _context.Features.Join(_context.Members, f => f.MemberId, m=> m.MemberId, (f,m) => new
            {
                FeatureId = f.FeatureId,
                FeatureName = f.Name,
                Description = f.Description,
                EstimatedPoint = f.EstimatedPoint,
                ReleaseId = f.ReleaseId,
                MemberId = f.MemberId,
                Tag = f.Tag,
                Email = m.Email 
            }).Join(_context.Users, f=> f.Email, u => u.Email, (f,u) => new
            {
                FeatureId = f.FeatureId,
                FeatureName = f.FeatureName,
                Description = f.Description,
                EstimatedPoint = f.EstimatedPoint,
                ReleaseId = f.ReleaseId,
                MemberId = f.MemberId,
                Tag = f.Tag,
                MemberName = u.Name,
            }).Join(_context.Releases, f=> f.ReleaseId, r => r.ReleaseId,(f,r) => new FeatureWithMemberReleaseVM
            {
                FeatureId = f.FeatureId,
                Name = f.FeatureName,
                Description = f.Description,
                EstimatedPoint = f.EstimatedPoint,
                ReleaseId = f.ReleaseId,
                MemberId = f.MemberId,
                Tag = f.Tag,
                MemberName = f.MemberName,
                ReleaseName = r.ReleaseName
            }).ToListAsync();
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
