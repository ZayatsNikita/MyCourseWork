using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services.Abstract
{
    public interface IChartManager 
    {
        public List<(string, int)> GetDataForServiceCharts(DateTime from, DateTime to);
        public List<(string, int)> GetDataForComponentCharts(DateTime from, DateTime to);
    }
}
