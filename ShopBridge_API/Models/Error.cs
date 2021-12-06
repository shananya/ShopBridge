using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopBridge_API.Models
{
    public class Error
    {
        public int Id { get; set; }
        [Required]
        public string Errors { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}