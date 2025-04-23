using System.Diagnostics.CodeAnalysis;

namespace BusinessLogicLayer.Test.Service
{
    [ExcludeFromCodeCoverage]
    public class BaseTest : IDisposable
    {

        public BaseTest()
        {
            // Initialize the test setup here
        }
        public void Dispose()
        {
            // Clean up resources here
        }
    }
}

/*
 * 
 * 
 **** Reference ****
 * 
 * 
 
 
[ExcludeFromCodeCoverage]
public class BaseTest : IDisposable
{
    protected readonly ISession _sessionMock;
    protected readonly ITransaction _iTransactionMock;
    public BaseTest()
    {
        _sessionMock = Substitute.For<ISession>(); // Requires using NSubstitute;
        _iTransactionMock = Substitute.For<ITransaction>(); // Requires using NSubstitute;
    }
    public virtual void Dispose()
    {
        _sessionMock?.ClearSubstitute(); // Requires using NSubstitute.ClearExtensions;
        _iTransactionMock?.ClearSubstitute(); // Requires using NSubstitute.ClearExtensions;
    }
}

 */
