using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureFactoryBusinessLogics.ViewModels
{
    public class DiagramViewModel
    {
        public string ColumnName { get; set; }

        public string ValueName { get; set; }

        public string Title { get; set; }

        public List<Tuple<string, int>> Data { get; set; }
    }
}
