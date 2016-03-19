using System;
using System.Collections.Generic;

namespace VagMan
{
    public class ConsoleHandler
    {
        private VagrantManager vagman;
        private static int vboxesFound = 0;
        private static int vagFilesFound = 0;
        public ConsoleHandler(VagrantManager vagman)
        {
            this.vagman = vagman;
            
            // Set up the event handling
            this.vagman.vbFinder.FoundVBox += new VBoxFinder.FoundVBoxHandler(DisplayVBoxFound);
            this.vagman.vagFinder.FoundVagrantFile += new VagrantFinder.FoundVagrantFileHandler(DisplayVagrantFileFound);
            
            // Do the initial search for vagrant files and boxes;
            this.vagman.FindVagrantAndVboxes();
        }
        
        static void DisplayVBoxFound(VBox vboxFound)
        {
            vboxesFound += 1;
            Console.WriteLine("Boxes found: " + vboxesFound + " (" + vboxFound.name + ")");
        }

        static void DisplayVagrantFileFound(VagrantFile vagFileFound)
        {
            vagFilesFound += 1;
            Console.WriteLine("Vagrant files found: " + vagFilesFound + " (" + vagFileFound.GetFileFolder() + ")");
        }

        public void HandleInput()
        {
            Console.WriteLine(Environment.NewLine + "Welcome to Vagrant Manager! All Vagrantfiles and virtual machines that have been found are listed above. Please enter the number of the option you'd like to run and press ENTER.");
            
            bool exitApp = false;
            while (!exitApp)
            {
                this.ShowOptions();
                string input = Console.ReadLine();
                Console.WriteLine("");
                switch (input)
                {
                    case "1":
                        this.ShowVBoxesWhichArentVagrant();
                        break;
                    case "2":
                        this.ShowVBoxesWithMissingVagrantFolders();
                        break;
                    case "3":
                        this.ShowVagFilesWithVMs();
                        break;
                    case "4":
                        this.ShowVagFilesWithoutVMs();
                        break;
                    case "9":
                        return;
                    default:
                        this.ShowOptions();
                        break;
                }
            }
        }
        
        private void ShowOptions()
        {
            string output = "";
            string nl = Environment.NewLine;
            output += nl + "Enter which option you would like to run:" + nl;
            output += " 1. Show VirtualBox machines which are not Vagrant machines" + nl;
            output += " 2. Show VirtualBox machines with missing vagrant folders" + nl;
            output += " 3. Show vagrant files with VMs" + nl;
            output += " 4. Show vagrant files without VMs" + nl;
            output += " 9. Exit" + nl;
            Console.Write(output);
        }
        
        private void ShowVBoxesWhichArentVagrant()
        {
            List<VBox> vboxes = this.vagman.vbFinder.FilterNonVagrantBoxes(vagman.vboxes);
            if (vboxes.Count == 0) {
                Console.WriteLine(Environment.NewLine + "All virtual machines are Vagrant boxes" + Environment.NewLine);
            } else {
                foreach (VBox vbox in vboxes)
                {
                    Console.WriteLine(vbox.ToString() + Environment.NewLine);
                }
            }
        }

        private void ShowVBoxesWithMissingVagrantFolders()
        {
            List<VBox> vboxes = this.vagman.vbFinder.FilterBoxesWithMissingVagrantFolders(vagman.vboxes);
            if (vboxes.Count == 0) {
                Console.WriteLine(Environment.NewLine + "All virtual machines that were set up by Vagrant have their required Vagrant folder" + Environment.NewLine);
            } else {
                foreach (VBox vbox in vboxes)
                {
                    Console.WriteLine(vbox.ToString() + Environment.NewLine);
                }
            }
        }
        
        private void ShowVagFilesWithVMs()
        {
            List<VagrantFile> allVagFiles = this.vagman.vagrantFiles;
            List<VBox> allVBoxes = this.vagman.vboxes;
            foreach (VagrantFile vagFile in allVagFiles)
            {
                foreach (VBox vbox in allVBoxes)
                {
                    if (vagFile.GetFileFolder() == vbox.vagrantFolder) {
                        Console.WriteLine(vagFile.GetFileFolder() + " (machine name: " + vbox.name + ")");
                        break;
                    } 
                }
            }
        }

        private void ShowVagFilesWithoutVMs()
        {
            List<VagrantFile> allVagFiles = this.vagman.vagrantFiles;
            List<VBox> allVBoxes = this.vagman.vboxes;
            foreach (VagrantFile vagFile in allVagFiles)
            {
                bool matchFound = false;
                foreach (VBox vbox in allVBoxes)
                {
                    if (vagFile.GetFileFolder() == vbox.vagrantFolder) {
                        matchFound = true;
                        break;
                    } 
                }
                if (!matchFound) {
                    Console.WriteLine(vagFile.GetFileFolder());
                }
            }
        }
    }
}