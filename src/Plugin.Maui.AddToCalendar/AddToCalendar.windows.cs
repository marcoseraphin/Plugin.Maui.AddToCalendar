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
		var calendarList = new List<string>();

		var appointmentStore = await AppointmentManager.RequestStoreAsync(AppointmentStoreAccessType.AllCalendarsReadOnly);
		var calendars = await appointmentStore.FindAppointmentCalendarsAsync(FindAppointmentCalendarsOptions.None);
		foreach (var calendar in calendars)
		{
			calendarList.Add(calendar.DisplayName);
			System.Diagnostics.Debug.WriteLine($"Calendar: {calendar.DisplayName}");
		}

		return calendarList;
	}
}