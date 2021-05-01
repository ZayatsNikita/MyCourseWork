using System.Collections.Generic;

namespace PL.Models.ModelsForView
{
    public class BuilderStandartListViewModel
    {
        public IEnumerable<BuildStandart> Standarts { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public BuilderStabdartFilterViewModel FilterViewModel { get; set; }
        public BuildStandartSortViewModel SortViewModel { get; set; }
    }
}
