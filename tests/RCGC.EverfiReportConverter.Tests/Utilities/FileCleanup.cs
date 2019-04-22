using System.IO;
using System.Runtime.CompilerServices;
namespace RCGC.EverfiReportConverter.Tests.Utilities
{
    public sealed class FileCleanup
    {
        static readonly FileCleanup instance = new FileCleanup();
        private FileCleanup()
        {

        }

        public static FileCleanup Instance
        {
            get
            {
                return instance;
            }
        }

        public void RemoveFile(FileInfo fileInfo)
        {
            fileInfo.Delete();
        }

    }
}