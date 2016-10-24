namespace NgTodoList.Api.Models.Account
{
    public class RegisterUserViewModel
    {
        //reflete o estado de um objeto
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}