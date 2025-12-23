using SmHm.Core.Abstractions;

namespace SmHm.IntegrationTests.FakeDependencies
{
    public class FakeCurrentUserService : ICurrentUserService
    {
        public Guid UserId => Guid.Parse("11111111-1111-1111-1111-111111111111");

        public string UserName => "integration-test-user";

        public bool IsAuthenticated => true;
    }
}
