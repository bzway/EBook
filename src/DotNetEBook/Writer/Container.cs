﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;


namespace Bzway.EPubBook.Writer
{
    class Container
    {
        private struct RootFile
        {
            public string file;
            public string mediaType;
        }

        private List<RootFile> _rootFiles;

        internal Container()
        {
            _rootFiles = new List<RootFile>();
        }

        internal void AddRootFile(string file, string mediaType)
        {
            RootFile r;
            r.file = file;
            r.mediaType = mediaType;

            _rootFiles.Add(r);
        }

        internal XElement ToElement()
        {
            XNamespace ns = "urn:oasis:names:tc:opendocument:xmlns:container";
            XElement element = new XElement(ns + "container",
                new XAttribute("version", "1.0"));

            XElement filesElement = new XElement(ns + "rootfiles");
            foreach (RootFile r in _rootFiles)
            {
                XElement fileElement = new XElement(ns + "rootfile",
                    new XAttribute("full-path", r.file),
                    new XAttribute("media-type", r.mediaType));
                filesElement.Add(fileElement);
            }
            element.Add(filesElement);

            return element;
        }
    }
}
