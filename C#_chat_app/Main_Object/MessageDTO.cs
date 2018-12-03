using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Main_Object
{
    public class MessageDTO
    {
        public int Id { get; set; }
        [Required]
        public string SenderName { get; set; }
        [Required]
        public string ReceiverName { get; set; }
        [StringLength(250)]
        [Required]
        public string Message { get; set; }
        public DateTime DateTime { get; set; }


        public virtual User Sender { get; set; }

        public virtual User Receiver { get; set; }

   
    }
}
