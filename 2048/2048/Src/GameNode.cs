using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2048
{
    interface GameNode
    {
        List<GameNode> getChildren();

        void calculateChildren();

        double score();
    }
}
