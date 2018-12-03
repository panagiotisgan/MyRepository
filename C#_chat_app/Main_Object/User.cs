using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main_Object
{
    public class User
    {
        public int Id { get; set; }
        [EnumDataType(typeof(AccessId))]
        public AccessId AccessId { get; set; }
        [StringLength(12)]
        [MaxLength(12)]
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Salt { get; set; }

        [InverseProperty("Sender")]
        public virtual ICollection<MessageDTO> MessagesSender { get; set; }

        [InverseProperty("Receiver")]
        public virtual ICollection<MessageDTO> MessagesReceiver { get; set; }

    }
}
