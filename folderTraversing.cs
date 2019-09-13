using System;

public class folderTraversing
{
	public folderTraversing()
	{
        //dateTimePicker1.Value = DateTime.Now.AddMonths(-1);


        //String filePaths store the paths of all the different
        //files that are to be deleted by the program
        //right now it is hardcoded as .txt only

        string[] filePaths = Directory.GetDirectories(fbd.SelectedPath, "*");

        //qtyPaths is used as an array length for the filePaths
        //string that is used for dynamic allocation of new 
        //Data Types like FileAttributes , creationDateTime ,
        //lastModifiedDateTime

        int qtyPaths = filePaths.Length;

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



        string logFileLocation = String.Concat("C:/Users/dmehani/Desktop/", string.Format("text -{0:yyyy-MM-dd_hh-mm-ss-tt}.txt",
        DateTime.Now));
        //StreamWriter fileDeletionLog = File.CreateText(logFileLocation);

        const int V = 4;


        string[,] allFileDetails = new string[qtyPaths, V];

        //loop to traverse inside the filePaths string to extract 
        //the details about the files respective to the path

        //string fileDetails;


        //fileDeletionLog.
        File.Delete(logFileLocation);

        //string moveFolder = string.Concat(Environment.SpecialFolder.Desktop.ToString(),
        //                        string.Format("folder -{0:yyyy-MM-dd_hh-mm-ss-tt}"));

        //DirectoryInfo di = Directory.CreateDirectory(moveFolder);

        int i = 0;
        using (StreamWriter sw = File.CreateText(logFileLocation))
        {
            sw.WriteLine("Date of Deletion - " + DateTime.Now.ToString());
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
                progressBar1.Maximum = filePaths.Length;

                //setting the initial value of the progressbar as 1
                progressBar1.Value = 1;

                //setting the step of each operation as 1
                progressBar1.Step = 1;
                int counter = 0;

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


                    if (File.Exists(paths) &&
                         DateTime.Compare(lastModifiedDateTime[i].Date, dateTimePicker1.Value.Date) < 0)
                    {

                        FileInfo fi = new FileInfo(paths);

                        sw.Write(allFileDetails[i, j] + "    ");

                        sw.Write((((fi.Length) / 1024) / 1024) + " MB");

                        sw.WriteLine();

                        //File.Move(paths, di.FullName);
                        File.Delete(paths);

                        progressBar1.PerformStep();

                        counter++;

                    }

                }
                MessageBox.Show("Number of Files Deleted =" + counter.ToString());
                i++;
            }
            progressBar1.Value = progressBar1.Maximum;
        }

        //fileDeletion(filePaths , creationDateTime , lastModifiedDateTime , fileAttributes);
    }

}

