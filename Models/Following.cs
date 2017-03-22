using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterApi.Models
{
    public class Following
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FollowingID { get; set; }
        public User User { get ; set;}
        public User Follow { get; set;}
    }
}