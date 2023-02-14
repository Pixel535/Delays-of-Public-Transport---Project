namespace Transport_System_Process;

//Utworzenie parametrow dla klas - Punkt w Scorecard: Atrybuty własne
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]

public class Parameters : Attribute
{
    public double ChanceOfDelay { get; set; }
    public double ChanceOfSmallDelay { get; set; }
    public double ChanceOfBigDelay { get; set; }
    public double SmallDelay { get; set; }
    public double BigDelay { get; set; }
    public double DelayDuringPeakHours { get; set; }
    public double DelayDuringNormalHours { get; set; }
    
    public int StartHour_H { get; set; }
    public int StartHour_Min { get; set; }
    
    public int StartOfPeakHours_H { get; set; } = 16;
    public int StartOfPeakHours_Min { get; set; } = 00;
    public int EndOfPeakHours_H { get; set; } = 18;
    public int EndOfPeakHours_Min { get; set; } = 00;
    
    //Zdefiniowanie wzoru do czytania pliku - Punkt w Scorecard: Wykorzystanie wyrazen regularnych
    public string pattern { get; set; } = @"[0-9]{1,2}";

    public Parameters(double ChanceOfDelay, double ChanceOfSmallDelay, double ChanceOfBigDelay, double SmallDelay, 
        double BigDelay, double DelayDuringPeakHours, double DelayDuringNormalHours, int StartHour_H, int StartHour_Min)
    {
        this.ChanceOfDelay = ChanceOfDelay;
        this.ChanceOfSmallDelay = ChanceOfSmallDelay;
        this.ChanceOfBigDelay = ChanceOfBigDelay;
        this.SmallDelay = SmallDelay;
        this.BigDelay = BigDelay;
        this.DelayDuringPeakHours = DelayDuringPeakHours;
        this.DelayDuringNormalHours = DelayDuringNormalHours;
        this.StartHour_H = StartHour_H;
        this.StartHour_Min = StartHour_Min;
    }
}