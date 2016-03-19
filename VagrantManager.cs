using System;
using System.Collections.Generic;
using System.IO;

namespace VagMan
{
    public class VagrantManager
    {
        private string workingDirectory;
        public List<VagrantFile> vagrantFiles;
        public List<VBox> vboxes;
        public VagrantFinder vagFinder;
        public VBoxFinder vbFinder;
        public VagrantManager(string workingDirectory)
        {
            this.workingDirectory = workingDirectory;
            this.vagFinder = new VagrantFinder(this.workingDirectory);
            string user = Environment.GetEnvironmentVariable("USER");
            string vbXmlFile = "/users/" + user + "/Library/VirtualBox/VirtualBox.xml";
            this.vbFinder = new VBoxFinder(vbXmlFile);
        }
        
        public void FindVagrantAndVboxes()
        {
            this.vagrantFiles = this.vagFinder.FindVagrantFiles();
            
            this.vboxes = vbFinder.FindVBoxes();
        }
    }
}