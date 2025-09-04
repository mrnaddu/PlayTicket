using Volo.Abp.Modularity;
using Xunit;

namespace PlayTicket.UserService.Samples;

public abstract class SampleRepository_Tests<TStartupModule> : UserServiceTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    protected SampleRepository_Tests()
    {

    }

    [Fact]
    public void Method1()
    {

    }
}
