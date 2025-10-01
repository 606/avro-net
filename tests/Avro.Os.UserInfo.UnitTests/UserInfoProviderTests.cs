using Avro.Os.Identity;

namespace Avro.Os.UserInfo.UnitTests;

public class UserInfoProviderTests
{
    private readonly UserInfoProvider _userInfoProvider;

    public UserInfoProviderTests()
    {
        _userInfoProvider = new UserInfoProvider();
    }

    [Fact]
    public void Constructor_WithNullDetector_UsesDefaultDetector()
    {
        // Act
        var provider = new UserInfoProvider(null);
        var currentUser = provider.GetCurrentUser();

        // Assert
        currentUser.Should().NotBeNull();
        currentUser.Username.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void GetCurrentUser_ReturnsValidUserInfo()
    {
        // Act
        var currentUser = _userInfoProvider.GetCurrentUser();

        // Assert
        currentUser.Should().NotBeNull();
        currentUser.Username.Should().NotBeNullOrEmpty();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    public void GetUser_WithInvalidUsername_ReturnsNull(string username)
    {
        // Act
        var user = _userInfoProvider.GetUser(username);

        // Assert
        user.Should().BeNull();
    }

    [Fact]
    public void GetUser_WithNullUsername_ReturnsNull()
    {
        // Act
        var user = _userInfoProvider.GetUser(null!);

        // Assert
        user.Should().BeNull();
    }

    [Fact]
    public void GetAllUsers_ReturnsCollection()
    {
        // Act
        var users = _userInfoProvider.GetAllUsers();

        // Assert
        users.Should().NotBeNull();
    }

    [Fact]
    public void IsCurrentUserAdministrator_ReturnsBool()
    {
        // Act
        var isAdmin = _userInfoProvider.IsCurrentUserAdministrator();

        // Assert
        // Just check that it doesn't throw - the method should return a boolean
        // The actual value depends on the current user's privileges
    }
}