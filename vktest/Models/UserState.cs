namespace vktest.Models
{
    public class UserState : BaseModel
    {
        public UserState()
        {
            Users = new HashSet<User>();
        }
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public virtual ICollection<User>? Users { get; set; }
        public enum UserStatesState
        {
            Active = 1,
            Blocked = 2,
        }
    }
}
