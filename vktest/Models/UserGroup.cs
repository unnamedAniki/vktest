namespace vktest.Models
{
    public class UserGroup : BaseModel
    {
        public UserGroup() 
        {
            Users = new HashSet<User>();
        }
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public virtual ICollection<User>? Users { get; set; }
        public enum UserGroupState
        {
            Admin = 1,
            User = 2,
        }
    }
}
