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
    }
}
