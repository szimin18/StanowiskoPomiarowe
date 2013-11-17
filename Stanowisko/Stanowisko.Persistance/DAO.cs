namespace Stanowisko.Persistance
{
    public abstract class DAO
    {
        protected readonly ISQLiteDatabase _db = new SQLiteDatabase();

        protected DAO(ISQLiteDatabase db)
        {
            _db = db;
        }
    }
}
