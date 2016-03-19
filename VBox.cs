using System;
using System.Collections.Generic;

namespace VagMan
{
    public class VBox
    {
        public string name;
        public bool vboxMachineFileExists;
        public string vboxFile;
        public bool vagrantFolderExists;
        public string vagrantFolder;
        public List<VBoxDisk> DiskList = new List<VBoxDisk>();
        public VBox(string name, bool vboxMachineFileExists, string vboxFile, bool vagrantFolderExists, string vagrantFolder, List<VBoxDisk> DiskList)
        {
            this.name = name;
            this.vboxMachineFileExists = vboxMachineFileExists;
            this.vboxFile = vboxFile;
            this.vagrantFolderExists = vagrantFolderExists;
            this.vagrantFolder = vagrantFolder;
            this.DiskList = DiskList;
        }
        
        public bool IsVagrantBox()
        {
            return (this.vagrantFolder == null ? false : true);
        }
        
        public override string ToString()
        {
            string output = "";
            output += "name: " + this.name + Environment.NewLine;
            output += " vbox file: " + (this.vboxMachineFileExists ? "" : "(deleted)") + this.vboxFile + Environment.NewLine;
            
            output += " is vagrant: " + this.IsVagrantBox().ToString(); 
            if (this.IsVagrantBox()) {
                output += " " + (this.vagrantFolderExists ? "" : "(deleted)") + this.vagrantFolder;
            }
            output += Environment.NewLine;
            output += " disks:" + Environment.NewLine;
            foreach (VBoxDisk disk in this.DiskList)
            {
                output += "  " + (disk.DiskFileExists ? "" : "(deleted) ") + disk.File;
            }
            
            return output;
        }
    }
}