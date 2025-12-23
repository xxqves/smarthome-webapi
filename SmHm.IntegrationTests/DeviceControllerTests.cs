using FluentAssertions;
using SmHm.Core.Enums;
using SmHm.WebApi.Contracts.Devices;
using System.Net.Http.Json;
using Xunit;

namespace SmHm.IntegrationTests
{
    public class DeviceControllerTests
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public DeviceControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateDevice_Then_GetAllDevices_ReturnsCreatedDevice()
        {
            // Arrange
            var request = new DeviceRequest(
                Name: "Lamp",
                Description: "Living room lamp",
                DeviceType: DeviceType.SmartLight,
                RoomId: Guid.NewGuid());

            // Act - POST
            var createResponse = await _client.PostAsJsonAsync("/api/devices/add", request);

            createResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            var deviceId = await createResponse.Content.ReadFromJsonAsync<Guid>();

            // Act - GET
            var devices = await _client.GetFromJsonAsync<List<DeviceResponse>>("/api/devices/get/all");

            // Assert
            devices.Should().NotBeNull();
            devices.Should().ContainSingle(d => d.id == deviceId);

            var device = devices!.Single(d => d.id == deviceId);
            device.Name.Should().Be("Lamp");
            device.Description.Should().Be("Living room lamp");
            device.DeviceType.Should().Be(DeviceType.SmartLight);
            device.IsEnabled.Should().BeFalse();                
        }
    }
}