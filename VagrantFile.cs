// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

using System.IO;

namespace VagMan
{
    public class VagrantFile
    {
        public string FileLocation;

        public VagrantFile(string FileLocation)
        {
            this.FileLocation = FileLocation;
        }
        
        public string GetFileFolder()
        {
            return Path.GetDirectoryName(this.FileLocation);
        }
    }
}
