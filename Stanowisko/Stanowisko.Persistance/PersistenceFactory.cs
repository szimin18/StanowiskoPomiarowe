namespace Stanowisko.Persistance
{
    public static class PersistenceFactory
    {

        //private static readonly IDatabase Database = new SQLiteDatabase("../../../database.db");

        private static readonly IDatabase Database = new XMLDatabase("../../../database.xml");

        public static IPersistenceManager GetPersistenceManager()
        {
            return new PersistenceManager(Database);
        }
    }
}
