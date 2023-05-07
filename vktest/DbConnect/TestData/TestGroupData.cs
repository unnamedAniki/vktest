using NuGet.Packaging;
using vktest.Models;

namespace vktest.DbConnect.TestData
{
    public class TestGroupData : ITestData<UserGroup>
    {
        private readonly List<UserGroup> _groups;
        public TestGroupData()
        {
            _groups = new List<UserGroup>();
            _groups.AddRange(
                new[]
                {
                    new UserGroup()
                    {
                        Id = 1,
                        Code = "Admin",
                        Description = "Admin group"
                    },
                    new UserGroup()
                    {
                        Id = 2,
                        Code = "User",
                        Description = "User group"
                    }
                });
        }
        public List<UserGroup> GetData()
        {
            return _groups;
        }
    }
}
