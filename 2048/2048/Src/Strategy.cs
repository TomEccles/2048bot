using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2048
{
    interface Strategy
    {
        Direction getDirection(GameBoard board);
    }
}
