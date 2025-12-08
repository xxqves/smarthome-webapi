using Xunit;
using Moq;
using FluentAssertions;
using SmHm.Core.Models;
using SmHm.Core.Enums;
using SmHm.Core.Abstractions;
using SmHm.Application.Services;
using SmHm.Core.Abstractions.Messaging;

namespace SmHm.UnitTests.Devices
{
    public class DeviceServiceTests
    {
        [Fact]
        public async Task TurnOnAsync_DeviceExists_SetIsOn()
        {
            // Arrange
            var device = Device.Create(
                Guid.NewGuid(),
                "TestName",
                "TestDesc",
                DeviceType.AirConditioner,
                Guid.NewGuid());

            var repoMock = new Mock<IDeviceRepository>();
            var currUserServiceMock = new Mock<ICurrentUserService>();
            var messageBusMock = new Mock<IMessageBus>();

            repoMock.Setup(x => x.GetById(device.Id, new CancellationToken())).ReturnsAsync(device);

            var svc = new DeviceService(repoMock.Object, currUserServiceMock.Object, messageBusMock.Object);

            // Act
            await svc.TurnOn(device.Id);

            // Assert
            device.IsEnabled.Should().BeTrue();
        }
    }
}
