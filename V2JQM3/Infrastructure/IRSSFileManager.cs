using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2JQM3.Infrastructure
{
    internal interface IRSSFileManager
    {
        void LoadSavedRssFiles(List<RSSRecord> rssRecords);
        void EmptySavedItemsFolder();
        string GetCurrentRssFilePath();
        void SetCurrentRssFilePath(string path);
    }
}
