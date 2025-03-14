namespace CapaciConnectBackend.DTOS.Responses
{
    public class UserAuthResponse
    {
        public int Id_user {  get; set; }
        public string Name { get; set; } = string.Empty;
        public string Last_names { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;   
        public string Email { get; set; } = string.Empty;
        public int Id_rol_id {  get; set; } 
    }
}
