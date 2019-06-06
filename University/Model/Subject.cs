using System.ComponentModel.DataAnnotations.Schema;

namespace University.Model
{
    public class Subject
    {
        public int    Id          { get; set; }
        public string Name        { get; set; }
        public int    Ects        { get; set; }
        public int    Semester    { get; set; }
        public int    MajorId     { get; set; }
        public int    ProfessorId { get; set; }

        [ForeignKey("MajorId")]
        public virtual Major Major { get; set; }
        [ForeignKey("ProfessorId")]
        public virtual Professor Professor { get; set; }
    }
}
