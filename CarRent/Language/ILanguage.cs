namespace CarRent.Language
{
    interface ILanguage
    {
        string UserName { get; }
        string Password { get; }
        string CityName { get; }
        string CountryName { get; }
        string Address { get; }
        string Name { get; }
        string Phone { get; }
        string Email { get; }


        string SignIn { get; }
        string SignUp { get; }
        string Exit { get; }
        string Return { get; }
        string LoginError { get; }
        string ErrorPassword { get; }
        string ErrorEmail { get; }
        string ErrorPhone { get; }
        string ErrorLogin { get; }
    }
}
