using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitemapChecker.Data.Interfaces
{
    interface IWorkWithUri
    {
        Dictionary<string, string> GetResponceTime();
    }
}
