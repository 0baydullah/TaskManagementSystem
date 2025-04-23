using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using NSubstitute;

namespace BusinessLogicLayer.Test.Service.BugServiceTest
{
    [ExcludeFromCodeCoverage]
    public class BugServiceTest : BugServiceBaseTest
    {
        #region AddBug Test
        [Fact]
        public void AddBug_IdAndValidBugVM_CallRepoAddBug()
        {
            // Arrange
            const int id = 57;
            var bugVM = new BugVM
            {
                Name = "Test Bug",
                Descripton = "Test Description",
                BugStatus = 9,
                AssignMembersId = 67,
                TaskId = 1267,
                QaRemarks = "Test Remarks",
                Priority = 5,
                CreatedBy = 7
            };

            // Act
            _sut.AddBug(id, bugVM);

            // Assert
            _bugRepoMock.Received().AddBug(Arg.Any<Bug>());
        }
        #endregion

        #region DeleteBug Test
        [Fact]
        public void DeleteBug_ValidBug_CallRepoDeleteBug()
        {
            // Arrange
            var bug = new Bug
            {
                Id = 1,
                Name = "Test Bug",
                Descripton = "Test Description",
                BugStatus = 1,
                AssignMembersId = 1,
                TaskId = 1,
                UserStoryId = 1,
                QaRemarks = "Test Remarks",
                Priority = 1,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                CreatedBy = 1,
                BugReopen = 0
            };

            // Act
            _sut.DeleteBug(bug);

            // Assert
            _bugRepoMock.Received().DeleteBug(bug);
        }
        #endregion
    }
}
