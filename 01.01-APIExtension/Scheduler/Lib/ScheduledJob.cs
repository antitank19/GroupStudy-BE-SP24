namespace Utilities.ServiceExtensions.Scheduler.Lib;

public class ScheduledJob
{
    public ScheduledJob(Type type, string scheduleExpression)
    {
        Type = type;
        ScheduleExpression = scheduleExpression;
    }

    public Type Type { get; }
    public string ScheduleExpression { get; }
}