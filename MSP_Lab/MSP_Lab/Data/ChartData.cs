using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSP_Lab.Data
{
    static class ChartData
    {
        public static readonly ChartEntry[] PieEntries = new[]{
            new ChartEntry(15)
            {
                Color = SKColor.Parse("#FFFF00")
            },
            new ChartEntry(25)
            {
                Color = SKColor.Parse("#AA5500")
            },
            new ChartEntry(45)
            {
                Color = SKColor.Parse("#AAAAAA")
            },
            new ChartEntry(10)
            {
                Color = SKColor.Parse("#FF0000")
            },
            new ChartEntry(5)
            {
                Color = SKColor.Parse("#7F00FF")
            }
        };
        public static readonly IEnumerable<float> Data = Enumerable.Range(0, 120).Select(x => { var y = x / 20f - 3; return y * y * y; });
        public static readonly IEnumerable<ChartEntry> LineEntries = Data.Select(x => new ChartEntry(x) { Color = SKColor.Parse("#000000") });

    }
}
