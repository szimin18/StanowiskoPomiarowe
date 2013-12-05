namespace Stanowisko.Persistance
{
    public static class PersistenceFactory
    {

        private static readonly IDatabase _Database = new InMemoryDatabase();

        public static IPersistenceManager GetPersistenceManager()
        {
            return new PersistenceManager(_Database);
        }
    }
}
