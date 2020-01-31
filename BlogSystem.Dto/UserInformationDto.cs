﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Dto
{
    public class UserInformationDto
    {
        public Guid Id { get; set; }
        public string Eamil { get; set; }
        public string ImagePath { get; set; }
        public string SiteName { get; set; }
        public int FunsCount { get; set; }
        public int FocusCount { get; set; }
    }
}
