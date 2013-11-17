namespace Stanowisko.Persistance
{
    public abstract class DAO
    {
        protected readonly IDatabase _db = new SQLiteDatabase();

        protected DAO(IDatabase db)
        {
            _db = db;
        }
    }
}
