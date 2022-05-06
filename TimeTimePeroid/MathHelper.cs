using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTimePeroid
{
    public static class MathHelper
    {
        public static int Mod(int x, int mod)
        {
            int r = x % mod;
            return r < 0 ? r + mod : r;
        }
    }
}
