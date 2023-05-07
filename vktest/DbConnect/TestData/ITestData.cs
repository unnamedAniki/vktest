
namespace vktest.DbConnect.TestData
{
    interface ITestData<T> where T : class
    {
        public List<T> GetData();
    }
}
