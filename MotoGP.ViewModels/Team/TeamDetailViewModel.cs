﻿using MotoGP.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoGP.ViewModels.Team
{
    public class TeamDetailViewModel
    {

        public int Year { get; set; }

        public string Name { get; set; }

        public EnumGameClass Class { get; set; }

        public string Bike { get; set; }

        public int cc { get; set; }

        public EnumBikeCompany Company { get; set; }
    }
}
