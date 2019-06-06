using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace University.Model
{
    public class Department
    {
        [Key]
        public string Name { get; set; }
        public virtual ICollection<Major> Majors { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
