﻿using DataAccessLayer.Data;
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
            try
            {
                var existFeature = await _context.Features.FirstOrDefaultAsync(f => f.Name.Equals(feature.Name) && f.ProjectId == feature.ProjectId);

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
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<FeatureWithMemberReleaseVM>> GetAllFeature(int projectId)
        {
            try
            {
                var features = await _context.Features.Where(feature => feature.ProjectId == projectId).Join(_context.Members, f => f.MemberId, m => m.MemberId, (f, m) => new
                {
                    FeatureId = f.FeatureId,
                    FeatureName = f.Name,
                    Description = f.Description,
                    EstimatedPoint = f.EstimatedPoint,
                    MemberId = f.MemberId,
                    ProjectId = f.ProjectId,
                    ReleaseId = f.ReleaseId,
                    Tag = f.Tag,
                    Email = m.Email
                }).Join(_context.Users, f => f.Email, u => u.Email, (f, u) => new
                {
                    FeatureId = f.FeatureId,
                    FeatureName = f.FeatureName,
                    Description = f.Description,
                    EstimatedPoint = f.EstimatedPoint,
                    MemberId = f.MemberId,
                    ProjectId = f.ProjectId,
                    ReleaseId = f.ReleaseId,
                    Tag = f.Tag,
                    MemberName = u.Name,

                }).GroupJoin(_context.Releases, f => f.ReleaseId, r => r.ReleaseId, (f, r) => new { f, r }).SelectMany(
                    x => x.r.DefaultIfEmpty(), (x, r) => new FeatureWithMemberReleaseVM
                    {
                        FeatureId = x.f.FeatureId,
                        Name = x.f.FeatureName,
                        Description = x.f.Description,
                        EstimatedPoint = x.f.EstimatedPoint,
                        ReleaseId = x.f.ReleaseId,
                        MemberId = x.f.MemberId,
                        ProjectId = x.f.ProjectId,
                        Tag = x.f.Tag,
                        MemberName = x.f.MemberName,
                        ReleaseName = r != null ? r.ReleaseName : "No release"
                    }).ToListAsync();

                return features;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<Feature> GetFeatureById(int id)
        {
            try
            {
                var feature = await _context.Features.FirstOrDefaultAsync(f => f.FeatureId.Equals(id));
                return feature;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Feature> GetFeatureByName(string name, int id, int projectId) 
        {
            try
            {
                var feature = await _context.Features.FirstOrDefaultAsync(f => f.Name == name && f.FeatureId != id && f.ProjectId == projectId);
                return feature;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateFeature(Feature feature)
        {
            try
            {
                _context.Features.Update(feature);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteFeature(Feature feature)
        {
            try
            {
                _context.Features.Remove(feature);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
