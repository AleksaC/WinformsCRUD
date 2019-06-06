using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using University.Model;

namespace University.Forms
{
    public partial class AddStudentForm : Form
    {
        private string imageSrc;
        private string imageDest;

        public AddStudentForm()
        {
            InitializeComponent();
            using (var context = new DatabaseContext())
            {
                comboBox1.DataSource = context.Departments.ToList();
            }
            ComboBox1_SelectedIndexChanged(this, new EventArgs());
        }

        private void Button2_Click(object sender, System.EventArgs e)
        {
            using (var uploadImageDialog = new OpenFileDialog())
            {
                uploadImageDialog.InitialDirectory =
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                uploadImageDialog.Filter =
                    "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png)|*.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                uploadImageDialog.FilterIndex = 2;
                uploadImageDialog.RestoreDirectory = true;
                if (uploadImageDialog.ShowDialog() == DialogResult.OK)
                {
                    imageSrc = uploadImageDialog.FileName;
                    var imageDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        ConfigurationManager.AppSettings["imagePath"]);
                    if (!File.Exists(imageDir))
                    {
                        (new FileInfo(imageDir)).Directory.Create();
                    }
                    imageDest = Path.Combine(imageDir, Path.GetFileName(imageSrc));
                    Debug.WriteLine(imageDest);
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var student = new Student
                {
                    FirstName = textBox1.Text,
                    LastName = textBox2.Text,
                    MajorId = ((Major) comboBox2.SelectedItem).Id,
                    IndexNumber = context.Students
                                      .Where(st => st.EnrollmentYear == DateTime.Today.Year)
                                      .OrderByDescending(st => st.IndexNumber)
                                      .FirstOrDefault()?.IndexNumber + 1 ?? 1,
                    EnrollmentYear = DateTime.Today.Year
                };
                if (File.Exists(imageSrc))
                {
                    if (File.Exists(imageDest))
                    {
                        imageDest = Path.GetFileNameWithoutExtension(imageDest)
                                    + Guid.NewGuid() + Path.GetExtension(imageDest);
                    }
                    File.Copy(imageSrc, imageDest);
                    student.Photo = imageDest;
                }

                context.Students.Add(student);
                context.SaveChanges();
            }
            Close();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                comboBox2.DataSource = context.Majors
                    .Where(major => major.DepartmentName == comboBox1.Text)
                    .ToList();
            }
        }
    }
}
