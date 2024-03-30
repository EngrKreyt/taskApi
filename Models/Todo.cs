using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace taskApi.Models
{
    public class Todo
    {
        public int Id { get; }
        public required string Title { get; set; }
        public  string? Description { get; set; }
        public int Status { get; set; }

        // Foreign key property
        public int UserId { get; set; }
    }
}
