namespace SistemaDeGestao.Settings
{
    public class MongoDbSettings
    {
        public MongoDbSettings()
        {
                
        }
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string CollectionName { get; set; } = null!;
    }
}
