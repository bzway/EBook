﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bzway.EPubBook.Reader.Entities
{
    public class EpubContent
    {
        public Dictionary<string, EpubTextContentFile> Html { get; set; }
        public Dictionary<string, EpubTextContentFile> Css { get; set; }
        public Dictionary<string, EpubByteContentFile> Images { get; set; }
        public Dictionary<string, EpubByteContentFile> Fonts { get; set; }
        public Dictionary<string, EpubContentFile> AllFiles { get; set; }
    }
}
