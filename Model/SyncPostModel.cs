using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SyncPostWebAPI.Model
{
    #region Registration
    public class UserAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDNUM { get; set; }

        [Required]
        [MaxLength(255)]
        public string username { get; set; }

        [Required]
        [MaxLength(255)]
        public string password { get; set; }
    }

    public class Templates
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int template_id { get; set; }

        [ForeignKey("UserAccount")]
        public int user_id { get; set; }

        [Required]
        [MaxLength(255)]
        public string template_title { get; set; }

        [Required]
        public string template_content { get; set; }

        public DateTime last_modification_date { get; set; }

    }
    #endregion

    #region login
    public class LoginCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    #endregion
}
