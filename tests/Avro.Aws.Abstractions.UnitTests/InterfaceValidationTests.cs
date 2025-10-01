using Xunit;

namespace Avro.Aws.Abstractions.UnitTests;

public class InterfaceValidationTests
{
    [Fact]
    public void IAwsCredentialProvider_HasRequiredMethods()
    {
        var type = typeof(IAwsCredentialProvider);
        Assert.NotNull(type.GetMethod("GetAccessKeyIdAsync"));
        Assert.NotNull(type.GetMethod("GetSecretAccessKeyAsync"));
        Assert.NotNull(type.GetMethod("GetSessionTokenAsync"));
    }

    [Fact]
    public void IAwsSessionManager_HasRequiredMethods()
    {
        var type = typeof(IAwsSessionManager);
        Assert.NotNull(type.GetMethod("StartSessionAsync"));
        Assert.NotNull(type.GetMethod("EndSessionAsync"));
        Assert.NotNull(type.GetMethod("HasActiveSessionAsync"));
    }

    [Fact]
    public void IAwsRegionProvider_HasRequiredMethods()
    {
        var type = typeof(IAwsRegionProvider);
        Assert.NotNull(type.GetMethod("GetRegionAsync"));
        Assert.NotNull(type.GetMethod("SetRegionAsync"));
    }
}