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
            ml.SignUp(s);
        }
    }
}
