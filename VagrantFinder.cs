// using System;
using System.IO;
using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

namespace VagMan
{
    public class VagrantFinder
    {
        private string StartingDirectory;
        public delegate void FoundVagrantFileHandler(VagrantFile vagFileFound);
        public event FoundVagrantFileHandler FoundVagrantFile;

        public VagrantFinder(string StartingDirectory)
        {
            this.StartingDirectory = StartingDirectory;
        }

        public List<VagrantFile> FindVagrantFiles()
        {
            string[] files = Directory.GetFiles(this.StartingDirectory, "Vagrantfile", SearchOption.AllDirectories);

            List<VagrantFile> vagrantFiles = new List<VagrantFile>();
            foreach (string file in files)
            {
                //Console.WriteLine(file);
                VagrantFile vagrantFile = new VagrantFile(file);
                vagrantFiles.Add(vagrantFile);
                OnFoundVBox(vagrantFile);
            }

            return vagrantFiles;
        }
        
        protected void OnFoundVBox(VagrantFile vagFileFound)
        {
            if (vagFileFound != null) {
                FoundVagrantFile(vagFileFound);
            }
        }

    }
}
