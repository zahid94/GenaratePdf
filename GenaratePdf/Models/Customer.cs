﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenaratePdf.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
    }
}