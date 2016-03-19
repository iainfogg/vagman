namespace VagMan
{
    public class VBoxDisk
    {
        public bool DiskFileExists;
        public string File;
        public VBoxDisk(bool diskFileExists, string File)
        {
            this.DiskFileExists = diskFileExists;
            this.File = File;
        }
    }
}