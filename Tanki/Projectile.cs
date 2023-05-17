using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanki
{
    internal interface IProjectile
    {
        int Damage { get; }
        int Range { get; }
    }
    struct ShortProjectile : IProjectile
    {
        public int Damage => 20;
        public int Range => 3;
    }

    struct MediumProjectile : IProjectile
    {
        public int Damage => 15;
        public int Range => 5;
    }

    struct LongProjectile : IProjectile
    {
        public int Damage => 10;
        public int Range => 7;
    }
}
