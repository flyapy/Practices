using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practices.Commons.Validators
{
    public class StringValidator
    {

        public bool LengthBetween(string s, int min, int max)
        {
            return s != null && s.Length >= min && s.Length <= max;
        }

        public bool LengthGreaterThan(string s, int len)
        {
            return s != null && s.Length > len;
        }

        public bool LengthLessThan(string s, int len)
        {
            return s != null && s.Length < len;
        }
    }
}
