using System;
using System.Collections.Generic;

public class Event
{
    public virtual DateTime? GetNextOccurrence()
    {
        return null;
    }
}

public class SingleEvent : Event
{
    private DateTime eventDate;

    public SingleEvent(DateTime date)
    {
        eventDate = date;
    }

    public override DateTime? GetNextOccurrence()
    {
        return eventDate;
    }
}

public class PeriodicEvent : Event
{
    private DateTime startDate;
    private TimeSpan interval;

    public PeriodicEvent(DateTime startDate, TimeSpan interval)
    {
        this.startDate = startDate;
        this.interval = interval;
    }

    public override DateTime? GetNextOccurrence()
    {
        DateTime now = DateTime.Now;
        DateTime nextOccurrence = startDate;

        while (nextOccurrence < now)
        {
            nextOccurrence += interval;
        }

        return nextOccurrence;
    }
}

public class UserCalendar
{
    private List<Event> events = new List<Event>();

    public void AddEvent(Event e)
    {
        events.Add(e);
    }

    public DateTime? GetNextEventDate()
    {
        DateTime? nextDate = null;

        foreach (Event e in events)
        {
            DateTime? occurrence = e.GetNextOccurrence();
            if (occurrence != null && (!nextDate.HasValue || occurrence < nextDate))
            {
                nextDate = occurrence;
            }
        }

        return nextDate;
    }
}

class Program
{
    static void Main(string[] args)
    {
        UserCalendar calendar = new UserCalendar();


        calendar.AddEvent(new SingleEvent(new DateTime(2024, 3, 30)));
        calendar.AddEvent(new PeriodicEvent(new DateTime(2024, 3, 25), TimeSpan.FromDays(7)));
        calendar.AddEvent(new PeriodicEvent(new DateTime(2024, 4, 1), TimeSpan.FromDays(30)));


        DateTime? nextEventDate = calendar.GetNextEventDate();
        if (nextEventDate != null)
        {
            Console.WriteLine("Наступна подiя вiдбудеться {0:d}", nextEventDate);
        }
        else
        {
            Console.WriteLine("Немає запланованих подiй.");
        }
    }
}
