using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControls.Interface;

interface IDateSelector
{
    public DateTime Start { get; }
    public DateTime End { get; }

    public const int MinYear = 2000;
    public const int MaxYear = 2070;
    public const int MinMonth = 1;
    public const int MaxMonth = 12;
}