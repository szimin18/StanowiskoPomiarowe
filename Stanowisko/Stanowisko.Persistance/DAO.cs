namespace Stanowisko.Persistance
{
    public abstract class DAO
    {
        protected readonly IDatabase Db = new SQLiteDatabase();

        protected DAO(IDatabase db)
        {
            Db = db;
        }

    }
}
