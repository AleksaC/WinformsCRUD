using System.Linq;
using System.Windows.Forms;
using University.Forms;

namespace University
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            UpdateSubjects("");
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderCell.Value = "Ime Predmeta";
            dataGridView1.Columns[2].HeaderCell.Value = "Semestar";
            dataGridView1.Columns[3].HeaderCell.Value = "ECTS";
            dataGridView1.Columns[4].HeaderCell.Value = "Profesor";
            dataGridView1.Columns[2].FillWeight = 50;
            dataGridView1.Columns[3].FillWeight = 50;
        }

        private void Button1_Click(object sender, System.EventArgs e)
        {
            var addDeptForm = new AddDepartmentForm();
            addDeptForm.ShowDialog();
            addDeptForm.Dispose();
            UpdateTreeView();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            UpdateTreeView();
        }

        private void UpdateTreeView()
        {
            treeView1.Nodes.Clear();
            treeView1.BeginUpdate();
            using (var context = new DatabaseContext())
            {
                foreach (var dept in context.Departments)
                {
                    var node = treeView1.Nodes.Add(dept.Name);

                    foreach (var major in dept.Majors)
                    {
                        node.Nodes.Add(major.Name);
                    }
                }
            }
            treeView1.EndUpdate();
        }

        private void UpdateSubjects(string major)
        {
            using (var context = new DatabaseContext())
            {
                dataGridView1.DataSource = context.Subjects
                    .Where(subject => subject.Major.Name == major)
                    .Select(subject => new
                    {
                        subject.Id,
                        subject.Name,
                        subject.Semester,
                        subject.Ects,
                        Professor = subject.Professor.FirstName + " " + subject.Professor.LastName
                    })
                    .ToList();
            }
        }

        private void Button2_Click(object sender, System.EventArgs e)
        {
            var addMajorForm = new AddMajorForm();
            addMajorForm.ShowDialog();
            addMajorForm.Dispose();
            UpdateTreeView();
        }

        private void Button3_Click(object sender, System.EventArgs e)
        {
            var addSubjectForm = new AddSubjectForm();
            addSubjectForm.ShowDialog();
            addSubjectForm.Dispose();
            UpdateSubjects(treeView1.SelectedNode.Text);
        }

        private void Button4_Click(object sender, System.EventArgs e)
        {
            var studentsForm = new StudentsForm();
            studentsForm.ShowDialog();
            studentsForm.Dispose();
        }

        private void Button5_Click(object sender, System.EventArgs e)
        {
            var professorsForm = new ProfessorsForm();
            professorsForm.ShowDialog();
            professorsForm.Dispose();
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateSubjects(e.Node.Nodes.Count == 0 ? e.Node.Text : "");
        }

        private void Button6_Click(object sender, System.EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Izaberite predmete koje želite da obrišete");
                return;
            }

            using (var context = new DatabaseContext())
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    var id = (int) row.Cells[0].Value;
                    context.Subjects.Remove(context.Subjects.First(subject => subject.Id == id));
                }
                context.SaveChanges();
            }
            UpdateSubjects(treeView1.SelectedNode?.Text ?? "");
        }
    }
}
