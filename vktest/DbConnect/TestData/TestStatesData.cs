using vktest.Models;

namespace vktest.DbConnect.TestData
{
    public class TestStatesData : ITestData<UserState>
    {
        private readonly List<UserState> _states;
        public TestStatesData()
        {
            _states = new List<UserState>();
            _states.AddRange(
                new[]
                {
                    new UserState()
                    {
                        Id = 1,
                        Code = "Active",
                        Description = "Active state"
                    },
                    new UserState()
                    {
                        Id = 2,
                        Code = "Blocked",
                        Description = "Blocked user"
                    }
                });
        }
        public List<UserState> GetData()
        {
            return _states;
        }
    }
}
