namespace Stanowisko.Persistance
{
    public static class PersistenceFactory
    {

        private static readonly IDatabase Database = new SQLiteDatabase("D:\\Dropbox\\Minions\\Dawid\\TO\\ex1.db");

        //private static readonly IDatabase Database = new XMLDatabase();

        //private static readonly IDatabase Database = new InMemoryDatabase();

        public static IPersistenceManager GetPersistenceManager()
        {
            return new PersistenceManager(Database);
        }
    }
}
