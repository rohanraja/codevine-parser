using System;
using System.Collections.Generic;
using System.Text;

namespace TestCSharpProject
{
    class WithSetter
    {
        public int testSetter
        {
            set
            {
                int b = 3;
                b += 3;
                b -= 4;
            }
        }
    }
}
