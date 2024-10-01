using HRMangmentSystem.API.DTOS.PermissionDTO;

namespace HRMangmentSystem.API.DTOS.GroupDTO
{
    public class GroupCommandDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PermissionCommandDTO> Permissions { get; set; }
    }
}
