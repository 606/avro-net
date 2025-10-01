using Xunit;

namespace Avro.Aws.Auth.UnitTests;

public class AwsAuthManagerTests
{
    [Fact]
    public void Constructor_CreatesInstance()
    {
        var manager = new AwsAuthManager();
        Assert.NotNull(manager);
    }
}