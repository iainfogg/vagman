using System;

namespace VagMan
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string workingDir;
            if (args.Length == 1 && args[0] != "--help")
            {
                workingDir = args[0];
            }
            else {
                Console.WriteLine("You must pass in one parameter, which is the root folder that contains the Vagrantfiles that you want to search for.");
                return;
            }
            
            VagrantManager vagman = new VagrantManager(workingDir);
            ConsoleHandler console = new ConsoleHandler(vagman);
            console.HandleInput();
            
            // return;

            // Get the vagrantfiles that are in the requested folder
            // VagrantFinder finder = new VagrantFinder(workingDir);
            // List<VagrantFile> files = finder.FindVagrantFiles();
            
            // string vbXmlFile = "/users/iainfogg/Library/VirtualBox/VirtualBox.xml";
            // VBoxFinder vbFinder = new VBoxFinder(vbXmlFile);
            // List<VBox> vboxList = vbFinder.FindVBoxes();
            // List<VBox> vboxWithMissingVagrantFolderList = vbFinder.FilterMissingVagrantFolders(vboxList);
            //List<VBox> vboxWithMissingVagrantFolderList = VBoxFinder.

            // foreach (VagrantFile file in files)
            // {
            //     Console.WriteLine(file.FileLocation);
            // }

            // foreach (VBox vbox in vboxWithMissingVagrantFolderList)
            // {
            //     Console.WriteLine(vbox.ToString() + Environment.NewLine);
            // }

            Console.WriteLine("Thanks for using me!");

            // Console.ReadLine();
        }
    }
}