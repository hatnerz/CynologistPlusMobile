using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CynologistPlusMobile.Interfaces
{
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();
    }
}
