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

        #region AddBug exception Test
        [Fact]
        public void AddBug_IdAndBugVM_ExceptionThrown()
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

            _bugRepoMock.When(x => x.AddBug(Arg.Any<Bug>())).Do(x => throw new Exception("Test exception"));

            // Act & Assert
            Assert.Throws<Exception>(() => _sut.AddBug(id, bugVM));
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

        #region DeleteBug exception Test
        [Fact]
        public void DeleteBug_InvalidBug_ExceptionThrown()
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
            _bugRepoMock.When(x => x.DeleteBug(bug)).Do(x => throw new Exception("Test exception"));
            // Act & Assert
            Assert.Throws<Exception>(() => _sut.DeleteBug(bug));
        }
        #endregion

        #region GetAllBug Test
        [Fact]
        public void GetAllBug_ValidCall_ListOfBugs()
        {
            // Arrange
            var bugs = new List<Bug>
            {
                new Bug { Id = 1, Name = "Test Bug 1", Descripton = "Test Description 1", BugStatus = 2, UserStoryId = 1, Priority = 3 ,QaRemarks = "sdjhlksdfjhkasjhkdv", AssignMembersId =34 },
                new Bug { Id = 2, Name = "Test Bug 2", Descripton = "Test Description 2", BugStatus = 2, UserStoryId = 1, Priority = 3 ,QaRemarks = "sdjhlksdfjhkasjhkdv", AssignMembersId =34  }
            };
            _bugRepoMock.GetAllBug().Returns(bugs);

            // Act
            var result = _sut.GetAllBug();

            // Assert
            Assert.Equal(2, result.Count);
        }
        #endregion
    }
}
