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

        public void Begin()
        {
            while (true)
            {
                int Choice = vl.Begin();
                switch (Choice)
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

        public void SignIn()
        {
            oLogin ld;

            while (true)
            {
                ld = vl.SignIn();
                if (ld is null)
                {
                    return;
                }
                else
                {
                    string truePassword = ml.SignIn(ld.Login);
                    if (truePassword.Equals(""))
                    {
                        if (ml.LastException is null)
                            vl.ShowLoginError();
                        else
                            vl.ShowException(ml.LastException);
                    }
                    else
                    {
                        if (ld.Password.Equals(truePassword))
                        {
                            //Nadać parametry użytkownika
                            throw new NotImplementedException(
                                "Everything is good, but the app is in alpha and the functionality is in development");
                        }
                        else
                        {
                            do
                            {
                                ld.Password = vl.AskForRepeat("password");
                            }
                            while (!(ld.Password is null ||
                            ld.Password.Equals("") ||
                            ld.Password.Equals(truePassword)));

                            if (ld.Password is null ||
                            ld.Password.Equals(""))
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

        public void SignUp()
        {
            oUser s = vl.SignUp();
            if (s is null)
                return;
            else
            {
                VerifyTypeData(s);

                if (s is null)
                    return;
                else
                    VerifyCorrectData(s);
            }
        }

        private string Field(string data, string name, int minimallength, Func<string, bool> pattern)
        {
            while (data.Length < minimallength || !pattern(data))
            {
                data = vl.AskForRepeat(name, minimallength);
                if (data is null || data.Equals(""))
                    return "";
            }
            return data;
        }

        private bool VerifyTypeData(oUser s)
        {
            bool returnTrue(string a) => true;
            bool cityName(string c) => Regex.IsMatch(c, @"^\d{2}-\d{3} ");
            bool email(string c) => c.Contains("@");

            s.Address = Field(s.Address, "incorrectaddress", 3, returnTrue);
            if (s.Address.Equals(""))
                return false;

            s.CityName = Field(s.CityName, "incorrectcityname", 8, cityName);
            if (s.CityName.Equals(""))
                return false;

            s.CountryName = Field(s.CountryName, "incorrectcountryname", 2, returnTrue);
            if (s.CountryName.Equals(""))
                return false;

            s.Email = Field(s.Email, "incorrectemail", 3, email);
            if (s.Email.Equals(""))
                return false;

            s.Name = Field(s.Name, "incorrectname", 2, returnTrue);
            if (s.Name.Equals(""))
                return false;

            s.Password = Field(s.Password, "incorrectpassword", 6, returnTrue);
            if (s.Password.Equals(""))
                return false;

            s.Phone = Field(s.Phone, "incorrectphone", 7, returnTrue);
            if (s.Phone.Equals(""))
                return false;

            s.UserName = Field(s.UserName, "incorrectusername", 4, returnTrue);
            if (s.UserName.Equals(""))
                return false;

            return true;
        }

        private void VerifyCorrectData(oUser s)
        {
            ml.SignUp(s);

            while (ml.ExistingEmail)
            {
                s.Email = vl.AskForRepeat("email");
                if (s.Email is null || s.Email.Equals(""))
                    return;
                ml.SignUp(s);
            }

            while (ml.ExistingLogin)
            {
                s.UserName = vl.AskForRepeat("login");
                if (s.UserName is null || s.UserName.Equals(""))
                    return;
                ml.SignUp(s);
            }

            while (ml.ExistingPhone)
            {
                s.Phone = vl.AskForRepeat("phone");
                if (s.Phone is null || s.Phone.Equals(""))
                    return;
                ml.SignUp(s);
            }
        }
    }
}
