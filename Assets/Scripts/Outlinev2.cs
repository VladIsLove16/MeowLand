using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public static class Outlinev2
    {
        public enum OutlineState
        {
            show,
            hide
        }
        public static OutlineState state = OutlineState.show;
    }
}
