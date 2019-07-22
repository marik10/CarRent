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
        }
    }
}
