using FluentAssertions;
using SmHm.Core.Enums;
using SmHm.Core.Models;
using Xunit;

namespace SmHm.UnitTests.Devices
{
    public class DeviceTests
    {
        [Fact]
        public void Create_WithValidData_ReturnsDevice()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Smart Light";
            var description = "Living room light";
            var deviceType = DeviceType.SmartLight;
            var roomId = Guid.NewGuid();

            // Act
            var device = Device.Create(id, name, description, deviceType, roomId);

            // Assert
            device.Should().NotBeNull();
            device.Id.Should().Be(id);
            device.Name.Should().Be(name);
            device.Description.Should().Be(description);
            device.DeviceType.Should().Be(deviceType);
            device.RoomId.Should().Be(roomId);
            device.IsEnabled.Should().BeFalse();
        }

        [Theory]
        [InlineData(51)]
        [InlineData(100)]
        public void Create_WithTooLongName_ThrowsArgumentException(int nameLength)
        {
            // Arrange
            var longName = new string('a', nameLength);

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                Device.Create(
                    Guid.NewGuid(),
                    longName,
                    "Description",
                    DeviceType.SmartLight,
                    Guid.NewGuid()));
        }
    }
}
