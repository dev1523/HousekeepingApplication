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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void Button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            //fbd.RootFolder = Environment.SpecialFolder.MyDocuments;
            fbd.Description = "Select a Folder for Housekeeping";
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                MessageBox.Show(fbd.SelectedPath);
                label1.Text = fbd.SelectedPath;
                
            }
            folderTraversal_fileDetails(fbd);
            
        }
        
        public void Form1_Load(object sender, EventArgs e)
        {

        }

        public void folderTraversal_fileDetails(FolderBrowserDialog fbd)
        {
            //string[] filePaths = Directory.GetFiles(fbd.SelectedPath);
            string[] filePaths = Directory.GetFiles(fbd.SelectedPath,"*.txt");
            int qtyPaths = filePaths.Length;
            FileAttributes[] fileAttributes = new FileAttributes[qtyPaths];
            DateTime[] creationDate = new DateTime[qtyPaths];
            DateTime[] lastModifiedDate = new DateTime[qtyPaths];
            int i = 0;
            foreach (string paths in filePaths)
            {
                //Console.WriteLine(paths);
                fileAttributes[i] = File.GetAttributes(paths);
                creationDate[i] = File.GetCreationTime(paths);
                lastModifiedDate[i] = File.GetLastWriteTime(paths);
                i++;
            }
            System.IO.File.WriteAllLines(@"C:\Users\dmehani\Desktop\test.txt", filePaths);
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string fileSelected_formatDetails = (comboBox1.SelectedItem as String);
            MessageBox.Show(fileSelected_formatDetails.IndexOf(" *").ToString());
            MessageBox.Show((fileSelected_formatDetails.IndexOf("* ")-1).ToString());
            label3.Text = fileSelected_formatDetails.Substring(fileSelected_formatDetails.IndexOf(" *"), fileSelected_formatDetails.IndexOf("* ") - 1);
        }
    }

}
