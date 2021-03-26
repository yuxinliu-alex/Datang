using System;
using System.ComponentModel.DataAnnotations;

namespace Account.Model
{
    public class Account
    {
        [Required]
        public string UserName { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        public int Id { get; set; }
       
        public string Uuid { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        
        public string Token { get; set; }
        public string Ip { get; set; }
        public long Expire { get; set; }
        public string DeviceId { get; set; }
        public string OpenId { get; set; }
        public string NickName { get; set; }
        public string Avatar { get; set; }
        public string Ref { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}