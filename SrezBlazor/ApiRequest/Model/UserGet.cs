namespace SrezBlazor.ApiRequest.Model
{
    public class UserGet
    {
        public int id_User { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool isAdmin { get; set; }
        public bool Edit { get; set; } = false;
    }

    public class UserShort
    {
        UserGet userGet { get; set; }
        bool Status { get; set; }
    }
    public class UserProd
    {
        public int id_User { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool isAdmin { get; set; }
    }

    public class AddUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool isAdmin { get; set; }
    }
}
