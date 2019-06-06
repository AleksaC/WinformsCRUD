using System;
using System.Linq;
using System.Windows.Forms;

namespace University.Forms
{
    public partial class ProfessorsForm : Form
    {
        public ProfessorsForm()
        {
            InitializeComponent();
            UpdateList();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderCell.Value = "Ime";
            dataGridView1.Columns[2].HeaderCell.Value = "Prezime";
            dataGridView1.Columns[3].HeaderCell.Value = "Fakultet";
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            var addProfessorForm = new AddProfessorForm();
            addProfessorForm.ShowDialog();
            addProfessorForm.Dispose();
            UpdateList();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var text = textBox1.Text;
            using (var context = new DatabaseContext())
            {
                dataGridView1.DataSource = context.Professors
                    .Where(professor => professor.FirstName.Contains(text) ||
                                        professor.LastName.Contains(text)  ||
                                        professor.DepartmentName.Contains(text))
                    .Select(professor => new
                    {
                        professor.Id,
                        professor.FirstName,
                        professor.LastName,
                        professor.DepartmentName,
                        professor.Photo,
                        professor.Biography
                    }).ToList();
            }
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Button1_Click(this, new EventArgs());
            }
        }

        private void DataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged != DataGridViewElementStates.Selected) return;
            
            pictureBox1.ImageLocation = e.Row.Cells[4].Value.ToString();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            label1.Text = "Ime: " + e.Row.Cells[1].Value;
            label2.Text = "Prezime: " + e.Row.Cells[2].Value;
            label3.Text = "Fakultet: " + e.Row.Cells[3].Value;
            label4.Text = "Biografija: \n\n" + e.Row.Cells[5].Value;
        }

        private void UpdateList()
        {
            using (var context = new DatabaseContext())
            {
                dataGridView1.DataSource = context.Professors
                    .Select(professor => new
                    {
                        professor.Id,
                        professor.FirstName,
                        professor.LastName,
                        professor.DepartmentName,
                        professor.Photo,
                        professor.Biography
                    }).ToList();
            }
        }
    }
}
