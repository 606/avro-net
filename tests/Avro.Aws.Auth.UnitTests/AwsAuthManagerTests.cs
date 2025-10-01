using Avro.Aws.Abstractions;
using Moq;
using Xunit;
using Times = Moq.Times;

namespace Avro.Aws.Auth.UnitTests;

public class AwsAuthManagerTests : IDisposable
{
    private const string TestAccessKeyId = "AKIATESTKEY123";
    private const string TestSecretKey = "testSecretKey123";
    private const string TestSessionToken = "testSessionToken123";
    private const string TestRegion = "us-west-2";
    private const string TestDefaultRegion = "us-east-1";
    private const string TestRoleArn = "arn:aws:iam::123456789012:role/test-role";

    private readonly IDictionary<string, string?> _envVarBackup;

    public AwsAuthManagerTests()
    {
        // Backup any existing AWS environment variables
        _envVarBackup = new Dictionary<string, string?>
        {
            { "AWS_ACCESS_KEY_ID", Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID") },
            { "AWS_SECRET_ACCESS_KEY", Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY") },
            { "AWS_SESSION_TOKEN", Environment.GetEnvironmentVariable("AWS_SESSION_TOKEN") },
            { "AWS_REGION", Environment.GetEnvironmentVariable("AWS_REGION") },
            { "AWS_DEFAULT_REGION", Environment.GetEnvironmentVariable("AWS_DEFAULT_REGION") },
            { "AWS_ROLE_ARN", Environment.GetEnvironmentVariable("AWS_ROLE_ARN") }
        };
    }

    public void Dispose()
    {
        // Restore original environment variables
        foreach (var (key, value) in _envVarBackup)
        {
            if (value == null)
            {
                Environment.SetEnvironmentVariable(key, null);
            }
            else
            {
                Environment.SetEnvironmentVariable(key, value);
            }
        }
    }

    [Fact]
    public void Constructor_CreatesInstance()
    {
        var manager = new AwsAuthManager();
        Assert.NotNull(manager);
    }

    [Fact]
    public async Task GetAccessKeyIdAsync_FromEnvironment_ReturnsValue()
    {
        // Arrange
        Environment.SetEnvironmentVariable("AWS_ACCESS_KEY_ID", TestAccessKeyId);
        var manager = new AwsAuthManager();

        // Act
        var result = await manager.GetAccessKeyIdAsync();

        // Assert
        Assert.Equal(TestAccessKeyId, result);
    }

    [Fact]
    public async Task GetSecretAccessKeyAsync_FromEnvironment_ReturnsValue()
    {
        // Arrange
        Environment.SetEnvironmentVariable("AWS_SECRET_ACCESS_KEY", TestSecretKey);
        var manager = new AwsAuthManager();

        // Act
        var result = await manager.GetSecretAccessKeyAsync();

        // Assert
        Assert.Equal(TestSecretKey, result);
    }

    [Fact]
    public async Task GetSessionTokenAsync_FromEnvironment_ReturnsValue()
    {
        // Arrange
        Environment.SetEnvironmentVariable("AWS_SESSION_TOKEN", TestSessionToken);
        var manager = new AwsAuthManager();

        // Act
        var result = await manager.GetSessionTokenAsync();

        // Assert
        Assert.Equal(TestSessionToken, result);
    }

    [Fact]
    public async Task GetSessionTokenAsync_NoSessionToken_ReturnsNull()
    {
        // Arrange
        Environment.SetEnvironmentVariable("AWS_SESSION_TOKEN", null);
        var manager = new AwsAuthManager();

        // Act
        var result = await manager.GetSessionTokenAsync();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAccessKeyIdAsync_MissingEnvironmentVariable_ThrowsException()
    {
        // Arrange
        Environment.SetEnvironmentVariable("AWS_ACCESS_KEY_ID", null);
        var manager = new AwsAuthManager();

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => manager.GetAccessKeyIdAsync());
    }

    [Fact]
    public async Task GetSecretAccessKeyAsync_MissingEnvironmentVariable_ThrowsException()
    {
        // Arrange
        Environment.SetEnvironmentVariable("AWS_SECRET_ACCESS_KEY", null);
        var manager = new AwsAuthManager();

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => manager.GetSecretAccessKeyAsync());
    }

    [Fact]
    public async Task CredentialProvider_WhenProvided_IsUsed()
    {
        // Arrange
        var mockProvider = new Mock<IAwsCredentialProvider>();
        mockProvider.Setup(p => p.GetAccessKeyIdAsync())
            .ReturnsAsync(TestAccessKeyId);
        mockProvider.Setup(p => p.GetSecretAccessKeyAsync())
            .ReturnsAsync(TestSecretKey);
        mockProvider.Setup(p => p.GetSessionTokenAsync())
            .ReturnsAsync(TestSessionToken);

        var manager = new AwsAuthManager(mockProvider.Object);

        // Act & Assert
        Assert.Equal(TestAccessKeyId, await manager.GetAccessKeyIdAsync());
        Assert.Equal(TestSecretKey, await manager.GetSecretAccessKeyAsync());
        Assert.Equal(TestSessionToken, await manager.GetSessionTokenAsync());

        mockProvider.Verify(p => p.GetAccessKeyIdAsync(), Times.Once);
        mockProvider.Verify(p => p.GetSecretAccessKeyAsync(), Times.Once);
        mockProvider.Verify(p => p.GetSessionTokenAsync(), Times.Once);
    }

    [Fact]
    public async Task GetRegionAsync_FromRegionEnv_ReturnsValue()
    {
        // Arrange
        Environment.SetEnvironmentVariable("AWS_REGION", TestRegion);
        Environment.SetEnvironmentVariable("AWS_DEFAULT_REGION", null);
        var manager = new AwsAuthManager();

        // Act
        var result = await manager.GetRegionAsync();

        // Assert
        Assert.Equal(TestRegion, result);
    }

    [Fact]
    public async Task GetRegionAsync_FromDefaultRegionEnv_ReturnsValue()
    {
        // Arrange
        Environment.SetEnvironmentVariable("AWS_REGION", null);
        Environment.SetEnvironmentVariable("AWS_DEFAULT_REGION", TestDefaultRegion);
        var manager = new AwsAuthManager();

        // Act
        var result = await manager.GetRegionAsync();

        // Assert
        Assert.Equal(TestDefaultRegion, result);
    }

    [Fact]
    public async Task GetRegionAsync_NoRegionSet_ThrowsException()
    {
        // Arrange
        Environment.SetEnvironmentVariable("AWS_REGION", null);
        Environment.SetEnvironmentVariable("AWS_DEFAULT_REGION", null);
        var manager = new AwsAuthManager();

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => manager.GetRegionAsync());
    }

