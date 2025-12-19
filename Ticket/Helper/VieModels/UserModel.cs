namespace Helper.VieModels;
public class UserViewModel : BaseEntity
{
    public string FullName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }

    public string Role { get; set; }
    public string Token { get; set; }

}
public class LoginRequestViewModel
{
    public string Email { get; set; }
    public string Password { get; set; }

}

