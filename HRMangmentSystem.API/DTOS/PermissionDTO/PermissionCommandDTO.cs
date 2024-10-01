namespace HRMangmentSystem.API.DTOS.PermissionDTO
{
    public class PermissionCommandDTO
    {
        public string Name { get; set; }
        public bool? Create { get; set; }
        public bool? Read { get; set; }
        public bool? Update { get; set; }
        public bool? Delete { get; set; }
    }
}
