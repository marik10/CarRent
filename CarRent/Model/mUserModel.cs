using System;
using System.Collections.Generic;

namespace CarRent.Model
{
    //OLD
    class mUserModel : mLogin
    {
        public IEnumerable<Avaiabilities> GetAvaiabilities() { throw new NotImplementedException(); }
        public IEnumerable<Bodies> GetBodies() { throw new NotImplementedException(); }
        public IEnumerable<Brands> GetBrands() { throw new NotImplementedException(); }
        public IEnumerable<Cars> GetCars() { throw new NotImplementedException(); }
        public IEnumerable<MileageHistory> GetMileageHistories() { throw new NotImplementedException(); }
        public IEnumerable<Models> GetModels() { throw new NotImplementedException(); }
        public IEnumerable<Rents> GetRents() { throw new NotImplementedException(); }
        public IEnumerable<Transmissions> GetTransmissions() { throw new NotImplementedException(); }
        public IEnumerable<Users> GetUsers() { throw new NotImplementedException(); }
    }
}
