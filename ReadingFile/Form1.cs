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
        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
               
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                openFileDialog.Title = "Browse Text Files"; 
                openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"; 

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fullPath = openFileDialog.FileName;

                    if (!File.Exists(fullPath))
                    {
                        throw new FileNotFoundException("The selected file does not exist.");
                    }

                    using (StreamReader reader = new StreamReader(fullPath)) 
                    {
                        txtInput.Clear(); 

                        string line;
                        while ((line = reader.ReadLine()) != null) 
                        {
                            txtInput.AppendText(line + Environment.NewLine);
                        }
                    }

                    MessageBox.Show("File successfully opened!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "File Not Found Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}