    [Fact]
    public async Task SetRegionAsync_ValidRegion_UpdatesRegion()
    {
        // Arrange
        var manager = new AwsAuthManager();

        // Act
        await manager.SetRegionAsync(TestRegion);
        var result = await manager.GetRegionAsync();

        // Assert
        Assert.Equal(TestRegion, result);
    }

    [Fact]
    public async Task SetRegionAsync_EmptyRegion_ThrowsException()
    {
        // Arrange
        var manager = new AwsAuthManager();

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            () => manager.SetRegionAsync(string.Empty));
    }

    [Fact]
    public async Task RegionProvider_WhenProvided_IsUsed()
    {
        // Arrange
        var mockProvider = new Mock<IAwsRegionProvider>();
        mockProvider.Setup(p => p.GetRegionAsync())
            .ReturnsAsync(TestRegion);

        var manager = new AwsAuthManager(regionProvider: mockProvider.Object);

        // Act
        await manager.SetRegionAsync(TestRegion);
        var result = await manager.GetRegionAsync();

        // Assert
        Assert.Equal(TestRegion, result);
        mockProvider.Verify(p => p.GetRegionAsync(), Times.Once);
        mockProvider.Verify(p => p.SetRegionAsync(TestRegion), Times.Once);
    }

    [Fact]
    public void GetRoleArn_FromEnvironment_ReturnsValue()
    {
        // Arrange
        Environment.SetEnvironmentVariable("AWS_ROLE_ARN", TestRoleArn);
        var manager = new AwsAuthManager();

        // Act
        var result = manager.GetRoleArn();

        // Assert
        Assert.Equal(TestRoleArn, result);
    }

    [Fact]
    public void SetRoleArn_ValidArn_UpdatesRoleArn()
    {
        // Arrange
        var manager = new AwsAuthManager();

        // Act
        manager.SetRoleArn(TestRoleArn);
        var result = manager.GetRoleArn();

        // Assert
        Assert.Equal(TestRoleArn, result);
    }

    [Fact]
    public void SetRoleArn_EmptyArn_ThrowsException()
    {
        // Arrange
        var manager = new AwsAuthManager();

        // Act & Assert
        Assert.Throws<ArgumentException>(
            () => manager.SetRoleArn(string.Empty));
    }
}