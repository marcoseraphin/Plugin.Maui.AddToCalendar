using EventKit;
using Microsoft.Maui;

namespace Plugin.Maui.AddToCalendar;

partial class AddToCalendarServiceImplementation : IAddToCalendar
{
	/// <summary>
	/// Create new event to local calendar with calendarName and description, location for startDate till endDate
	/// </summary>
	/// <param name="title"></param>
	/// <param name="description"></param>
	/// <param name="location"></param>
	/// <param name="startDate"></param>
	/// <param name="endDate"></param>
	/// <param name="calendarName"></param>
	public void CreateCalendarEvent(string title, string description, string location, DateTime startDate, DateTime endDate, string calendarName)
	{
		var eventStore = new EKEventStore();
		var calendars = eventStore.GetCalendars(EKEntityType.Event);
		var targetCalendar = calendars.FirstOrDefault(c => c.Title == calendarName);

		eventStore.RequestAccess(EKEntityType.Event, (bool granted, NSError e) =>
		{
			if (targetCalendar != null)
			{
				EKEvent newEvent = EKEvent.FromStore(eventStore);
				newEvent.Title = title;
				newEvent.Notes = description;
				newEvent.Location = location;
				newEvent.StartDate = ToNsDate(startDate);
				newEvent.EndDate = ToNsDate(endDate);
				newEvent.Calendar = targetCalendar;

				NSError error;
				eventStore.SaveEvent(newEvent, EKSpan.ThisEvent, out error);

			}
		});
	}

	public NSDate ToNsDate(DateTime dateTime)
	{
		if (dateTime.Kind == DateTimeKind.Unspecified)
		{
			dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
		}
		return (NSDate)dateTime;
	}

	/// <summary>
	/// Get calendar list of current device
	/// </summary>
	/// <returns></returns>
	public List<string> GetCalendarList()
	{
		var eventStore = new EKEventStore();
		var calendars = eventStore.GetCalendars(EKEntityType.Event);

		return calendars.Select(c => c.Title).ToList();
	}
}