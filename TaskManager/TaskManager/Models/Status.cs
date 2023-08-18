using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class Status
    {
        [Key]
        public int StatusId { get; set; }
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }
        public int? UserId { get; set; }
    }
}
