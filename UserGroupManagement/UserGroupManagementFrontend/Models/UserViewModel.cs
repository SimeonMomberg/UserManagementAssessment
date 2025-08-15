using System.Collections.Generic;

namespace UserGroupManagement.MVC.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<int> GroupIds { get; set; } = new List<int>();

        public List<string> Groups { get; set; } = new List<string>();
    }
}