using System;
using System.Linq;
using System.Windows.Forms;
using University.Model;

namespace University.Forms
{
    public partial class AddMajorForm : Form
    {
        public AddMajorForm()
        {
            InitializeComponent();
            using (var context = new DatabaseContext())
            {
                comboBox1.DataSource = context.Departments.ToList();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                context.Majors.Add(new Major
                {
                    Name = textBox1.Text,
                    DepartmentName = ((Department) comboBox1.SelectedItem).Name
                });
                context.SaveChanges();
            }

            Close();
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Button1_Click(this, new EventArgs());
            }
        }
    }
}
