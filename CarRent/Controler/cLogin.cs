using CarRent.Model;
using CarRent.View;
using System;
using System.Text.RegularExpressions;

namespace CarRent.Controler
{
    class cLogin
    {
        private readonly vLogin vl;
        private readonly mLogin ml;

        public cLogin()
        {
            vl = new vLogin();
            ml = new mLogin();
        }

        /// <summary>
        /// Reading code from first menu.
        /// </summary>
        public void Begin()
        {
            while (true)
            {
                int choice = vl.Begin();
                switch (choice)
                {
                    case 0:
                        SignIn();
                        break;
                    case 1:
                        SignUp();
                        break;
                    case 2:
                        return;
                }
            }
        }

        /// <summary>
        /// Management of signing in.
        /// </summary>
        public void SignIn()
        {
            oLoginData loginData;

            while (true)
            {
                loginData = vl.SignIn();
                if (loginData is null)
                {
                    return;
                }
                else
                {
                    string truePassword = ml.SignIn(loginData.Login);
                    if (truePassword.Equals(""))
                    {
                        if (ml.LastException is null)
                            vl.ShowLoginError();
                        else
                            vl.ShowException(ml.LastException);
                    }
                    else
                    {
                        if (loginData.Password.Equals(truePassword))
                        {
                            //Nadać parametry użytkownika
                            throw new NotImplementedException(
                                "Everything is good, but the app is in alpha and the functionality is in development");
                        }
                        else
                        {
                            do
                            {
                                loginData.Password = vl.AskForRepeat("password");
                            }
                            while (!(loginData.Password is null ||
                            loginData.Password.Equals("") ||
                            loginData.Password.Equals(truePassword)));

                            if (loginData.Password is null ||
                            loginData.Password.Equals(""))
                                return;
                            else
                            {
                                //Nadać parametry użytkownika
                                throw new NotImplementedException(
                                    "Everything is good, but the app is in alpha and the functionality is in development");
                            }

                        }
                    }
                }
            }
        }

        /// <summary>
        /// Management of signing up.
        /// </summary>
        public void SignUp()
        {
            oUser user = vl.SignUp();
            if (user is null)
                return;
            else
            {
                VerifyTypeData(user);

                if (user is null)
                    return;
                else
                    VerifyCorrectData(user);
            }
        }

        /// <summary>
        /// Helper for single field ask.
        /// </summary>
        /// <param name="data">Input string with data.</param>
        /// <param name="name">Specified View/vLogin/AskForRepeat param.</param>
        /// <param name="minimalLength">Minimal length of data.</param>
        /// <param name="pattern">Additional condition pattern.</param>
        /// <returns>Correct data matching pattern (original or repeated). Empty string if interrupted.</returns>
        private string SingleRepeat(string data, string name, int minimalLength, Func<string, bool> pattern)
        {
            while (data.Length < minimalLength || !pattern(data))
            {
                data = vl.AskForRepeat(name, minimalLength);
                if (data is null || data.Equals(""))
                    return "";
            }
            return data;
        }

        /// <summary>
        /// Verify that every user params is matching pattern.
        /// </summary>
        /// <param name="user">Checking user.</param>
        /// <returns>True if every param is good, false if interrupted.</returns>
        private bool VerifyTypeData(oUser user)
        {
            bool returnTrue(string a) => true;
            bool cityName(string c) => Regex.IsMatch(c, @"^\d{2}-\d{3} ");
            bool email(string c) => c.Contains("@");

            user.Address = SingleRepeat(user.Address, "incorrectaddress", 3, returnTrue);
            if (user.Address.Equals(""))
                return false;

            user.CityName = SingleRepeat(user.CityName, "incorrectcityname", 8, cityName);
            if (user.CityName.Equals(""))
                return false;

            user.CountryName = SingleRepeat(user.CountryName, "incorrectcountryname", 2, returnTrue);
            if (user.CountryName.Equals(""))
                return false;

            user.Email = SingleRepeat(user.Email, "incorrectemail", 3, email);
            if (user.Email.Equals(""))
                return false;

            user.Name = SingleRepeat(user.Name, "incorrectname", 2, returnTrue);
            if (user.Name.Equals(""))
                return false;

            user.Password = SingleRepeat(user.Password, "incorrectpassword", 6, returnTrue);
            if (user.Password.Equals(""))
                return false;

            user.Phone = SingleRepeat(user.Phone, "incorrectphone", 7, returnTrue);
            if (user.Phone.Equals(""))
                return false;

            user.UserName = SingleRepeat(user.UserName, "incorrectusername", 4, returnTrue);
            if (user.UserName.Equals(""))
                return false;

            return true;
        }

        /// <summary>
        /// Checking the uniqueness of user data.
        /// </summary>
        /// <param name="user">Checking user.</param>
        private void VerifyCorrectData(oUser user)
        {
            ml.SignUp(user);

            while (ml.ExistingEmail)
            {
                user.Email = vl.AskForRepeat("email");
                if (user.Email is null || user.Email.Equals(""))
                    return;
                ml.SignUp(user);
            }

            while (ml.ExistingLogin)
            {
                user.UserName = vl.AskForRepeat("login");
                if (user.UserName is null || user.UserName.Equals(""))
                    return;
                ml.SignUp(user);
            }

            while (ml.ExistingPhone)
            {
                user.Phone = vl.AskForRepeat("phone");
                if (user.Phone is null || user.Phone.Equals(""))
                    return;
                ml.SignUp(user);
            }
        }
    }
}
