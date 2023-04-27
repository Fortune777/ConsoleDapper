using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleDapper.Model;

namespace ConsoleDapper
{
    class Program
    {
        static void Main(string[] args)
        {
            GenericRepository<User> repo = new UserRepository();
           // repo.CreateTable();

            var result = repo.GetAllAsync("select * from Users").Result.ToList() ;

            //var adduser = new User
            //{
            //    Age = 43,
            //    Name = "Kid"
            //};
            
            //repo.Create(adduser);
        }
    }
}
