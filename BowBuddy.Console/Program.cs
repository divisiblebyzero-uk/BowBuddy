using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BowBuddy.Model;
using BowBuddy.Service;
using Newtonsoft.Json;

namespace BowBuddy.Console
{
    class Program
    {

        static void Main(string[] args)
        {
            System.Console.WriteLine(HandicapCalculationService.Instance.GetHandicapChartHtml(RoundRegistry.Instance.Rounds["Junior National"]));
        }
    }
}
