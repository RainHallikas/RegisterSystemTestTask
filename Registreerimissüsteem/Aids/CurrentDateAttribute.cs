using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Registreerimissüsteem.Aids
{
    public class CurrentDateAttribute : ValidationAttribute
    {
        public CurrentDateAttribute() {
            
        }
        public override bool IsValid(object value) {
            var dt = (DateTime)value;
            if (dt >= DateTime.Now)
            {
                return true;
            }
            return false;
        }
    }
}