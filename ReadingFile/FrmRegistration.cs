using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadingFile
{
    public partial class FrmRegistration : Form
    {
        public FrmRegistration()
        {
            InitializeComponent();
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(txtStudentNo.Text))
                {
                    throw new Exception("Please enter a valid Student Number.");
                }

                if (string.IsNullOrWhiteSpace(txtLastName.Text))
                {
                    throw new Exception("Last Name cannot be empty.");
                }

                if (string.IsNullOrWhiteSpace(txtFirstName.Text))
                {
                    throw new Exception("First Name cannot be empty.");
                }

                if (string.IsNullOrWhiteSpace(txtMiddleInitial.Text))
                {
                    throw new Exception("Middle Initial (M.I.) cannot be empty.");
                }

                if (string.IsNullOrWhiteSpace(cbPrograms.Text))
                {
                    throw new Exception("Please select a Program.");
                }

                if (string.IsNullOrWhiteSpace(cbGender.Text))
                {
                    throw new Exception("Please select a Gender.");
                }

                if (!int.TryParse(txtAge.Text, out int age) || age <= 0)
                {
                    throw new Exception("Please enter a valid Age (numbers only).");
                }

                if (string.IsNullOrWhiteSpace(txtContactNo.Text) || !Regex.IsMatch(txtContactNo.Text, @"^[0-9]+$"))
                {
                    throw new Exception("Please enter a valid Contact Number (numbers only).");
                }

                string fileName = txtStudentNo.Text + ".txt";
                string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string fullPath = Path.Combine(docPath, fileName);

                if (File.Exists(fullPath))
                {
                    throw new IOException($"A record for Student No. '{txtStudentNo.Text}' already exists.");
                }

                string[] studentDetails = new string[]
                {
                   "Student No.: " + txtStudentNo.Text,
                   "Full Name: " + txtLastName.Text + ", " + txtFirstName.Text + ", " + txtMiddleInitial.Text + ".",
                   "Program: " + cbPrograms.Text,
                   "Gender: " + cbGender.Text,
                   "Age: " + txtAge.Text,
                   "Birthday: " + datePickerBirtday.Value.ToString("yyyy-MM-dd"),
                   "Contact No.: " + txtContactNo.Text
                };

                using (StreamWriter writer = new StreamWriter(fullPath))
                {
                    foreach (string line in studentDetails)
                    {
                        writer.WriteLine(line);
                    }
                }
                MessageBox.Show("Registration details saved to " + fileName, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (IOException ex)
            {

                MessageBox.Show(ex.Message, "Duplicate Entry Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Input Validation Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            FrmStudentRecord recordForm = new FrmStudentRecord();
            recordForm.Show();
            this.Close();
        }
    }
}
