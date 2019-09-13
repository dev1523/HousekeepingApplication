using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Globalization;
using System.Security.AccessControl;

namespace WindowsFormsApp1
{

    public partial class Form1 : Form
    {
        //fbd is used to browse and extract info of the 
        //selected directory
        FolderBrowserDialog fbd = new FolderBrowserDialog();
        
        //fileFormat string captures the format selected
        String fileFormat;

        //housekeepingDate store the selected date from
        //the datetimepicker menu
        DateTime houseKeepingDate = new DateTime();

        //This button determines that the user can confirm
        //the houskeeping details and start the housekeeping
        //only once it returns YES.
        MessageBoxButtons messageBoxButtons = MessageBoxButtons.YesNo;
        DialogResult messageBoxResult;

        

        public Form1()
        {
            InitializeComponent();
            changeCulture();
            initializeViews();
        }

        public void initializeViews()
        {
   

            dateTimePicker1.ShowCheckBox = true;
            //dateTimePicker1.Checked = false;
            dateTimePicker1.Visible = true;
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            Controls.Add(dateTimePicker1);

            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            fbd.SelectedPath = @"D:\HouseKeepingDemo";
        }
        //function to change the current thread culture
        //with date format as dd/mm/yyyy

        public void changeCulture()
        {
            CultureInfo ci = new CultureInfo(CultureInfo.CurrentCulture.Name);
            //Console.WriteLine(ci);
            ci.DateTimeFormat.ShortDatePattern = "dd'/'MM'/'yyyy";
            // ci.DateTimeFormat.LongTimePattern = "hh':'mm tt";
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
            dateTimePicker1.Value = DateTime.Now.AddMonths(-1);

        }

        //This button is used to choose a folder or a directory on the device.

        public void Button1_Click(object sender, EventArgs e)
        {
            //can set a root folder from where the browsing of the destination 
            //starts
            //fbd.RootFolder = Environment.SpecialFolder.MyDocuments;

            fbd.Description = "Select a Folder for Housekeeping";

            //the following if logic states that if the user has succefully
            //selected a directory or a folder then it should return the 
            //DialogResult as OK

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //MessageBox is used to give the user a prompt about the 
                //selected Folder

                //MessageBox.Show(fbd.SelectedPath);


                //After the prompt , the Label Text is changed to the 
                //path selected by the user

                label1.Text = fbd.SelectedPath;
                button1.Text = fbd.SelectedPath;

                //Making the comboBox(FileFormat selector) enabled
                comboBox1.Enabled = true;

                //Making the DateTime Picker enabled
                dateTimePicker1.Enabled = true;

                //Making the Click To Confirm Button enabled
                button2.Enabled = true;

                //Making the Click To Confirm Button enabled
                button3.Enabled = true;

                //dateTimePicker1.Value = DateTime.Now.AddMonths(-1);

            }

        }

        //button to traverse the folder and extract only those files
        //which has a specified selected format from the dropdown
        //list

        private void Button3_Click(object sender, EventArgs e)
        {
            //dateTimePicker1.Value = DateTime.Now.AddMonths(-1);

            fileFormat = "TEXT - .txt";
            houseKeepingDate = dateTimePicker1.Value;
            messageBoxResult = MessageBox.Show("The Path Selected is-" +
                fbd.SelectedPath + ", File Fromat Selected is-" +
                fileFormat + ", Selected Housekeeping Date is -" + houseKeepingDate.Date
                , "ARE YOU SURE?", messageBoxButtons);
            if(messageBoxResult == DialogResult.Yes )
                folderTraversal_fileDetails();
            
        }



        public void Form1_Load(object sender, EventArgs e)
        {
            InitializeComponent();
            //dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
        }


        //Following is a function to traverse and extract details
        //of the files inside the folder or the directory selected
        //It only gets the files that are selected from the dropdown
        //menu

