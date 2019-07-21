using CarRent.Controler;
using CarRent.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    class Program
    {
        static void Main(string[] args)
        {
            (new cLogin()).Begin();
            /*
            var context = new DatabaseEntities();
            Countries c = new Countries() { CountryName = "Poland" };
            context.Countries.Add(c);
            //context.Entry(c).State = EntityState.Added;
            context.SaveChanges();
            foreach (var a in context.Countries.ToList())
            {
                Console.WriteLine(a.CountryName);
            }
            
            var query = context.Avaiabilities.Where(s => s.AvaiabilityName == ("W serwisie")).FirstOrDefault();
            Console.Write(query.AvaiabilityId);
            var query2 = from av in context.Avaiabilities
                         where av.AvaiabilityName == "W serwisie"
                         select av;
            Console.Write(query2.FirstOrDefault().AvaiabilityId);
            */

            //Console.ReadKey();

        }
    }
}
