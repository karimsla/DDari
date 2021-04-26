using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDari.Models
{
    public class SpamPrediction
    {
        [ColumnName("PredictedLabel")]
        public string isSpam { get; set; }
    }
}