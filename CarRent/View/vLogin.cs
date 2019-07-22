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
        private readonly ILanguage Language;

        public vLogin()
        {
            Language = new PL();
        }

        /// <summary>
        /// Prepare first menu.
        /// </summary>
        /// <returns>Selected option.</returns>
        public int Begin()
        {
            Console.Clear();
            string[] Options = new string[]
            {
                Language.SignIn,
                Language.SignUp,
                Language.Exit
            };
            SelectingByList(Options);
            var result = Selector(Options.Count());
            return result;
        }

        /// <summary>
        /// Shows sign in.
        /// </summary>
        /// <returns>Login Data object.</returns>
        public oLoginData SignIn()
        {
            Console.Clear();
            oLoginData ld = new oLoginData();
            TypingByList(new string[]
            {
                Language.UserName,
                Language.Password
            });
            Console.SetCursorPosition(42, 0);
            ld.Login = TextCatcher(true);
            if (ld.Login.Equals(""))
                return null;
            Console.SetCursorPosition(42, 1);
            ld.Password = TextCatcher(false);
            return ld;
        }

        /// <summary>
        /// Shows sign up.
        /// </summary>
        /// <returns>Object user with data or null if interrupted.</returns>
        public oUser SignUp()
        {
            Console.Clear();
            string[] options = new string[]
            {
                Language.UserName,
                Language.Password,
                Language.CityName,
                Language.CountryName,
                Language.Address,
                Language.Name,
                Language.Phone,
                Language.Email
            };
            TypingByList(options);
            var result = MenuReaderCatcher(options.Count());
            Console.Write("\n");
            return result;
        }

        /// <summary>
        /// Shows any exception and wait 8 sec.
        /// </summary>
        /// <param name="e">An exception to show.</param>
        public void ShowException(Exception e)
        {
            Console.WriteLine("\n" + e + "\n");
            Thread.Sleep(8000);
        }

        /// <summary>
        /// Shows login error text and wait 5 sec.
        /// </summary>
        public void ShowLoginError()
        {
            Console.WriteLine("\n" + Language.WrongLogin + "\n");
            Thread.Sleep(5000);
        }

        /// <summary>
        /// Ask the user for new data. 
        /// </summary>
        /// <param name="errorName">"password" for secret password, "email", "phone" or "login" for visible data, 
        /// "incorrectaddress", "incorrectcityname", "incorrectcountryname" "incorrectemail", "incorrectname", 
        /// "incorrectpassword", "incorrectphone", "incorrectusername" for wrong pattern words.</param>
        /// <param name="length">Minimal length of correct data.</param>
        /// <returns>input by user or empty string.</returns>
        public string AskForRepeat(string errorName, int length = 0)
        {
            string result;
            switch (errorName)
            {
                case "password":
                    result = AskForField(Language.WrongPassword, false);
                    return result;
                case "email":
                    result = AskForField(Language.ExistingEmail, true);
                    return result;
                case "phone":
                    result = AskForField(Language.ExistingPhone, true);
                    return result;
                case "login":
                    result = AskForField(Language.ExistingLogin, true);
                    return result;
                case "incorrectaddress":
                    result = AskForField(Language.IncorrectAddress(length), true);
                    return result;
                case "incorrectcityname":
                    result = AskForField(Language.IncorrectCityName(length, "XX-XXX CityName"), true);
                    return result;
                case "incorrectcountryname":
                    result = AskForField(Language.IncorrectCountryName(length), true);
                    return result;
                case "incorrectemail":
                    result = AskForField(Language.IncorrectEmail(length, "@"), true);
                    return result;
                case "incorrectname":
                    result = AskForField(Language.IncorrectName(length), true);
                    return result;
                case "incorrectpassword":
                    result = AskForField(Language.IncorrectPassword(length), true);
                    return result;
                case "incorrectphone":
                    result = AskForField(Language.IncorrectPhone(length), true);
                    return result;
                case "incorrectusername":
                    result = AskForField(Language.IncorrectUserName(length), true);
                    return result;
                default:
                    throw new FormatException("The parameter is incorrect!");
            }
        }

        /// <summary>
        /// Ask again for repeat a single variable.
        /// </summary>
        /// <param name="errorName">Name of error exist.</param>
        /// <param name="isVisible">true for visible input, false for stars input.</param>
        /// <returns>input data or empty string if interrupted.</returns>
        private string AskForField(string errorName, bool isVisible)
        {
            Console.WriteLine("\n" + errorName + "\n");
            string result = TextCatcher(isVisible);
            return result;
        }

        /// <summary>
        /// Reads user data.
        /// </summary>
        /// <param name="OptionsCount">Count (lines) to insert.</param>
        /// <returns>oUser object with data or null if interrupted.</returns>
        private oUser MenuReaderCatcher(int optionsCount)
        {
            string[] tableOfResult = new string[optionsCount];
            for (int i = 0; i < optionsCount; i++)
            {
                Console.SetCursorPosition(42, i);
                string text = TextCatcher(true);
                if (text.Equals(""))
                    return (null);
                tableOfResult[i] = text;
            }
            var result = TabStringToUser(tableOfResult);
            return result;
        }

        /// <summary>
        /// Convert string table to user (verify the order).
        /// </summary>
        /// <param name="data">Table of input data.</param>
        /// <returns>oUser object with data.</returns>
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

        /// <summary>
        /// Catching the input text.
        /// </summary>
        /// <param name="isVisible">True show signs, false show stars.</param>
        /// <returns>Input line or empty string (if interrupted).</returns>
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
                            Backspace();
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

        /// <summary>
        /// Removes last character, backspace functionality.
        /// </summary>
        private void Backspace()
        {
            Console.CursorLeft -= 1;
            Console.Write(" ");
            Console.CursorLeft -= 1;
        }

        /// <summary>
        /// Draws list of options with typing as input method.
        /// </summary>
        /// <param name="optionsCount">All options to write.</param>
        private void TypingByList(string[] optionsCount)
        {

            foreach (string Option in optionsCount)
            {
                Console.WriteLine("{0,-40} : ", Option);
            }
        }

        /// <summary>
        /// Draws list of options with selecting option as star.
        /// </summary>
        /// <param name="optionsCount">All options to write.</param>
        private void SelectingByList(string[] optionsCount)
        {
            foreach (string Option in optionsCount)
            {
                Console.WriteLine("   {0}", Option);
            }
        }

        /// <summary>
        /// Selecting one of with up and down arrow.
        /// </summary>
        /// <param name="optionsCount">Count (lines) to select width.</param>
        /// <returns>Selected number, from 0 to OptionsCount-1.</returns>
        private int Selector(int optionsCount)
        {
            optionsCount--;
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
                        return optionsCount;
                    case ConsoleKey.UpArrow:
                        current = SelectAbove(current);
                        break;
                    case ConsoleKey.DownArrow:
                        current = SelectBelow(current, optionsCount);
                        break;
                }
            }
        }

        /// <summary>
        /// Chosing the option below.
        /// </summary>
        /// <param name="current">Current selected row.</param>
        /// <param name="optionsCount">Number of options.</param>
        /// <returns>Row below or current (if on the end).</returns>
        private int SelectBelow(int current, int optionsCount)
        {
            if (current < optionsCount)
            {
                MoveSelection(current, current + 1);
                current++;
            }
            return current;
        }

        /// <summary>
        /// Chosing the option above.
        /// </summary>
        /// <param name="current">Current selected row.</param>
        /// <returns>Row above or current (if on the end).</returns>
        private int SelectAbove(int current)
        {
            if (current > 0)
            {
                MoveSelection(current, current - 1);
                current--;
            }
            return current;
        }

        /// <summary>
        /// Moving selected row with star.
        /// </summary>
        /// <param name="oldPosition">Old position of star (clear).</param>
        /// <param name="newPosition">New position of star.</param>
        private void MoveSelection(int oldPosition, int newPosition)
        {
            Console.SetCursorPosition(1, oldPosition);
            Console.Write(" ");
            Console.SetCursorPosition(1, newPosition);
            Console.Write("*");
        }

    }
}
