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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {

                FrmFileName frmFileName = new FrmFileName();
                frmFileName.ShowDialog();

                if (string.IsNullOrWhiteSpace(FrmFileName.SetFileName) || FrmFileName.SetFileName == ".txt")
                {
                    throw new Exception("File creation cancelled or file name was empty.");
                }

                string getInput = txtInput.Text;
                string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string fullPath = Path.Combine(docPath, FrmFileName.SetFileName);

                if (File.Exists(fullPath))
                {

                    throw new IOException($"The file '{FrmFileName.SetFileName}' already exists in your Documents folder.");
                }

                using (StreamWriter outputFile = new StreamWriter(fullPath))
                {
                    outputFile.WriteLine(getInput);
                    Console.WriteLine(getInput);
                }

                MessageBox.Show("File successfully created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                FrmRegistration frmRegistration = new FrmRegistration();
                frmRegistration.Show();
            }
            catch (IOException ex)
            {

                MessageBox.Show(ex.Message, "File Already Exists Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
    }
}