using System;
using System.Windows.Forms;
using University.Model;

namespace University.Forms
{
    public partial class AddDepartmentForm : Form
    {
        public AddDepartmentForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                context.Departments.Add(new Department{ Name = textBox1.Text });
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
