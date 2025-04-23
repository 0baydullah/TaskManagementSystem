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

        #region GetAllBugByMember Exception Test

        [Fact]
        public void GetAllBugByMember_RepoThrowsException_ExceptionLoggedAndRethrown()
        {
            // Arrange
            var members = new List<Member>
    {
        new Member { MemberId = 1, Email = "email" , ProjectId = 1 , RoleId = 3 },
        new Member { MemberId = 2, Email = "email" , ProjectId = 1 , RoleId = 3 }
    };

            _bugRepoMock.When(x => x.GetAllBug())
                .Do(x => throw new Exception("Repo failed"));

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _sut.GetAllBugByMember(members));
            Assert.Equal("Repo failed", ex.Message);
        }

        #endregion

        #region GetBug Exception Test

        [Fact]
        public void GetBug_RepoThrowsException_ExceptionLoggedAndRethrown()
        {
            // Arrange
            int bugId = 1;
            _bugRepoMock.When(x => x.GetBug(bugId))
                .Do(x => throw new Exception("Bug not found"));

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _sut.GetBug(bugId));
            Assert.Equal("Bug not found", ex.Message);
        }

        #endregion

        #region UpdateBug with BugVM Exception Test

        [Fact]
        public void UpdateBug_RepoThrowsException_ExceptionLoggedAndRethrown()
        {
            // Arrange
            int bugId = 1;
            var bugVM = new BugVM
            {
                Name = "Bug Name",
                Descripton = "Desc",
                BugStatus = 2,
                Priority = 1,
                AssignMembersId = 3,
                QaRemarks = "Check",
                TaskId = 101
            };

            _bugRepoMock.When(x => x.GetBug(bugId))
                .Do(x => throw new Exception("Update failed"));

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _sut.UpdateBug(bugId, bugVM));
            Assert.Equal("Update failed", ex.Message);
        }

        #endregion

        #region GetAllBugByMember Success Test

        [Fact]
        public void GetAllBugByMember_ValidMembers_ReturnsFilteredBugs()
        {
            // Arrange
            var members = new List<Member>
    {
        new Member { MemberId = 1 },
        new Member { MemberId = 2 }
    };

            var allBugs = new List<Bug>
    {
        new Bug { Id = 1, AssignMembersId = 1, Name = "Bug 1" },
        new Bug { Id = 2, AssignMembersId = 2, Name = "Bug 2" },
        new Bug { Id = 3, AssignMembersId = 3, Name = "Bug 3" }
    };

            _bugRepoMock.GetAllBug().Returns(allBugs);

            // Act
            var result = _sut.GetAllBugByMember(members);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, b => b.Id == 1);
            Assert.Contains(result, b => b.Id == 2);
        }

        #endregion


        #region GetAllBugOfStory Success Test

        [Fact]
        public void GetAllBugOfStory_ValidId_ReturnsBugs()
        {
            // Arrange
            int storyId = 1;
            var bugs = new List<Bug>
    {
        new Bug { Id = 1, UserStoryId = 1 },
        new Bug { Id = 2, UserStoryId = 1 }
    };

            _bugRepoMock.GetAllBugOfStory(storyId).Returns(bugs);

            // Act
            var result = _sut.GetAllBugOfStory(storyId);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, b => Assert.Equal(1, b.UserStoryId));
        }

        #endregion
        #region GetBug Success Test

        [Fact]
        public void GetBug_ValidId_ReturnsBug()
        {
            // Arrange
            int bugId = 1;
            var bug = new Bug { Id = bugId, Name = "Test Bug" };

            _bugRepoMock.GetBug(bugId).Returns(bug);

            // Act
            var result = _sut.GetBug(bugId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bugId, result.Id);
        }

        #endregion


        #region UpdateBug with BugVM Success Test

        [Fact]
        public void UpdateBug_ValidIdAndViewModel_UpdatesBug()
        {
            // Arrange
            int bugId = 1;
            var bugVM = new BugVM
            {
                Name = "Updated Bug",
                Descripton = "Updated Description",
                BugStatus = 2,
                Priority = 1,
                AssignMembersId = 5,
                QaRemarks = "Test QA",
                TaskId = 20
            };

            var existingBug = new Bug
            {
                Id = bugId,
                Name = "Old Bug",
                Descripton = "Old Desc"
            };

            _bugRepoMock.GetBug(bugId).Returns(existingBug);

            // Act
            _sut.UpdateBug(bugId, bugVM);

            // Assert
            _bugRepoMock.Received(1).UpdateBug(Arg.Is<Bug>(b =>
                b.Name == bugVM.Name &&
                b.Descripton == bugVM.Descripton &&
                b.BugStatus == bugVM.BugStatus &&
                b.Priority == bugVM.Priority &&
                b.AssignMembersId == bugVM.AssignMembersId &&
                b.TaskId == bugVM.TaskId &&
                b.QaRemarks == bugVM.QaRemarks
            ));
        }

        #endregion

        #region UpdateBug with Bug Model Success Test

        [Fact]
        public void UpdateBug_ValidBug_CallsRepoUpdate()
        {
            // Arrange
            var bug = new Bug { Id = 1, Name = "Update this bug" };

            // Act
            _sut.UpdateBug(bug);

            // Assert
            _bugRepoMock.Received(1).UpdateBug(bug);
        }

        #endregion

        #region GetAllBug Exception Test

        [Fact]
        public void GetAllBug_RepoThrowsException_ExceptionLoggedAndRethrown()
        {
            // Arrange
            _bugRepoMock.When(x => x.GetAllBug())
                .Do(x => throw new Exception("Failed to fetch all bugs"));

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _sut.GetAllBug());
            Assert.Equal("Failed to fetch all bugs", ex.Message);
        }

        #endregion

        #region GetAllBugOfStory Exception Test

        [Fact]
        public void GetAllBugOfStory_RepoThrowsException_ExceptionLoggedAndRethrown()
        {
            // Arrange
            int storyId = 10;
            _bugRepoMock.When(x => x.GetAllBugOfStory(storyId))
                .Do(x => throw new Exception("Story fetch error"));

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _sut.GetAllBugOfStory(storyId));
            Assert.Equal("Story fetch error", ex.Message);
        }

        #endregion

        #region UpdateBug (Single Bug Parameter) Exception Test

        [Fact]
        public void UpdateBug_BugRepoThrowsException_ExceptionLoggedAndRethrown()
        {
            // Arrange
            var bug = new Bug
            {
                Id = 99,
                Name = "Unstable Bug"
            };

            _bugRepoMock.When(x => x.UpdateBug(bug))
                .Do(x => throw new Exception("Unable to update"));

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _sut.UpdateBug(bug));
            Assert.Equal("Unable to update", ex.Message);
        }

        #endregion


    }
}
