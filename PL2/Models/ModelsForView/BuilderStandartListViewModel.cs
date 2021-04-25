using System.Collections.Generic;

namespace PL.Models.ModelsForView
{
    public class BuilderStandartListViewModel
    {
        public IEnumerable<BuildStandart> Standarts { get; set; }
        public BuilderStandartSortViewModel SortViewModel { get; set; }
    }
}
