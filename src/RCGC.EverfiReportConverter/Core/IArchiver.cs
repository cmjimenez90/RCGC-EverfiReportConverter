using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCGC.EverfiReportConverter.Core
{
    interface IArchiver<Source,Destination>
    {
        Source Archive(Source item, Destination destination);
    }
}
