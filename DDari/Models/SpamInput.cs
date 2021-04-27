using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDari.Models
{
    public class SpamInput
    {
        [LoadColumn(0)]
        public string Label { get; set; }
        [LoadColumn(1)]
        public string Message { get; set; }
    }
}