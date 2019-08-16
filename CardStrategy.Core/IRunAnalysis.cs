using CardStrategy.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CardStrategy.Core
{
    public interface IRunAnalysis
    {
        Task<decimal> Run(AnalysisConfiguration analysisConfiguration, IUpdateProgress updateProgress);
    }
}
