using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RESTful_API_OAuth.Models
{
    public class InitDatabase : DropCreateDatabaseAlways<RESTful_API_DB_Model>
    {
        protected override void Seed(RESTful_API_OAuth.Models.RESTful_API_DB_Model context)
        {
            List<Roles> rolesList = new List<Roles>();
            rolesList.Add(new Roles { RoleName = "Admin" });
            rolesList.Add(new Roles { RoleName = "Member" });
            rolesList.Add(new Roles { RoleName = "Visit" });

            context.Roles.AddRange(rolesList);

            List<Status> statusList = new List<Status>();
            statusList.Add(new Status { StatusName = "Normal" });
            statusList.Add(new Status { StatusName = "Not Enabled" });
            statusList.Add(new Status { StatusName = "Block" });
            statusList.Add(new Status { StatusName = "Disabled" });

            context.Status.AddRange(statusList);

            context.SaveChanges();

            List<Users> usersList = new List<Users>();
            usersList.Add(new Users
            {
                Id = Guid.NewGuid().ToString(),
                Email = "admin@test.com",
                Username = "admin",
                Password = "admin",
                Name = "Administrator",
                PhoneNumber = "0000-0000",
                EditTime = DateTime.Now,

                Roles = context.Roles.FirstOrDefault(x => x.RoleName == "Admin"),
                Status = context.Status.FirstOrDefault(x => x.StatusName == "Normal")
            });
            usersList.Add(new Users
            {
                Id = Guid.NewGuid().ToString(),
                Email = "member@test.com",
                Username = "member",
                Password = "member",
                Name = "Member1",
                PhoneNumber = "0000-1111",
                EditTime = DateTime.Now,

                Roles = context.Roles.FirstOrDefault(x => x.RoleName == "Member"),
                Status = context.Status.FirstOrDefault(x => x.StatusName == "Normal")
            });
            usersList.Add(new Users
            {
                Id = Guid.NewGuid().ToString(),
                Email = "visit@test.com",
                Username = "visit",
                Password = "visit",
                Name = "Visit",
                PhoneNumber = "0000-2222",
                EditTime = DateTime.Now,

                Roles = context.Roles.FirstOrDefault(x => x.RoleName == "Visit"),
                Status = context.Status.FirstOrDefault(x => x.StatusName == "Normal")
            });

            context.Users.AddRange(usersList);

            context.SaveChanges();
        }
    }
}