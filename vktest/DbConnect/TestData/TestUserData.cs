using vktest.Models;

namespace vktest.DbConnect.TestData
{
    public class TestUserData : ITestData<User>
    {
        private readonly List<User> _users;
        public TestUserData()
        {
            _users = new List<User>();
            _users.AddRange(
                new[]
                {
                    new User()
                    {
                        Id = 1,
                        Login = "Test1",
                        Password = "123",
                        User_Group_Id = 1,
                        User_State_Id = 2
                    },
                    new User()
                    {
                        Id = 2,
                        Login = "Test2",
                        Password = "1234",
                        User_Group_Id = 2,
                        User_State_Id = 1
                    }
                });
        }
        public List<User> GetData()
        {
            return _users;
        }
    }
}
