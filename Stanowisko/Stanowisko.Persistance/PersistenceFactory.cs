namespace Stanowisko.Persistance
{
    public static class PersistenceFactory
    {

        //private static readonly IDatabase _Database = new SQLiteDatabase("D:\\Dropbox\\Minions\\Dawid\\TO\\DBtest.db");

        private static readonly IDatabase Database = new XMLDatabase();

        public static IPersistenceManager GetPersistenceManager()
        {
            return new PersistenceManager(Database);
        }
    }
}
