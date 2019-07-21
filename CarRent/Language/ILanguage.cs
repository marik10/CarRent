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

        string WrongLogin { get; }
        string WrongPassword { get; }

        string ExistingEmail { get; }
        string ExistingPhone { get; }
        string ExistingLogin { get; }

        string IncorrectUserName(int length);
        string IncorrectPassword(int length);
        string IncorrectCityName(int length, string regex);
        string IncorrectCountryName(int length);
        string IncorrectAddress(int length);
        string IncorrectName(int length);
        string IncorrectPhone(int length);
        string IncorrectEmail(int length, string regex);
    }
}
