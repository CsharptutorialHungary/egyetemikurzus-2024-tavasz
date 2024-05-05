using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2JQM3.Infrastructure
{
    internal record RSSRecord
    {
        public RSSRecord(string filename, string title, DateTime date, string fullpath)
        {
            Filename = filename;
            Title = title;
            Date = date;
            FullPath = fullpath;
        }

        public string Filename { get; }
        public string Title { get; }
        public DateTime Date { get; }
        public string FullPath { get; }
    }
}
