using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using University.Model;

namespace University.Forms
{
    public partial class AddProfessorForm : Form
    {
        private string imageSrc;
        private string imageDest;

        public AddProfessorForm()
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
                var professor = new Professor
                {
                    FirstName = textBox1.Text,
                    LastName = textBox2.Text,
                    DepartmentName = comboBox1.Text,
                    Biography = richTextBox1.Text
                };
                if (File.Exists(imageSrc))
                {
                    if (File.Exists(imageDest))
                    {
                        imageDest = Path.GetFileNameWithoutExtension(imageDest) 
                                    + Guid.NewGuid() + Path.GetExtension(imageDest);
                    }
                    File.Copy(imageSrc, imageDest);
                    professor.Photo = imageDest;
                }

                context.Professors.Add(professor);
                context.SaveChanges();
            }
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
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
    }
}
