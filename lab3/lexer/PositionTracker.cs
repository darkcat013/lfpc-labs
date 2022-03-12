using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lexer
{
    public class PositionTracker
    {
        public int Line { get; private set; } = 1;
        public int Column { get; set; } = -1;
        public int Position { get; set; }
        public void AddLine()
        {
            Line++;
            Column = -1;
        }
    }
}
