using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Supplier_SRM_MVC.Entities
{
    [Table("Admins")]
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        public AppUser? User { get; set; }
    }
}
