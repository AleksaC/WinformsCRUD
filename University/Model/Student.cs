using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Model
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName  { get; set; }
        public string Photo     { get; set; }

        [Key, Column(Order = 0)]
        public int IndexNumber { get; set; }
        [Key, Column(Order = 1)]
        public int EnrollmentYear { get; set; }
        [Key, Column(Order = 2)]
        public int MajorId { get; set; }

        [ForeignKey("MajorId")]
        public virtual Major Major { get; set; }
    }
}