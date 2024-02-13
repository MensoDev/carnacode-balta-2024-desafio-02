namespace Imc.Extensions;

public static class DateTimeExtensions
{
    public static string ToPrettierString(this DateTime date)
    {
        var now = DateTime.Now;
        var between = now - date;
        var allMinutes = (int)between.TotalMinutes;

        switch (allMinutes)
        {
            case < 60: return $"{allMinutes}m atrás";
            case < 1440:// 1440 minutos em 24 horas
            {
                var horasPassadas = allMinutes / 60;
                return $"{horasPassadas}h atrás";
            }
            default:
                return date.ToShortDateString();
        }
    }
}