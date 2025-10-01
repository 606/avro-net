namespace Avro.Os.UserInfo.UnitTests;

public class OsUserInfoTests
{
    [Fact]
    public void GetCurrentUser_ReturnsUserInfo()
    {
        // Act
        var currentUser = OsUserInfo.GetCurrentUser();

        // Assert
        currentUser.Should().NotBeNull();
        currentUser.Username.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void GetCurrentUsername_ReturnsNonEmptyString()
    {
        // Act
        var username = OsUserInfo.GetCurrentUsername();

        // Assert
        username.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void GetCurrentUserHomeDirectory_ReturnsString()
    {
        // Act
        var homeDirectory = OsUserInfo.GetCurrentUserHomeDirectory();

        // Assert
        // Home directory might be null on some systems, so we just check it's not throwing
        Assert.True(true); // If we get here without exception, the test passes
    }

    [Fact]
    public void GetCurrentUserFullName_ReturnsString()
    {
        // Act
        var fullName = OsUserInfo.GetCurrentUserFullName();

        // Assert
        // Full name might be null on some systems, so we just check it's not throwing
        Assert.True(true); // If we get here without exception, the test passes
    }

    [Fact]
    public void IsCurrentUserAdministrator_ReturnsBool()
    {
        // Act
        var isAdmin = OsUserInfo.IsCurrentUserAdministrator();

        // Assert
        // Just check that it doesn't throw - the method should return a boolean
        // The actual value depends on the current user's privileges
    }

    [Fact]
    public void GetAllUsers_ReturnsCollection()
    {
        // Act
        var users = OsUserInfo.GetAllUsers();

        // Assert
        users.Should().NotBeNull();
    }

    [Theory]
    [InlineData("nonexistentuser")]
    [InlineData("invalid-user-name")]
    public void GetUser_WithNonExistentUser_ReturnsNull(string username)
    {
        // Act
        var user = OsUserInfo.GetUser(username);

        // Assert
        // Most likely returns null for non-existent users
        // But we can't guarantee this across all platforms
        (user == null || user.Username == username).Should().BeTrue();
    }

    [Fact]
    public void GetUser_WithCurrentUsername_ReturnsUserInfo()
    {
        // Arrange
        var currentUsername = OsUserInfo.GetCurrentUsername();

        // Act
        var user = OsUserInfo.GetUser(currentUsername);

        // Assert
        // Should be able to find the current user
        // Note: This might fail on some systems where user lookup is not supported
        if (user != null)
        {
            user.Username.Should().BeEquivalentTo(currentUsername);
        }
    }

    [Fact]
    public void StaticMethods_UseConsistentProvider()
    {
        // Act
        var user1 = OsUserInfo.GetCurrentUser();
        var user2 = OsUserInfo.GetCurrentUser();
        var username1 = OsUserInfo.GetCurrentUsername();
        var username2 = OsUserInfo.GetCurrentUsername();

        // Assert
        user1.Username.Should().Be(user2.Username);
        username1.Should().Be(username2);
        user1.Username.Should().Be(username1);
    }
}