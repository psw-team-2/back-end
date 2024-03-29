﻿using Explorer.Payments.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class BundleDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public BundleStatus Status { get; set; }

        public List<int> Tours { get; set; }

        public string Image { get; set; }
        
    }

    public enum BundleStatus
    {
        Draft,
        Published,
        Archived
    }


}
