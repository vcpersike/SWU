﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraping.Domain.Models
{
    public class SwuPage
    {
        public string Content { get; set; }
        public string Url { get; set; }

        public SwuPage(string content, string url)
        {
            Content = content;
            Url = url;
        }
    }
}
