namespace Plugin.Maui.AddToCalendar;

/// <summary>
/// Interface read local calendar list from current device and create a new event
/// </summary>
public interface IAddToCalendar
{
	List<string> GetCalendarList();
	void CreateCalendarEvent(string title, string description, string location, DateTime startDate, DateTime endDate, string calendarName);
}