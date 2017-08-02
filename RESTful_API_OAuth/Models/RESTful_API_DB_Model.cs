namespace RESTful_API_OAuth.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class RESTful_API_DB_Model : DbContext
    {
        public RESTful_API_DB_Model() : base("name=RESTful_API_DB_Model")
        {
            Database.SetInitializer(new InitDatabase());
        }

        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}