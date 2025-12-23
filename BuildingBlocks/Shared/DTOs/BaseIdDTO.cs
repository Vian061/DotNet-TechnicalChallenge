namespace BuildingBlocks.Shared.DTOs
{
    public class BaseIdDTO
    {
        public BaseIdDTO()
        {
            Id = 0;
            Alias = Guid.NewGuid().ToString();
        }
        public int Id { get; set; }
        public string Alias { get; set; }
    }
}
