namespace CarRent.Language
{
    class PL : ILanguage
    {
        public string UserName => "Nazwa użytkownika";

        public string Password => "Hasło";

        public string CityName => "Kod pocztowy i miasto";

        public string CountryName => "Kraj";

        public string Address => "Ulica i numer domu/mieszkania";

        public string Name => "Imię i nazwisko";

        public string Phone => "Telefon";

        public string Email => "Email";


        public string SignIn => "Zaloguj się";

        public string SignUp => "Zarejestruj się";

        public string Exit => "Koniec";

        public string Return => "Powrót";


        public string WrongLogin => "BŁĄD! Login jest nieprawidłowy.";

        public string WrongPassword => "BŁĄD! Hasło jest nieprawidłowe Podaj hasło ponownie:";


        public string ExistingEmail => "Ten adres email jest przypisany do innego konta. Podaj inny:";

        public string ExistingPhone => "Ten numer telefonu jest przypisany do innego konta. Podaj inny:";

        public string ExistingLogin => "Ten login jest przypisany do innego konta. Podaj inny:";


        public string IncorrectAddress(int length) => "Adres powinien mieć długość co najmniej " + length + " znaków.";

        public string IncorrectCityName(int length, string regex) => "Kod pocztowy i miasto powinno być podane w formacie " + regex;

        public string IncorrectCountryName(int length) => "Nazwa kraju powinna mieć co najmniej " + length + " znaków.";

        public string IncorrectEmail(int length, string regex) => "Email jest niepoprawny.";

        public string IncorrectName(int length) => "Imię i nazwisko powinny zawierać co najmniej " + length + " znaków.";

        public string IncorrectPassword(int length) => "Hasło powinno zawierać co najmniej " + length + " znaków.";

        public string IncorrectPhone(int length) => "Telefon jest niepoprawny.";

        public string IncorrectUserName(int length) => "Nazwa użytkownika powinna zawierać co najmniej " + length + " znaków.";
    }
}
