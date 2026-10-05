using System;
using System.Collections.Generic;
using System.Text;

namespace MdToOffice.Models
{
    public class RootConfig
    {
        public List<Config> Config { get; set; }
    }


    public class Config
    {
        public string Name { get; set; }
        public List<RemoveTag> RemoveTag { get; set; }
    }

    public class RemoveTag
    {
        public string Replace { get; set; }
        public string To { get; set; }
    }
}
