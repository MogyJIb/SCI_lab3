using DbDataLibrary.Data;

namespace lab3.Utils
{
    public class DbInitializationUtil
    {
        public static void Init()
        {
            ToursSqliteDbContext dbContext = new ToursSqliteDbContext();
            DbInitializer<ToursSqliteDbContext>.Initialize(dbContext);
        }
   
    }
}
