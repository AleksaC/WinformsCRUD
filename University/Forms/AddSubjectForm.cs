using System;
using System.Linq;
using System.Windows.Forms;
using University.Model;

namespace University.Forms
{
    public partial class AddSubjectForm : Form
    {
        public AddSubjectForm()
        {
            InitializeComponent();
            using (var context = new DatabaseContext())
            {
                comboBox1.DataSource = context.Departments.ToList();
            }
            ComboBox1_SelectedIndexChanged(this, new EventArgs());
        }

        private void Button1_Click(object sender, System.EventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var subject = new Subject
                {
                    Name= textBox1.Text,
                    MajorId = ((Major)comboBox2.SelectedItem).Id,
                    ProfessorId = ((Professor) comboBox3.SelectedItem).Id,
                    Semester = int.Parse(numericUpDown1.Text),
                    Ects = int.Parse(numericUpDown2.Text)
                };
                
                context.Subjects.Add(subject);
                context.SaveChanges();
            }
            Close();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                comboBox2.DataSource = context.Majors
                    .Where(major => major.DepartmentName == comboBox1.Text)
                    .ToList();
                comboBox3.DataSource = context.Professors
                    .Where(professor => professor.Department.Name == comboBox1.Text)
                    .ToList();
            }
        }
    }
}
