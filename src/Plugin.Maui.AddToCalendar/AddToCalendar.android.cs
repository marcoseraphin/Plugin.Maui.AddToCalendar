using Android.Content;
using Android.Provider;
using Microsoft.Maui.ApplicationModel;

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
		var calendarsUri = CalendarContract.Calendars.ContentUri;
		string[] calendarsProjection = {
										CalendarContract.Calendars.InterfaceConsts.Id,
										CalendarContract.Calendars.InterfaceConsts.CalendarDisplayName,
									   };

		if (calendarsUri != null && Platform.CurrentActivity?.ContentResolver != null)
		{
			var cursor = Platform.CurrentActivity.ContentResolver.Query(calendarsUri, calendarsProjection, null, null, null);
			long calendarId = 0;

			if (cursor != null)
			{
				while (cursor.MoveToNext())
				{
					string name = cursor.GetString(cursor.GetColumnIndex(calendarsProjection[1]));
					if (name == calendarName)
					{
						calendarId = cursor.GetLong(cursor.GetColumnIndex(calendarsProjection[0]));
						break;
					}
				}

				if (calendarId != 0)
				{
					var intent = new Intent(Intent.ActionInsert);
					intent.SetData(CalendarContract.Events.ContentUri);
					intent.PutExtra(Android.Provider.CalendarContract.IEventsColumns.CalendarId, calendarId);
					intent.PutExtra(Android.Provider.CalendarContract.IEventsColumns.Title, title);
					intent.PutExtra(Android.Provider.CalendarContract.IEventsColumns.Description, description);
					intent.PutExtra(Android.Provider.CalendarContract.IEventsColumns.EventLocation, location);
					intent.PutExtra(CalendarContract.ExtraEventBeginTime, ToUnixTimeMilliseconds(startDate));
					intent.PutExtra(CalendarContract.ExtraEventEndTime, ToUnixTimeMilliseconds(endDate));
					Platform.CurrentActivity.StartActivity(intent);
				}
			}
		}
	}

	long ToUnixTimeMilliseconds(DateTime dateTime)
	{
		DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		return (long)(dateTime.ToUniversalTime() - epoch).TotalMilliseconds;
	}

	/// <summary>
	/// Get calendar list of current device
	/// </summary>
	/// <returns></returns>
	public List<string> GetCalendarList()
	{
		var calendarsUri = CalendarContract.Calendars.ContentUri;
		string[] calendarsProjection = {
										CalendarContract.Calendars.InterfaceConsts.Id,
										CalendarContract.Calendars.InterfaceConsts.CalendarDisplayName,
									   };
		if (calendarsUri != null && Platform.CurrentActivity?.ContentResolver != null)
		{
			var cursor = Platform.CurrentActivity.ContentResolver.Query(calendarsUri, calendarsProjection, null, null, null);
			var calendarList = new List<string>();

			if (cursor != null)
			{
				while (cursor.MoveToNext())
				{
					string calendarName = cursor.GetString(cursor.GetColumnIndex(calendarsProjection[1]));
					if (!string.IsNullOrEmpty(calendarName))
					{
						calendarList.Add(calendarName);
					}
				}
			}

			return calendarList;
		}

		return new List<string>();
	}
}