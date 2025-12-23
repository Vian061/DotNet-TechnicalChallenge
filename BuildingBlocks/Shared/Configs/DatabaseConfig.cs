namespace BuildingBlocks.Shared.Configs
{
    public class DatabaseConfig
    {
        public required string ConnectionString { get; set; }
        public string? DatabaseName { get; set; }
    }
}
