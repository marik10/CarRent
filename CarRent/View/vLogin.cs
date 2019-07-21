using CarRent.Language;
using CarRent.Model;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace CarRent.View
{
    class vLogin
    {
        private ILanguage language;

        public vLogin()
        {
            language = new PL();
        }

        public int Begin()
        {
            Console.Clear();
            string[] Options = new string[]
            {
                language.SignIn,
                language.SignUp,
                language.Exit
            };
            SelectingByList(Options);
            var result = Selector(Options.Count());
            return result;
        }

        public oLogin SignIn()
        {
            Console.Clear();
            oLogin ld = new oLogin();
            TypingByList(new string[]
            {
                language.UserName,
                language.Password
            });
            Console.SetCursorPosition(42, 0);
            ld.Login = TextCatcher(true);
            if (ld.Login.Equals(""))
                return null;
            Console.SetCursorPosition(42, 1);
            ld.Password = TextCatcher(false);
            return ld;
        }

        public oUser SignUp()
        {
            Console.Clear();
            string[] Options = new string[]
            {
                language.UserName,
                language.Password,
                language.CityName,
                language.CountryName,
                language.Address,
                language.Name,
                language.Phone,
                language.Email
            };
            TypingByList(Options);
            var result = MenuReaderCatcher(Options.Count());
            Console.Write("\n");
            return result;
        }

        public void ShowException(Exception e)
        {
            Console.WriteLine("\n" + e + "\n");
            Thread.Sleep(8000);
        }

        public void ShowLoginError()
        {
            Console.WriteLine("\n" + language.WrongLogin + "\n");
            Thread.Sleep(5000);
        }

        /// <summary>
        /// Ask the user for new data. 
        /// </summary>
        /// <param name="errorName">"password" for secret password, "email", "phone" or "login" for visible data.</param>
        /// <returns>input by user or empty string.</returns>
        public string AskForRepeat(string errorName, int length = 0)
        {
            string result;
            switch (errorName)
            {
                case "password":
                    result = AskForField(language.WrongPassword, false);
                    return result;
                case "email":
                    result = AskForField(language.ExistingEmail, true);
                    return result;
                case "phone":
                    result = AskForField(language.ExistingPhone, true);
                    return result;
                case "login":
                    result = AskForField(language.ExistingLogin, true);
                    return result;
                case "incorrectaddress":
                    result = AskForField(language.IncorrectAddress(length), true);
                    return result;
                case "incorrectcityname":
                    result = AskForField(language.IncorrectCityName(length, "XX-XXX CityName"), true);
                    return result;
                case "incorrectcountryname":
                    result = AskForField(language.IncorrectCountryName(length), true);
                    return result;
                case "incorrectemail":
                    result = AskForField(language.IncorrectEmail(length, "@"), true);
                    return result;
                case "incorrectname":
                    result = AskForField(language.IncorrectName(length), true);
                    return result;
                case "incorrectpassword":
                    result = AskForField(language.IncorrectPassword(length), true);
                    return result;
                case "incorrectphone":
                    result = AskForField(language.IncorrectPhone(length), true);
                    return result;
                case "incorrectusername":
                    result = AskForField(language.IncorrectUserName(length), true);
                    return result;
                default:
                    throw new FormatException("The parameter is incorrect!");
            }
        }

        private string AskForField(string errorName, bool isVisible)
        {
            Console.WriteLine("\n" + errorName + "\n");
            string result = TextCatcher(isVisible);
            return result;
        }

        private oUser MenuReaderCatcher(int Options)
        {
            string[] result = new string[Options];
            for (int i = 0; i < Options; i++)
            {
                Console.SetCursorPosition(42, i);
                string text = TextCatcher(true);
                if (text.Equals(""))
                    return (null);
                result[i] = text;
            }
            return TabStringToUser(result);
        }

        private oUser TabStringToUser(string[] data)
        {
            oUser result = new oUser()
            {
                UserName = data[0],
                Password = data[1],
                CityName = data[2],
                CountryName = data[3],
                Address = data[4],
                Name = data[5],
                Phone = data[6],
                Email = data[7]
            };
            return result;
        }

        private string TextCatcher(bool isVisible)
        {
            string text = "";
            while (true)
            {
                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Escape:
                        return "";
                    case ConsoleKey.Enter:
                        return text;
                    case ConsoleKey.Backspace:
                        if (text.Length > 0)
                        {
                            RemoveLastKey();
                            text = text.Substring(0, text.Length - 1);
                        }
                        break;
                    default:
                        if (Regex.IsMatch(key.KeyChar.ToString(),
                            @"^[a-zA-Z0-9 /@-]+$"))
                        {
                            text += key.KeyChar;
                            if (isVisible)
                                Console.Write(key.KeyChar);
                            else
                                Console.Write("*");
                        }
                        break;
                }
            }
        }

        private void RemoveLastKey()
        {
            Console.CursorLeft -= 1;
            Console.Write(" ");
            Console.CursorLeft -= 1;
        }

        private void SelectingByList(string[] Options)
        {
            foreach (string Option in Options)
            {
                Console.WriteLine("   {0}", Option);
            }
        }

        private void TypingByList(string[] Options)
        {

            foreach (string Option in Options)
            {
                Console.WriteLine("{0,-40} : ", Option);
            }
        }

        private int Selector(int OptionsCount)
        {
            OptionsCount--;
            int current = 0;
            MoveSelection(current, current);
            while (true)
            {
                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        return current;
                    case ConsoleKey.Escape:
                        return OptionsCount;
                    case ConsoleKey.UpArrow:
                        current = SelectAbove(current);
                        break;
                    case ConsoleKey.DownArrow:
                        current = SelectBelow(current, OptionsCount);
                        break;
                }
            }
        }

        private int SelectBelow(int current, int OptionsCount)
        {
            if (current < OptionsCount)
            {
                MoveSelection(current, current + 1);
                current++;
            }
            return current;
        }

        private int SelectAbove(int current)
        {
            if (current > 0)
            {
                MoveSelection(current, current - 1);
                current--;
            }
            return current;
        }

        private void MoveSelection(int oldPosition, int newPosition)
        {
            Console.SetCursorPosition(1, oldPosition);
            Console.Write(" ");
            Console.SetCursorPosition(1, newPosition);
            Console.Write("*");
        }

    }
}
