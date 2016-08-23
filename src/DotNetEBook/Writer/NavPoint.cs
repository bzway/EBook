﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Bzway.EBook.Writer
{
    /// <summary>
    /// Class for TOC entry. Top-level navPoints should be created by Epub.Document.AddNavPoint method
    /// </summary>
    public class NavPoint
    {
        private string _label;
        private string _id;
        private string _content;
        private string _class;
        private int _playOrder;
        List<NavPoint> _navpoints;

        internal NavPoint(string label, string id, string content, int playOrder, string @class)
        {
            _label = label;
            _id = id;
            _content = content;
            _playOrder = playOrder;
            _class = @class;

            _navpoints = new List<NavPoint>();
        }

        internal NavPoint(string label, string id, string content, int playOrder)
            : this(label, id, content, playOrder, String.Empty)
        {

        }

        /// <summary>
        /// Add TOC entry as a direct child of this NavPoint
        /// </summary>
        /// <param name="label">Text of TOC entry</param>
        /// <param name="content">Link to TOC entry</param>
        /// <param name="playOrder">play order counter</param>
        /// <returns>newly created NavPoint </returns>
        public NavPoint AddNavPoint(string label, string content, int playOrder)
        {
            string id = _id + "x" + (_navpoints.Count + 1).ToString();

            NavPoint n = new NavPoint(label, id, content, playOrder);
            _navpoints.Add(n);
            return n;
        }

        internal XElement ToElement()
        {
            XElement e = new XElement(NCX.NcxNS + "navPoint", new XAttribute("id", _id), new XAttribute("playOrder", _playOrder));
            if (!String.IsNullOrEmpty(_class))
                e.Add(new XAttribute("class", _class));
            e.Add(new XElement(NCX.NcxNS + "navLabel", new XElement(NCX.NcxNS + "text", _label)));
            e.Add(new XElement(NCX.NcxNS + "content", new XAttribute("src", _content)));
            foreach (NavPoint n in _navpoints)
            {
                e.Add(n.ToElement());
            }
            return e;
        }

    }
}
