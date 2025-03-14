namespace CapaciConnectBackend.DTOS.Users
{
    public class UpdateUserAdminDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Last_names { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Profile_img { get; set; }
        public string? Description { get; set; }
        public int Id_rol_id { get; set; }
    }   
}
