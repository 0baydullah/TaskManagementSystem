using BusinessLogicLayer.Service;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models.Entity;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Test.Service.ProjectInfoTest
{
    public class ProjectInfoServiceTests
    {
        private readonly IProjectInfoRepo _projectInfoRepo;
        private readonly ProjectInfoService _projectInfoService;

        public ProjectInfoServiceTests()
        {
            // Initialize substitutes (mocks) for dependencies
            _projectInfoRepo = Substitute.For<IProjectInfoRepo>();
            var memberRepo = Substitute.For<IMemberRepo>();
            var roleRepo = Substitute.For<IRoleRepo>();
            var releaseRepo = Substitute.For<IReleaseRepo>();
            var sprintRepo = Substitute.For<ISprintRepo>();
            var userStoryRepo = Substitute.For<IUserStoryRepo>();
            var featureRepo = Substitute.For<IFeatureRepo>();
            var tasksRepo = Substitute.For<ITasksRepo>();
            var bugRepo = Substitute.For<IBugRepo>();
            var activityRepo = Substitute.For<IActivityRepo>();

            // Initialize the service with mocked dependencies
            _projectInfoService = new ProjectInfoService(
                _projectInfoRepo,
                memberRepo,
                roleRepo,
                releaseRepo,
                sprintRepo,
                userStoryRepo,
                featureRepo,
                tasksRepo,
                bugRepo,
                activityRepo);
        }

        [Fact]
        public void GetProjectInfo_ValidId_ReturnsProjectInfo()
        {
            // Arrange
            int projectId = 1;
            var expectedProjectInfo = new ProjectInfo
            {
                ProjectId = projectId,
                Name = "Test Project",
                Key = "TP",
                Description = "This is a test project",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(30),
                CompanyName = "Test Company",
                ProjectOwnerId = 1
            };

            _projectInfoRepo.GetProjectInfo(projectId).Returns(expectedProjectInfo);

            // Act
            var result = _projectInfoService.GetProjectInfo(projectId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedProjectInfo.ProjectId, result.ProjectId);
            Assert.Equal(expectedProjectInfo.Name, result.Name);
            Assert.Equal(expectedProjectInfo.Key, result.Key);
        }

        [Fact]
        public void GetProjectInfo_InvalidId_ReturnsNull()
        {
            // Arrange
            int invalidProjectId = 999;
            _projectInfoRepo.GetProjectInfo(invalidProjectId).Returns((ProjectInfo)null);

            // Act
            var result = _projectInfoService.GetProjectInfo(invalidProjectId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetProjectInfo_ExceptionThrown_ThrowsException()
        {
            // Arrange
            int projectId = 1;
            _projectInfoRepo.GetProjectInfo(projectId).Throws(new Exception("Database error"));

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _projectInfoService.GetProjectInfo(projectId));
            Assert.Equal("Database error", exception.Message);
        }
    }

}

