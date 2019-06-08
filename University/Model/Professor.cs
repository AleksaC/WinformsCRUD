using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Model
{
    public class Professor
    {
        public int    Id              { get; set; }
        public string FirstName       { get; set; }
        public string LastName        { get; set; }
        public string Photo           { get; set; }
        public string Specialty       { get; set; }
        [Column(TypeName = "text")]
        public string Biography       { get; set; }
        public string DepartmentName  { get; set; }

        [ForeignKey("DepartmentName")]
        public virtual Department Department { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
