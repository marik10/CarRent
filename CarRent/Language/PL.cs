namespace CarRent.Language
{
    class PL : ILanguage
    {
        public string UserName => "Nazwa użytkownika";

        public string Password => "Hasło";

        public string SignIn => "Zaloguj się";

        public string SignUp => "Zarejestruj się";

        public string Exit => "Koniec";

        public string Return => "Powrót";

        public string LoginError => "BŁĄD! Login jest nieprawidłowy";

        public string PasswordError => "BŁĄD! Hasło jest nieprawidłowe";

        public string CityName => "Miasto";

        public string CountryName => "Kraj";

        public string Address => "Adres zamieszkania";

        public string Name => "Imię i nazwisko";

        public string Phone => "Telefon";

        public string Email => "Email";

        public string ErrorEmail => "Ten adres email jest przypisany do innego konta. Podaj inny:";

        public string ErrorPhone => "Ten numer telefonu jest przypisany do innego konta. Podaj inny:";

        public string ErrorLogin => "Ten login jest przypisany do innego konta. Podaj inny:";
    }
}
