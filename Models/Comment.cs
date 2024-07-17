using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IKGAi.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string commentText { get; set; }
        public DateTime commentDate { get; set; }

        [ForeignKey("User")]
        public int userId { get; set; }

        public virtual User User { get; set; }

    }
}
