using System.ComponentModel.DataAnnotations.Schema;

namespace University.Model
{
    public class Major
    {
        public int    Id             { get; set; }
        public string Name           { get; set; }
        public string DepartmentName { get; set; }

        [ForeignKey("DepartmentName")]
        public virtual Department Department   { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
