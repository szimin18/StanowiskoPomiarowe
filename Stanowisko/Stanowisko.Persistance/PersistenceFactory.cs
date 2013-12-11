namespace Stanowisko.Persistance
{
    public static class PersistenceFactory
    {

        //private static readonly IDatabase Database = new SQLiteDatabase("ex1.db");

        //private static readonly IDatabase Database = new XMLDatabase();

        private static readonly IDatabase Database = new InMemoryDatabase();

        public static IPersistenceManager GetPersistenceManager()
        {
            return new PersistenceManager(Database);
        }
    }
}
