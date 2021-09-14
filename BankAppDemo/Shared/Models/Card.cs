﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppDemo.Shared.Models
{
    public partial class Card
    {
        public int CardId { get; set; }
        public int DispositionId { get; set; }
        public string Type { get; set; }
        public DateTime Issued { get; set; }
        public string Cctype { get; set; }
        public string Ccnumber { get; set; }
        public string Cvv2 { get; set; }
        public int ExpM { get; set; }
        public int ExpY { get; set; }

        // public virtual Disposition Disposition { get; set; }
    }
}
