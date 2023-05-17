using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanki
{
    internal interface ITankBody
    {
        int Weight { get; }
        int Durability { get; }
    }

    struct LightTankBody : ITankBody
    {
        public int Weight => 50;
        public int Durability => 50;
    }

    struct MediumTankBody : ITankBody
    {
        public int Weight => 75;
        public int Durability => 75;
    }

    struct HeavyTankBody : ITankBody
    {
        public int Weight => 100;
        public int Durability => 100;
    }
}
