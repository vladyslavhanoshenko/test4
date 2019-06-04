using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Laboratory
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Value should be not empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Value should be not empty")]
        public string Address { get; set; }

        //1-to-many
        public virtual ICollection<WaterIntake> WaterIntakes { get; set; }
        public Laboratory()
        {
            WaterIntakes = new List<WaterIntake>();
        }
    }
}
