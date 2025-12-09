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

            // Act
            Action act = () => Device.Create(
                Guid.NewGuid(),
                longName,
                "Description",
                DeviceType.SmartLight,
                Guid.NewGuid()
            );

            // Assert
            act.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(151)]
        [InlineData(300)]
        public void Create_WithTooLongDescription_ThrowsArgumentException(int length)
        {
            var longDesc = new string('b', length);

            Action act = () => Device.Create(
                Guid.NewGuid(),
                "Name",
                longDesc,
                DeviceType.SmartLight,
                Guid.NewGuid()
            );

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void TurnOn_WhenOff_ShouldTurnOn()
        {
            var device = Device.Create(Guid.NewGuid(), "Name", "Desc", DeviceType.SmartLight, Guid.NewGuid());

            device.TurnOn();

            device.IsEnabled.Should().BeTrue();
        }

        [Fact]
        public void TurnOn_WhenOn_ShouldTurnOff()
        {
            var device = Device.Create(Guid.NewGuid(), "Name", "Desc", DeviceType.SmartLight, Guid.NewGuid());
            device.TurnOn(); // now ON

            device.TurnOn(); // toggle → OFF

            device.IsEnabled.Should().BeFalse();
        }
    }
}
