using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IKGAi.Models
{
    public class Career
    {
        [Key]
        public int Id { get; set; }
        public string careerName { get; set; }
        public string description { get; set; }
        public string requirements { get; set; }

        [ForeignKey("User")]
        public int userId { get; set; }
        public virtual User User { get; set; }
    }
}
