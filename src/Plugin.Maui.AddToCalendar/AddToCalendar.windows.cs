using System;
using System.Collections.Generic;

namespace Plugin.Maui.AddToCalendar;

partial class AddToCalendarServiceImplementation : IAddToCalendar
{
	// TODO Implement your Android specific code
	public void CreateCalendarEvent(string title, string description, string location, DateTime startDate, DateTime endDate, string calendarName)
	{
		throw new NotImplementedException();
	}

	public List<string> GetCalendarList()
	{
		throw new NotImplementedException();
	}
}