using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerceAspCore.Models
{
    [Table("RoleAccount")]
    public class RoleAccount
    {
        public int RoleId { get; set; }
        public int AccountId { get; set; }
        public bool Status { get; set; }
        public virtual Role Role{ get; set; }
        public virtual Account Account { get; set; }
    }
}