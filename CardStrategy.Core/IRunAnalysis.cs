using CardStrategy.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardStrategy.Core
{
    public interface IRunAnalysis
    {
        decimal Run(AnalysisConfiguration analysisConfiguration);
    }
}
