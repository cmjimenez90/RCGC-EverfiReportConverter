using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RCGC.EverfiReportConverter.Core.FileTagger
{
    internal interface IFileTagger<TagType>
    {
        FileInfo Tag(FileInfo fileToTag,TagType tag);
    }
}
