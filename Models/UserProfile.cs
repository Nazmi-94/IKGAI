using System.ComponentModel.DataAnnotations;

namespace IKGAi.Models
{
    public class UserProfile
    {
        [Key]
        public int profileId { get; set; }
        public int userId { get; set; }
        public string skills  { get; set; }
        public string experience { get; set; }
        public string interests { get; set; }
        public string education { get; set; }

    }
}
