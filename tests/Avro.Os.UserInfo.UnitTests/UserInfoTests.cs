namespace Avro.Os.UserInfo.UnitTests;

public class UserInfoTests
{
    [Fact]
    public void Constructor_WithValidUsername_SetsProperties()
    {
        // Arrange
        const string username = "testuser";
        const string fullName = "Test User";
        const string homeDirectory = "/home/testuser";
        const string domain = "DOMAIN";
        const bool isAdministrator = true;
        const string userId = "1000";
        const string groupId = "1000";
        const string shell = "/bin/bash";

        // Act
        var userInfo = new UserInfo(
            username: username,
            fullName: fullName,
            homeDirectory: homeDirectory,
            domain: domain,
            isAdministrator: isAdministrator,
            userId: userId,
            groupId: groupId,
            shell: shell);

        // Assert
        userInfo.Username.Should().Be(username);
        userInfo.FullName.Should().Be(fullName);
        userInfo.HomeDirectory.Should().Be(homeDirectory);
        userInfo.Domain.Should().Be(domain);
        userInfo.IsAdministrator.Should().Be(isAdministrator);
        userInfo.UserId.Should().Be(userId);
        userInfo.GroupId.Should().Be(groupId);
        userInfo.Shell.Should().Be(shell);
    }

    [Fact]
    public void Constructor_WithNullUsername_ThrowsArgumentNullException()
    {
        // Act & Assert
        var act = () => new UserInfo(null!);
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Constructor_WithEmptyUsername_ThrowsArgumentNullException()
    {
        // Act & Assert
        var act = () => new UserInfo("");
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Constructor_WithMinimalParameters_SetsDefaultValues()
    {
        // Arrange
        const string username = "testuser";

        // Act
        var userInfo = new UserInfo(username);

        // Assert
        userInfo.Username.Should().Be(username);
        userInfo.FullName.Should().BeNull();
        userInfo.HomeDirectory.Should().BeNull();
        userInfo.Domain.Should().BeNull();
        userInfo.IsAdministrator.Should().BeFalse();
        userInfo.UserId.Should().BeNull();
        userInfo.GroupId.Should().BeNull();
        userInfo.Shell.Should().BeNull();
    }

    [Theory]
    [InlineData("testuser", null, "testuser")]
    [InlineData("testuser", "Test User", "testuser (Test User)")]
    [InlineData("admin", "Administrator", "admin (Administrator)")]
    public void ToString_ReturnsExpectedFormat(string username, string? fullName, string expected)
    {
        // Arrange
        var userInfo = new UserInfo(username, fullName: fullName);

        // Act
        var result = userInfo.ToString();

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void Equals_WithSameUsername_ReturnsTrue()
    {
        // Arrange
        var userInfo1 = new UserInfo("testuser", fullName: "Test User 1");
        var userInfo2 = new UserInfo("testuser", fullName: "Test User 2");

        // Act & Assert
        Assert.True(userInfo1.Equals(userInfo2));
        Assert.True(userInfo2.Equals(userInfo1));
    }

    [Fact]
    public void Equals_WithDifferentUsername_ReturnsFalse()
    {
        // Arrange
        var userInfo1 = new UserInfo("testuser1");
        var userInfo2 = new UserInfo("testuser2");

        // Act & Assert
        userInfo1.Equals(userInfo2).Should().BeFalse();
        userInfo2.Equals(userInfo1).Should().BeFalse();
    }

    [Fact]
    public void Equals_WithCaseInsensitiveUsername_ReturnsTrue()
    {
        // Arrange
        var userInfo1 = new UserInfo("TestUser");
        var userInfo2 = new UserInfo("testuser");

        // Act & Assert
        Assert.True(userInfo1.Equals(userInfo2));
        Assert.True(userInfo2.Equals(userInfo1));
    }

    [Fact]
    public void Equals_WithNullObject_ReturnsFalse()
    {
        // Arrange
        var userInfo = new UserInfo("testuser");

        // Act & Assert
        userInfo.Equals(null).Should().BeFalse();
    }

    [Fact]
    public void Equals_WithDifferentObjectType_ReturnsFalse()
    {
        // Arrange
        var userInfo = new UserInfo("testuser");
        var otherObject = "testuser";

        // Act & Assert
        userInfo.Equals(otherObject).Should().BeFalse();
    }

    [Fact]
    public void GetHashCode_WithSameUsername_ReturnsSameHashCode()
    {
        // Arrange
        var userInfo1 = new UserInfo("testuser", fullName: "Test User 1");
        var userInfo2 = new UserInfo("testuser", fullName: "Test User 2");

        // Act
        var hashCode1 = userInfo1.GetHashCode();
        var hashCode2 = userInfo2.GetHashCode();

        // Assert
        Assert.Equal(hashCode1, hashCode2);
    }

    [Fact]
    public void GetHashCode_WithCaseInsensitiveUsername_ReturnsSameHashCode()
    {
        // Arrange
        var userInfo1 = new UserInfo("TestUser");
        var userInfo2 = new UserInfo("testuser");

        // Act
        var hashCode1 = userInfo1.GetHashCode();
        var hashCode2 = userInfo2.GetHashCode();

        // Assert
        Assert.Equal(hashCode1, hashCode2);
    }
}