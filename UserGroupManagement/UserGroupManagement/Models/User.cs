﻿using System.Text.RegularExpressions;

namespace UserGroupManagement.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Group> Groups { get; set; } = new List<Group>();
    }
}
