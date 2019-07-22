using System;
using System.Linq;

namespace CarRent.Model
{
    class mLogin
    {
        private readonly DatabaseEntities Context;
        public Exception LastException { get; private set; }
        public bool ExistingEmail { get; private set; }
        public bool ExistingPhone { get; private set; }
        public bool ExistingLogin { get; private set; }

        public mLogin()
        {
            Context = new DatabaseEntities();
            LastException = null;
            ExistingEmail = false;
            ExistingPhone = false;
            ExistingLogin = false;
        }

        public string SignIn(string login)
        {
            LastException = null;
            try
            {
                var query = from us in Context.Users
                            where us.Login == login
                            select us;
                if (query.Any())
                {
                    var result = query.ToList()[0].Password;
                    return result;
                }
                else
                    return "";
            }
            catch (Exception e)
            {
                LastException = e;
                return "";
            }
        }

        public void SignUp(oUser oUser)
        {
            LastException = null;
            ExistingEmail = false;
            ExistingPhone = false;
            ExistingLogin = false;
            try
            {
                var query = Context.Users.Where(
                    a => a.Email == oUser.Email ||
                    a.Phone == oUser.Phone ||
                    a.Login == oUser.UserName);
                if (query.Any())
                {
                    var query2 = query.Where(a => a.Email == oUser.Email);
                    if (query2.Any())
                    {
                        ExistingEmail = true;
                    }

                    var query3 = query.Where(a => a.Phone == oUser.Phone);
                    if (query3.Any())
                    {
                        ExistingPhone = true;
                    }

                    var query4 = query.Where(a => a.Login == oUser.UserName);
                    if (query4.Any())
                    {
                        ExistingLogin = true;
                    }
                    return;
                }

                var countryId = getCountryId(oUser.CountryName);
                if (countryId == -1)
                    return;

                var cityId = getCityId(oUser.CityName, countryId);
                if (cityId == -1)
                    return;

                Users user = new Users()
                {
                    Login = oUser.UserName,
                    Password = oUser.Password,
                    Address = oUser.Address,
                    Name = oUser.Name,
                    Phone = oUser.Phone,
                    Email = oUser.Email,
                    StatusId = 2,
                    CountryId = countryId,
                    CityId = cityId
                };

                Context.Users.Add(user);
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                LastException = e;
            }
        }

        private int getCountryId(string countryName)
        {
            LastException = null;
            try
            {
                var query = Context.Countries.ToList().Where(a => a.CountryName.Equals(countryName));
                if (query.Count() == 0)
                {
                    Context.Countries.Add(new Countries() { CountryName = countryName });
                    Context.SaveChanges();

                    var query2 = Context.Countries.ToList().Max(a => a.CountryId);
                    return query2;
                }
                else
                {
                    int id = query.First().CountryId;
                    return id;
                }
            }
            catch (Exception e)
            {
                LastException = e;
                return -1;
            }
        }

        private int getCityId(string cityName, int countryId)
        {
            LastException = null;
            try
            {
                var query = Context.Cities.ToList().Where(a => a.CityName == cityName && a.CountryId == countryId);
                if (query.Count() == 0)
                {
                    Context.Cities.Add(new Cities()
                    {
                        CityName = cityName,
                        CountryId = countryId
                    });
                    Context.SaveChanges();
                    var query2 = Context.Cities.ToList().Max(a => a.CityId);
                    return query2;
                }
                else
                {
                    int id = query.First().CountryId;
                    return id;
                }
            }
            catch (Exception e)
            {
                LastException = e;
                return -1;
            }
        }
    }
}
