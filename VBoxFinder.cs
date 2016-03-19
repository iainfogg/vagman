//using System;
//using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml;

namespace VagMan
{
    public class VBoxFinder
    {
        private string VBoxXmlFile;
        public delegate void FoundVBoxHandler(VBox vboxFound);
        public event FoundVBoxHandler FoundVBox;
        public VBoxFinder(string VBoxXmlFile)
        {
            this.VBoxXmlFile = VBoxXmlFile;
        }
        
        public List<VBox> FindVBoxes()
        {
            XNamespace ns = "http://www.innotek.de/VirtualBox-settings";
            XDocument vbDoc = XDocument.Load(this.VBoxXmlFile);
            var machineEntries = from element in vbDoc.Descendants(ns + "MachineEntry") select element;
            
            List<VBox> vboxList = new List<VBox>();

            foreach (XElement machineEntry in machineEntries)
            {
                string name = null;
                string vagrantFolder = null;
                bool vagrantFolderExists = false;
                List<VBoxDisk> diskList = new List<VBoxDisk>();
                
                string vboxMachineFile = machineEntry.Attribute("src").Value;
                bool vboxMachineFileExists = File.Exists(vboxMachineFile);
                
                if (vboxMachineFile != null) {
                    // Open the machine file
                    string pathForMachine = Path.GetDirectoryName(vboxMachineFile);
                    XDocument vboxMachineXml = XDocument.Load(vboxMachineFile);
                   
                    // Get the name of the machine                    
                    var machineElements = from element in vboxMachineXml.Descendants(ns + "Machine") select element;
                    name = machineElements.First().Attribute("name").Value;
                    
                    // Look in all shared folders for a folder called 'vagrant' (shows it's a vagrant VM rather than another one)
                    var sharedFolders = from element in vboxMachineXml.Descendants(ns + "SharedFolder") select element;
                    foreach (XElement sharedFolder in sharedFolders)
                    {
                        if (sharedFolder.Attribute("name").Value == "vagrant") {
                            vagrantFolder = sharedFolder.Attribute("hostPath").Value;
                            vagrantFolderExists = Directory.Exists(vagrantFolder);
                            break;
                        }
                    }
                    
                    // Look for hard drives and check they all exist
                    var disks = from element in vboxMachineXml.Descendants(ns + "HardDisk") select element;
                    foreach (XElement disk in disks)
                    {
                        string diskFile = pathForMachine + "/" + disk.Attribute("location").Value;
                        bool diskFileExists = File.Exists(diskFile);
                        diskList.Add(new VBoxDisk(diskFileExists, diskFile));
                    }
                }

                VBox vbox = new VBox(name, vboxMachineFileExists, vboxMachineFile, vagrantFolderExists, vagrantFolder, diskList);
                vboxList.Add(vbox);
                OnFoundVBox(vbox);
            }
            
            return vboxList;
        }
        
        public List<VBox> FilterNonVagrantBoxes(List<VBox> InputList)
        {
            List<VBox> missingVagrantList = InputList.Where(s => s.IsVagrantBox() == false).ToList();
            
            return missingVagrantList;
        }
        
        public List<VBox> FilterBoxesWithMissingVagrantFolders(List<VBox> InputList)
        {
            List<VBox> missingVagrantList = InputList.Where(s => (s.IsVagrantBox() == true && s.vagrantFolderExists == false)).ToList();
            
            return missingVagrantList;
        }
        
        protected void OnFoundVBox(VBox vboxFound)
        {
            if (vboxFound != null) {
                FoundVBox(vboxFound);
            }
        }
    }
}