        public void folderTraversal_fileDetails() 
        {
            //dateTimePicker1.Value = DateTime.Now.AddMonths(-1);


            //String filePaths store the paths of all the different
            //files that are to be deleted by the program
            //right now it is hardcoded as .txt only

            //string[] fileDirectories = Directory.GetDirectories(fbd.SelectedPath, "*");
            //Array.Resize(ref fileDirectories, fileDirectories.Length + 1);
            //fileDirectories[fileDirectories.Length - 1] = fbd.SelectedPath;

            List<String> filePaths = new List<string>();
            filePaths.AddRange(Directory.GetFiles(fbd.SelectedPath, "*.txt", SearchOption.AllDirectories));

            //int K = 0;
            //foreach(string directories in fileDirectories)
            //{
            //    filePaths.AddRange(Directory.GetFiles(directories, "*.txt")); 
            //}
            
                
                
            //qtyPaths is used as an array length for the filePaths
            //string that is used for dynamic allocation of new 
            //Data Types like FileAttributes , creationDateTime ,
            //lastModifiedDateTime

            int qtyPaths = filePaths.Count;

            //fileAttributes store the current info of the file
            //If the file is a system File then it wont be deleted
            //else it will be deleted

            FileAttributes[] fileAttributes = new FileAttributes[qtyPaths];

            //creationDateTime is used to track the creation
            //details of the file that we are about to delete.
            //It is needed to delete the files which have a 
            //creation date older than the selected Date

            DateTime[] creationDateTime = new DateTime[qtyPaths];

            //lastModifiedDateTime is used to keep a track of the
            //as name suggests the last time it was modified at
            //it is an uneseccary step but we do it just so we get
            //an idea about the activity details on the file we are
            //deleting and we can use it for adding security features
            //in the future

            DateTime[] lastModifiedDateTime = new DateTime[qtyPaths];

            

            string logFileLocation = String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),string.Format("text -{0:yyyy-MM-dd_hh-mm-ss-tt}.txt",
            DateTime.Now));
            //StreamWriter fileDeletionLog = File.CreateText(logFileLocation);
            
            const int V = 4;


            string[,] allFileDetails = new string[qtyPaths, V];

            //loop to traverse inside the filePaths string to extract 
            //the details about the files respective to the path

            //string fileDetails;


            //fileDeletionLog.
            File.Delete(logFileLocation);

            
            //DirectoryInfo di = Directory.CreateDirectory(moveFolder);
            ////di.Attributes &= ~FileAttributes.ReadOnly; 
            //DirectorySecurity dSecurity = di.GetAccessControl();
            //dSecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
            //Directory.SetAccessControl(moveFolder, dSecurity);
            //string moveFolder = "D:\\MoveFolder";

            int counter = 0;
            int i = 0;
            using (StreamWriter sw = File.CreateText(logFileLocation))
            {
                sw.WriteLine("Date of Deletion - "+DateTime.Now.ToString());   
                foreach (string paths in filePaths)
                {
                    // dateTimePicker1.Value = DateTime.Now.AddMonths(-1);

                   

                    fileAttributes[i] = File.GetAttributes(paths);
                    creationDateTime[i] = File.GetCreationTime(paths);
                    lastModifiedDateTime[i] = File.GetLastWriteTime(paths);
                    // Make the progressbar visible
                    progressBar1.Visible = true;

                    //Setting the minimum value of the progressbar as 1
                    progressBar1.Minimum = 1;

                    //setting the maximum value of progressbar equal to the number
                    //of filepaths
                    progressBar1.Maximum = filePaths.Count;

                    //setting the initial value of the progressbar as 1
                    progressBar1.Value = 1;

                    //setting the step of each operation as 1
                    progressBar1.Step = 1;
                    

                    for (int j = 0; j < 4; j++)
                    {
                        switch (j)
                        {
                            case 0:
                                allFileDetails[i, j] = paths;
                                break;
                            case 1:
                                allFileDetails[i, j] = creationDateTime[i].ToString();
                                break;
                            case 2:
                                allFileDetails[i, j] = lastModifiedDateTime[i].ToString();
                                break;
                            case 3:
                                allFileDetails[i, j] = fileAttributes[i].ToString();
                                break;
                        }

                    }

                    if (File.Exists(paths) &&
                            DateTime.Compare(lastModifiedDateTime[i].Date, dateTimePicker1.Value.Date) < 0)
                    {

                        FileInfo fi = new FileInfo(paths);

                        for (int j = 0; j < 4; j++)
                        {
                            sw.Write(allFileDetails[i, j] + "    ");
                        }

                        sw.Write((((fi.Length) / 1024) / 1024) + " MB");

                        sw.WriteLine();

                        //di.Attributes &= ~FileAttributes.ReadOnly;

                        string moveFolder = string.Concat("D:/MoveFolder/",
                                   string.Format("folder -{0:yyyy-MM-dd_hh-mm-ss-tt}.txt",
                                   DateTime.Now));

                        //File.Move(paths, moveFolder);
                        File.Delete(paths);
                        //int milliseconds = 2000;
                        //Thread.Sleep(milliseconds);

                        progressBar1.PerformStep();

                        counter++;

                    }

                    i++;
                }
                progressBar1.Value = progressBar1.Maximum;
            }
            MessageBox.Show("Number of Files Deleted =" + counter.ToString());
            //fileDeletion(filePaths , creationDateTime , lastModifiedDateTime , fileAttributes);
        }


        //public void fileDeletion(string[] fileDeletionPaths , DateTime[] fileCreationDate , DateTime[] fileLastAccessDate , FileAttributes[] fileCurrentAttribute)
        //{
            
        //    int i = 0;
        //    
        //    foreach ( string paths in fileDeletionPaths)
        //    {
                
        //       
        //        i++;
        //    }
            
        //}

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
