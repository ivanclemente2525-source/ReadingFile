using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadingFile
{
    public partial class FrmStudentRecord : Form
    {
        public FrmStudentRecord()
        {
            InitializeComponent();
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            FrmRegistration registrationForm = new FrmRegistration();
            registrationForm.Show();
            this.Close();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog1.Title = "Browse Student Text Files";
            openFileDialog1.DefaultExt = "txt";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog1.FileName;

                if (!string.IsNullOrEmpty(path))
                {
                    lvShowText.Items.Clear();

                    using (StreamReader streamReader = File.OpenText(path))
                    {
                        string _getText = "";
                        while ((_getText = streamReader.ReadLine()) != null)
                        {
                            Console.WriteLine(_getText);
                            lvShowText.Items.Add(_getText);
                        }
                    }
                }
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (lvShowText.Items.Count == 0)
            {
                MessageBox.Show("No record to upload! Please find and load a file first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Successfully Uploaded!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lvShowText.Items.Clear();
            }
        }
    }
}


  
   
