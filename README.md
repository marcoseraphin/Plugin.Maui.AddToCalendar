# Plugin.Maui.AddToCalendar

With this plugin `Plugin.Maui.AddToCalendar` it is possible in .Net MAUI apps for the platforms iOS, macOS and Android 
(Windows is currently not supported) to read out the calendar list on the corresponding device and then to create a calendar entry containing these parameters by specifying the selected calendar:

- Title of the entry
- Description of the entry
- Location
- Start date/time
- End date/time

The plugin has the following API:

```
public interface IAddToCalendar
{
	List<string> GetCalendarList(); 
	void CreateCalendarEvent(string title, string description, string location, DateTime startDate, DateTime endDate, string calendarName);
}
```

The repository also contains a small .Net MAUI demo app that includes the following features:

- Read calendar list and display it in a picker.
- Query the calendar permissions for the corresponding platform
- Adding a demo calendar entry for the current day in the selected calendar
- Use of the [MVVM CommunityToolkit](https://learn.microsoft.com/en-US/dotnet/communitytoolkit/mvvm/)
- Using the [.Net MAUI CommunityToolkit](https://learn.microsoft.com/en-US/dotnet/communitytoolkit/maui/)
- MVVM Architecture


## Getting Started

This sample code shows the usage of the API methods:

```
partial void OnSelectedCalendarItemChanged(string value)
	{
		string selectedCalendar = null;

		var calendars = this.addToCalendarService.GetCalendarList();
		if (calendars.Count <= 0)
		{
			return;
		}

		// get selected calendar
		if (calendars.Count <= 1)
		{
			return;
		}

		foreach (var itemCalendar in calendars)
		{
			if (SelectedCalendarItem != itemCalendar)
			{
				continue;
			}
			selectedCalendar = itemCalendar;
			break;
		}

		DateTime today = DateTime.Now;
		var startDate = new DateTime(today.Year,
									 today.Month,
									 today.Day, 8, 0, 0);

		var endDate = new DateTime(today.Year,
								   today.Month,
								   today.Day, 18, 0, 0);

		this.addToCalendarService.CreateCalendarEvent("Event MAUI conference",
		"Visit the MAUI conference, URL: https://learn.microsoft.com/en-US/dotnet/maui/what-is-maui",
		"Redmond", startDate, endDate, this.SelectedCalendarItem);

		if (!string.IsNullOrEmpty(selectedCalendar))
		{
			WeakReferenceMessenger.Default.Send(new CloseCalendarPickerMessage(string.Empty));
			Application.Current.MainPage.DisplayAlert("Calendar registration successful", $"The event was successfully added to calendar '{selectedCalendar}'!", "OK");
		}
	}
   ```

   Here some ScreenShots of the sample app using the `Plugin.Maui.AddToCalendar` for iOS, Android and macOS:

      Here some ScreenShots of the sample app using the `Plugin.Maui.AddToCalendar` for iOS, Android and macOS:

   (https://user-images.githubusercontent.com/10572315/262338408-5f3ab3af-6dd1-4fe0-a8c9-fed8a84da64b.png)
