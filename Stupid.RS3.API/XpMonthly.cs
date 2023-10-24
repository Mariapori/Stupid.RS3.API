
using System.Collections.Generic;
namespace Stupid.RS3.API
{
public class XpMonthly
{
public class MonthDatum
    {
        public int xpGain { get; set; }
        public long timestamp { get; set; }
        public int rank { get; set; }
    }

    public class MonthlyXpGain
    {
        public int skillId { get; set; }
        public int totalXp { get; set; }
        public int averageXpGain { get; set; }
        public int totalGain { get; set; }
        public List<MonthDatum> monthData { get; set; }
    }

    public class Root
    {
        public List<MonthlyXpGain> monthlyXpGain { get; set; }
        public bool loggedIn { get; set; }
    }
}
}
