namespace UserGroupManagement.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string PermissionName { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}