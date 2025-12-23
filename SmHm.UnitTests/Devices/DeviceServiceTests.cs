using FluentAssertions;
using Moq;
using SmHm.Application.Services;
using SmHm.Contracts.Events.DeviceEvents;
using SmHm.Core.Abstractions;
using SmHm.Core.Abstractions.Messaging;
using SmHm.Core.Enums;
using SmHm.Core.Models;
using Xunit;

namespace SmHm.UnitTests.Devices
{
    public class DeviceServiceTests
    {
        private readonly Mock<IDeviceRepository> _repo = new();
        private readonly Mock<ICurrentUserService> _user = new();
        private readonly Mock<IMessageBus> _bus = new();

        private DeviceService CreateService()
        {
            _user.Setup(x => x.UserId).Returns(Guid.NewGuid());
            _user.Setup(x => x.UserName).Returns("test");

            return new DeviceService(_repo.Object, _user.Object, _bus.Object);
        }

        [Fact]
        public async Task GetAllDevices_ReturnsList()
        {
            var devices = new List<Device>()
            {
                Device.Create(Guid.NewGuid(), "A", "B", DeviceType.SmartLight, Guid.NewGuid())
            };

            _repo.Setup(r => r.Get(It.IsAny<CancellationToken>()))
                 .ReturnsAsync(devices);

            var service = CreateService();

            var result = await service.GetAllDevices();

            result.Should().HaveCount(1);
        }

        [Fact]
        public async Task CreateDevice_PublishesEvent_AndSavesDevice()
        {
            var device = Device.Create(Guid.NewGuid(), "A", "B", DeviceType.SmartLight, Guid.NewGuid());

            _repo.Setup(r => r.Create(device, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(device.Id);

            var service = CreateService();

            var result = await service.CreateDevice(device);

            result.Should().Be(device.Id);

            _bus.Verify(x => x.PublishAsync(It.IsAny<DeviceCreated>(), It.IsAny<CancellationToken>()), Times.Once);
            _repo.Verify(x => x.Create(device, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateDevice_CallsRepository()
        {
            var id = Guid.NewGuid();
            var roomid = Guid.NewGuid();

            _repo.Setup(r => r.Update(id, "A", "B", DeviceType.SmartLight, roomid, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(id);

            var service = CreateService();

            var result = await service.UpdateDevice(id, "A", "B", DeviceType.SmartLight, roomid);

            result.Should().Be(id);
        }

        [Fact]
        public async Task DeleteDevice_CallsRepository()
        {
            var id = Guid.NewGuid();

            _repo.Setup(r => r.Delete(id, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(id);

            var service = CreateService();

            var result = await service.DeleteDevice(id);

            result.Should().Be(id);
        }

        [Fact]
        public async Task TurnOn_WhenDeviceNotFound_ShouldThrow()
        {
            _repo.Setup(r => r.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))!
                 .ReturnsAsync((Device?)null);

            var service = CreateService();

            var act = async () => await service.TurnOn(Guid.NewGuid());

            await act.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task TurnOn_WhenDeviceExists_ShouldToggleAndSave()
        {
            var device = Device.Create(Guid.NewGuid(), "Name", "Desc", DeviceType.SmartLight, Guid.NewGuid());

            _repo.Setup(r => r.GetById(device.Id, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(device);

            var service = CreateService();

            await service.TurnOn(device.Id);

            device.IsEnabled.Should().BeTrue();

            _repo.Verify(r => r.Update(
                device.Id,
                device.Name,
                device.Description,
                device.DeviceType,
                device.IsEnabled,
                device.RoomId,
                It.IsAny<CancellationToken>()
            ), Times.Once);
        }
    }
}
