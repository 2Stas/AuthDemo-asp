namespace AuthDemo.Models
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; } = "";
        public string email { get; set; } = "";
        public string password { get; set; } = "";
        public const string RoleDefault = "DefaultUser";
        public const string RolePro = "ProUser";

        public string role { get; set; } = RoleDefault;
    }
}
