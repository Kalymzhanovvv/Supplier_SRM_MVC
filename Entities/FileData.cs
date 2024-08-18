using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supplier_SRM_MVC.Entities
{
    [Table("FilesData")]
    public class FileData
    {
        [Key]
        public int Id { get; set; }
        public string? FileName { get; set; }
        public string? Path { get; set; }
        public long Size { get; set; }
        public DateTime UploadDate { get; set; }
        public AppUser? Uploader { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
