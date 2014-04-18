using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2048
{
    interface PositionEvaluator
    {
        double score(GameBoard board);
    }
}
