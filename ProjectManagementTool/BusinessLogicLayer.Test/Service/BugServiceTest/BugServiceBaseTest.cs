using System.Diagnostics.CodeAnalysis;
using BusinessLogicLayer.IService;
using BusinessLogicLayer.Service;
using DataAccessLayer.IRepository;
using log4net;
using NSubstitute;
using NSubstitute.ClearExtensions;

namespace BusinessLogicLayer.Test.Service.BugServiceTest
{
    [ExcludeFromCodeCoverage]
    public class BugServiceBaseTest : BaseTest
    {
        protected readonly IBugRepo _bugRepoMock;
        protected readonly IMemberRepo _memberRepoMock;
        protected readonly ILog _log = LogManager.GetLogger(typeof(BugService));

        protected readonly IBugService _sut;

        public BugServiceBaseTest()
        {
            _bugRepoMock = Substitute.For<IBugRepo>();
            _memberRepoMock = Substitute.For<IMemberRepo>();

            _sut = new BugService(_bugRepoMock, _memberRepoMock);
        }

        public override void Dispose()
        {
            _bugRepoMock?.ClearSubstitute();
            _memberRepoMock?.ClearSubstitute();

            base.Dispose();
        }
    }
}
