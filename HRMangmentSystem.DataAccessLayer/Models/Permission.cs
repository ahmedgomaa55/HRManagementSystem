using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMangmentSystem.DataAccessLayer.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? Create { get; set; }
        public bool? Read { get; set; }
        public bool? Update { get; set; }
        public bool? Delete { get; set; }
        [ForeignKey("Group")]
        public int? GroupId { get; set; }
        public Group? Group { get; set; }
    }
}
