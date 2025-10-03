using HeavenlyCalendar.Models;

namespace HeavenlyCalendar.Services;

public class HeavenlyCycleService
{
    static string[] stemsLong = { "Jia (Дерево Ян)", "Yi (Дерево Инь)", "Bing (Огонь Ян)", "Ding (Огонь Инь)", "Wu (Земля Ян)", "Ji (Земля Инь)", "Geng (Металл Ян)", "Xin (Металл Инь)", "Ren (Вода Ян)", "Gui (Вода Инь)" };
    static string[] stemsShort = { "K", "L", "F", "E", "C", "D", "B", "A", "G", "H" };
    static string[] branchesLong = { "Zi (Крыса)", "Chou (Бык)", "Yin (Тигр)", "Mao (Кролик)", "Chen (Дракон)", "Si (Змея)", "Wu (Лошадь)", "Wei (Коза)", "Shen (Обезьяна)", "You (Петух)", "Xu (Собака)", "Hai (Свинья)" };
    static string[] branchesShort = { "k", "l", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };
    const int baseYear = 1984;

    public (string desc, string code) GetCycle(int offset)
    {
        int stemIdx = ((offset % 10) + 10) % 10;
        int branchIdx = ((offset % 12) + 12) % 12;
        string longName = $"{stemsLong[stemIdx]} + {branchesLong[branchIdx]}";
        string shortCode = $"{stemsShort[stemIdx]}{branchesShort[branchIdx]}";
        return (longName, shortCode);
    }

    public (string desc, string code) GetYearCycle(int year) => GetCycle(year - baseYear);

    public (string desc, string code) GetMonthCycle(int year, int month) => GetCycle((year - baseYear) * 12 + month);

    public (string desc, string code) GetDayCycle(DateTime date)
    {
        DateTime baseDate = new DateTime(1984, 1, 1);
        int daysOffset = (int)(date - baseDate).TotalDays - 30;
        return GetCycle(daysOffset);
    }

    public List<CalendarDay> GenerateYearCalendar(int year)
    {
        var days = new List<CalendarDay>();

        for (int month = 1; month <= 12; month++)
        {
            int daysInMonth = DateTime.DaysInMonth(year, month);
            for (int day = 1; day <= daysInMonth; day++)
            {
                var date = new DateTime(year, month, day);
                var (desc, code) = GetDayCycle(date);

                // Пример: выделим 1-е число каждого месяца красным
                string color = day == 1 ? "red" : "default";

                days.Add(new CalendarDay
                {
                    Date = date,
                    Code = code,
                    Description = desc,
                    Color = color
                });
            }
        }
        return days; 
    }

    public List<CalendarDay> GenerateMonthCalendar(int year, int month)
        {
            var days = new List<CalendarDay>();
            int daysInMonth = DateTime.DaysInMonth(year, month);

            for (int day = 1; day <= daysInMonth; day++)
            {
                var date = new DateTime(year, month, day);
                var (desc, code) = GetDayCycle(date);

                // Пример: выделим 1-е число каждого месяца красным
                string color = day == 1 ? "red" : "default";

                days.Add(new CalendarDay
                {
                    Date = date,
                    Code = code,
                    Description = desc,
                    Color = color
                });
            }

            return days;
        }
    }
