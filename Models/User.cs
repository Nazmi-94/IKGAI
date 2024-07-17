using System.ComponentModel.DataAnnotations;

namespace IKGAi.Models
{
    public class User
    {
        [Key]
        public  int  Id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public virtual List<Comment> comments { get; set; }
        public virtual List<Career> careers { get; set; }


        //Testing Many to Many
        public  virtual List<ManyToMany> ManyToManies { get; set; }



    }
}
