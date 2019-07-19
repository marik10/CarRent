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
            string[] Options = new string[] { language.SignIn, language.SignUp, language.Exit };
            SelectingByList(Options);
            return Selector(Options.Count());
        }

        public oLogin SignIn()
        {
            Console.Clear();
            oLogin ld = new oLogin();
            TypingByList(new string[] { language.UserName, language.Password });
            Console.SetCursorPosition(24, 0);
            ld.Login = TextCatcher(true);
            if (ld.Login.Equals(""))
                return null;
            Console.SetCursorPosition(24, 1);
            ld.Password = TextCatcher(false);
            return ld;
        }

        public oUser SignUp()
        {
            Console.Clear();
            string[] Options = new string[]
            {
                language.UserName, language.Password,
                language.CityName, language.CountryName,
                language.Address, language.Name,
                language.Phone, language.Email
            };
            TypingByList(Options);
            var result = MenuReaderCatcher(Options.Count());
            return result;
        }

        public void ShowException(Exception e)
        {
            Console.WriteLine("\n" + e);
            Thread.Sleep(8000);
        }

        public void ShowLoginError()
        {
            Console.WriteLine("\n" + language.LoginError);
            Thread.Sleep(5000);
        }
        
        /// <summary>
        /// Ask the user for new data. 
        /// </summary>
        /// <param name="errorName">"password" for secret password, "email", "phone" or "login" for visible data.</param>
        /// <returns>input by user or empty string.</returns>
        public string AskForRepeat(string errorName)
        {
            string result;
            switch (errorName)
            {
                case "password":
                    result = AskForField(language.ErrorPassword, false);
                    return result;
                case "email":
                    result = AskForField(language.ErrorEmail, true);
                    return result;
                case "phone":
                    result = AskForField(language.ErrorPhone, true);
                    return result;
                case "login":
                    result = AskForField(language.ErrorLogin, true);
                    return result;
                default:
                    throw new FormatException("The parameter is incorrect!");
            }
        }

        private string AskForField(string errorName, bool isVisible)
        {
            Console.WriteLine(errorName);
            string result = TextCatcher(isVisible);
            return result;
        }

        private oUser MenuReaderCatcher(int Options)
        {
            string[] result = new string[Options];
            for (int i = 0; i < Options; i++)
            {
                Console.SetCursorPosition(24, i);
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
                if (key.Key == ConsoleKey.Escape)
                {
                    return "";
                }
                else if (key.Key == ConsoleKey.Enter)
                    return text;
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (text.Length > 0)
                    {
                        RemoveLastKey();
                        text = text.Substring(0, text.Length - 1);
                    }
                }
                else if (Regex.IsMatch(key.KeyChar.ToString(), @"^[a-zA-Z0-9]+$"))
                {
                    text += key.KeyChar;
                    if (isVisible)
                        Console.Write(key.KeyChar);
                    else
                        Console.Write("*");
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
                Console.WriteLine("{0,-22} : ", Option);
            }
        }

        private int Selector(int OptionsCount)
        {
            OptionsCount--;
            int current = 0;
            MoveSelection(current);
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    break;
                else if (key.Key == ConsoleKey.Escape)
                {
                    current = OptionsCount - 1;
                    break;
                }
                else if (key.Key == ConsoleKey.UpArrow)
                {
                    SelectAbove(current);
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    SelectBelow(current, OptionsCount);
                }
            }
            return current;
        }

        private void SelectBelow(int current, int OptionsCount)
        {
            if (current < OptionsCount)
                MoveSelection(++current);
        }

        private void SelectAbove(int current)
        {
            if (current > 0)
                MoveSelection(--current);
        }

        private void MoveSelection(int Position)
        {
            Console.Write(" ");
            Console.SetCursorPosition(1, Position);
            Console.Write("*");
            Console.CursorLeft = 1;
        }

    }
}
