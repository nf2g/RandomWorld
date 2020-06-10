using System.Data.Entity;

namespace WebApplication1.Models
{
    public class Context : DbContext
    {
        public DbSet<WordSet> WordSets { get; set; }
    }

    //для пересоздания базы данных
    /*public class MyContextInitializer : DropCreateDatabaseAlways<Context>//пересоздает базу данных
    {

    }*/
}