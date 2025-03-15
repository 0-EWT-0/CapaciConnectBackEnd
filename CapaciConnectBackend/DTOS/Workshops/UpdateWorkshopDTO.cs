namespace CapaciConnectBackend.DTOS.Workshops
{
    public class UpdateWorkshopDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int Id_type_id { get; set; }

    }
}
