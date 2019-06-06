using System;
using System.Linq;
using System.Windows.Forms;

namespace University.Forms
{
    public partial class StudentsForm : Form
    {
        public StudentsForm()
        {
            InitializeComponent();
            UpdateList();
            dataGridView1.Columns[0].HeaderCell.Value = "Ime";
            dataGridView1.Columns[1].HeaderCell.Value = "Prezime";
            dataGridView1.Columns[2].HeaderCell.Value = "Fakultet";
            dataGridView1.Columns[3].HeaderCell.Value = "Smjer";
            dataGridView1.Columns[4].HeaderCell.Value = "Br Indeksa";
            dataGridView1.Columns[5].HeaderCell.Value = "Godina Upisa";
            dataGridView1.Columns[6].Visible = false;
        }

        private void Button3_Click(object sender, System.EventArgs e)
        {
            var addStudentForm = new AddStudentForm();
            addStudentForm.ShowDialog();
            addStudentForm.Dispose();
            UpdateList();
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Button1_Click(this, new EventArgs());
            }
        }

        private void Button1_Click(object sender, System.EventArgs e)
        {
            var text = textBox1.Text.Trim().ToLower();
            using (var context = new DatabaseContext())
            {
                dataGridView1.DataSource = context.Students
                    .Where(student => student.FirstName.ToLower().Contains(text)       ||
                                      student.LastName.ToLower().Contains(text)        ||
                                      student.Major.Name.ToLower().Contains(text)      ||
                                      student.MajorId.ToString().Contains(text)        ||
                                      student.EnrollmentYear.ToString().Contains(text) ||
                                      student.Major.DepartmentName.ToLower().Contains(text))
                    .Select(student => new
                    {
                        student.FirstName,
                        student.LastName,
                        student.Major.Department,
                        student.Major.Name,
                        student.IndexNumber,
                        student.EnrollmentYear,
                        student.Photo
                    }).ToList();
            }
        }

        private void UpdateList()
        {
            using (var context = new DatabaseContext())
            {
                dataGridView1.DataSource = context.Students
                    .Select(student => new
                    {
                        student.FirstName,
                        student.LastName,
                        student.Major.Department,
                        student.Major,
                        student.IndexNumber,
                        student.EnrollmentYear,
                        student.Photo
                    }).ToList();
            }
        }

        private void DataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged != DataGridViewElementStates.Selected) return;

            pictureBox1.ImageLocation = e.Row.Cells[6].Value.ToString();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            label1.Text = "Ime: " + e.Row.Cells[0].Value;
            label2.Text = "Prezime: " + e.Row.Cells[1].Value;
            label3.Text = $"Smjer: {e.Row.Cells[2].Value} - {e.Row.Cells[3].Value}";
            label4.Text = $"Broj Indeksa: {e.Row.Cells[4].Value}/{e.Row.Cells[5].Value.ToString().Substring(2)}";
        }

        private void Button2_Click(object sender, EventArgs e)
        {

        }
    }
}
