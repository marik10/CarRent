using CarRent.Model;
using CarRent.View;
using System;

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
                            throw new NotImplementedException();
                        }
                        else
                        {
                            do
                            {
                                ld.Password = vl.AskForRepeat("password");
                            }
                            while (ld.Password is null ||
                            ld.Password.Equals("") ||
                            ld.Password.Equals(truePassword));

                            if (ld.Password is null ||
                            ld.Password.Equals(""))
                                return;
                            else
                            {
                                //Nadać parametry użytkownika
                                throw new NotImplementedException();
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
}
