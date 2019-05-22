using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SheetsQuickstart
{
    public class Info
    {
        public Info(string url)
        {
            Uri uri = new Uri(url);
            if (uri.Segments.Length < 3 || uri.Segments.Length > 4)
                throw new ArgumentException("Segments.Length");
            // server
            this.Server = uri.Host;
            // user
            this.User = uri.Segments[1].TrimEnd('/');
            if (this.User.Contains(' '))
                throw new ArgumentException("User");

            Regex regex = new Regex(@"^(\d+)?(\.xml){0,1}$", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

            // collectiokey
            this.CollectionKey = uri.Segments[2].TrimEnd('/');
            if (regex.Match(this.CollectionKey).Success == false)
                throw new ArgumentException("CollectionKey");
            // sectionkey
            if (uri.Segments.Length == 4)
            {
                this.SectionKey = uri.Segments[3].TrimEnd('/');
                if (regex.Match(this.SectionKey).Success == false)
                    throw new ArgumentException("SectionKey");
            }
        }
        public readonly string Server;
        public readonly string User;
        public readonly string CollectionKey;
        public readonly string SectionKey;
    }
}
