using System;
using System.Collections.Generic;

using CompareServer.Domain;

namespace CompareServer.WebClient.Models
{
    public class ComparerResultModel
    {
        public ComparerResultModel()
        { 
            Comparisons = new List<ComparisonResult>();
            Errors = new List<string>();
        }

        public ComparerResultModel(ComparerResult result)
        {
            Comparisons = result.Comperisons;
            Errors = result.Errors;
        }

        public List<ComparisonResult> Comparisons { get; set; }
        public List<string> Errors { get; set; }
    }
}