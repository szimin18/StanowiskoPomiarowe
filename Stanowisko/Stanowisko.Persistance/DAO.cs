namespace Stanowisko.Persistance
{
    public abstract class DAO
    {
        protected readonly SQLiteDatabase _db = new SQLiteDatabase();

        protected DAO(SQLiteDatabase db)
        {
            _db = db;
        }
    }
}
