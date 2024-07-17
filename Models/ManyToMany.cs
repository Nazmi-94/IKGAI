namespace IKGAi.Models
{
    public class ManyToMany
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }  

        public int UserProfileId {  get; set; } 
        public virtual UserProfile UserProfile { get; set; }

    }
}
