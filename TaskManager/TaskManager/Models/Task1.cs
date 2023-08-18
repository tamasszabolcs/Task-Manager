using Microsoft.VisualBasic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace TaskManager.Models
{
    public class Task1
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey(nameof(Status))]
        [Required]
        public int StatusId { get; set; }
        
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey(nameof(Category))]
        [Required]
        public int CategoryId { get; set; }
        [Range(0, 100)]
        public int? Progress { get; set; }

        [Required(ErrorMessage = "Please enter a valid due date.")]
        [TypeConverter(typeof(DateTimeConverter))]
        public DateTime DueDate { get; set; }
        public int UserId { get; set; }


        /*public Task1(int id, string status, string name, string description, TaskCategory category, DateOnly dueDate)
        {
            Id = id;
            Status = status;
            Name = name;
            Description = description;
            Category = category;
            DueDate = dueDate;
        } */



    }

}

public enum TaskCategory { Work, School, Personal }