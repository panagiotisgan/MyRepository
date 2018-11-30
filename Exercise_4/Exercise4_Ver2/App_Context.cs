namespace Exercise4_Ver2
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class App_Context : DbContext
    {
       
        // Your context has been configured to use a 'App_Context' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Exercise4_Ver2.App_Context' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'App_Context' 
        // connection string in the application configuration file.
        public App_Context()
            : base("name=App_Context")
        {
        //public override void OnModelCreating(DbModelBuilder modelBuilder)
        //         {
        //    modelBuilder.Entity<Worker>()
        //    .Property(p => p.WeekSalary)
        //    .
            
        //         }
        }

    // Add a DbSet for each entity type that you want to include in your model. For more information 
    // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

    public virtual DbSet<Student> Students { get; set; }
    public virtual DbSet<Worker> Workers { get; set; }
}

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}