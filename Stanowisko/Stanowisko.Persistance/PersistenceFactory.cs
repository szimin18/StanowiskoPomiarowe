namespace Stanowisko.Persistance
{
    static class PersistenceFactory
    {
        
        public static IPersistenceManager GetPersistenceManager()
        {
            return new PersistenceManager(new InMemoryDatabase());
        }
    }
}
