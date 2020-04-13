using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaperLib
{
    public class Cell 
    {
        public Cell(bool hasBomb = false, bool isVisible = false)
        {
            HasBomb = hasBomb;
            IsVisible = isVisible;
        }

        public bool HasBomb{ get; set; }
        public bool IsVisible { get; set; }
    }
}
