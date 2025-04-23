using BusinessLogicLayer.Service;
using BusinessLogicLayer.Test.Service.ProjectInfoServiceTest;
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
    public class ProjectInfoServiceTests: ProjectInfoServiceBaseTest
    {

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

            _sut.GetProjectInfo(projectId).Returns(expectedProjectInfo);

            // Act
            var result = _sut.GetProjectInfo(projectId);

            // Assert
          
            Assert.Equal(expectedProjectInfo.Key, result.Key);
        }

        [Fact]
        public void GetProjectInfo_InvalidId_ReturnsNull()
        {
            // Arrange
            int invalidProjectId = 999;
            _sut.GetProjectInfo(invalidProjectId).Returns((ProjectInfo)null);

            // Act
            var result = _sut.GetProjectInfo(invalidProjectId);

            // Assert
            Assert.Null(result);
        }
    }

}

