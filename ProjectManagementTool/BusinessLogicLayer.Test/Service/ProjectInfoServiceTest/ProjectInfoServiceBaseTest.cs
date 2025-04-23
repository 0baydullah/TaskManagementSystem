using BusinessLogicLayer.IService;
using BusinessLogicLayer.Service;
using DataAccessLayer.IRepository;
using log4net;
using NSubstitute;
using NSubstitute.ClearExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Test.Service.ProjectInfoServiceTest
{
    public class ProjectInfoServiceBaseTest: BaseTest
    {
        protected readonly IProjectInfoRepo _projectInfoRepoMock;
        protected readonly IMemberRepo _memberRepoMock;
        protected readonly IRoleRepo _roleRepoMock;
        protected readonly IReleaseRepo _releaseRepoMock;
        protected readonly ISprintRepo _sprintRepoMock;
        protected readonly IUserStoryRepo _userStoryRepoMock;
        protected readonly IFeatureRepo _featureRepoMock;
        protected readonly ITasksRepo _tasksRepoMock;
        protected readonly IBugRepo _bugRepoMock;
        protected readonly IActivityRepo _activityRepoMock;

        protected readonly IProjectInfoService _sut;

        public ProjectInfoServiceBaseTest()
        {
            _projectInfoRepoMock = Substitute.For<IProjectInfoRepo>();
            _memberRepoMock = Substitute.For<IMemberRepo>();
            _roleRepoMock = Substitute.For<IRoleRepo>();
            _releaseRepoMock = Substitute.For<IReleaseRepo>();
            _sprintRepoMock = Substitute.For<ISprintRepo>();
            _userStoryRepoMock = Substitute.For<IUserStoryRepo>();
            _featureRepoMock = Substitute.For<IFeatureRepo>();
            _tasksRepoMock = Substitute.For<ITasksRepo>();
            _bugRepoMock = Substitute.For<IBugRepo>();
            _activityRepoMock = Substitute.For<IActivityRepo>();

            _sut = new ProjectInfoService(_projectInfoRepoMock ,_memberRepoMock, _roleRepoMock, _releaseRepoMock, _sprintRepoMock, _userStoryRepoMock
                , _featureRepoMock, _tasksRepoMock, _bugRepoMock, _activityRepoMock);
            
        }
        public override void Dispose()
        {
            _projectInfoRepoMock?.ClearSubstitute();
            _memberRepoMock.ClearSubstitute();
            _roleRepoMock?.ClearSubstitute();
            _releaseRepoMock?.ClearSubstitute();
            _sprintRepoMock?.ClearSubstitute();
            _userStoryRepoMock?.ClearSubstitute();
            _featureRepoMock?.ClearSubstitute();
            _tasksRepoMock?.ClearSubstitute();
            _bugRepoMock?.ClearSubstitute();
            _activityRepoMock?.ClearSubstitute();
     
            base.Dispose();
        }
    }
}
