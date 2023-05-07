namespace vktest.Models
{
    public class User : BaseModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime Created_Date { get; set; } = DateTime.UtcNow;
        public int User_Group_Id { get; set; }
        public UserGroup? UserGroup { get; set; }
        public int User_State_Id { get; set;}
        public UserState? UserState { get; set;}
    }
}
