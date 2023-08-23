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

# Permissions

## iOS / macOS:

You will need to add these entries to the `info.plist` file in the platform/iOS (platform/MacCatalyst) folder:

```
<key>NSCalendarsUsageDescription</key>
<string>Permissions are required to add events to the calendar.</string>
```

## Android:

You will need to add these entries to the `AndroidManifest.xml` file in the platform/Android folder:

```
<uses-permission android:name="android.permission.READ_CALENDAR" />
<uses-permission android:name="android.permission.WRITE_CALENDAR" />
```

## .Net MAUI App
In your app you can request these permissions like this:
(please note that in Android you need to request both read and write permissions):

```
public async Task<PermissionStatus> CheckAndRequestReadCalendarPermission()
{
	var status = await Permissions.CheckStatusAsync<Permissions.CalendarRead>();

	if (status == PermissionStatus.Granted)
	{
		return status;
	}

	if (status == PermissionStatus.Denied)
	{
		status = await Permissions.RequestAsync<Permissions.CalendarRead>();
		return status;
	}

	status = await Permissions.RequestAsync<Permissions.CalendarRead>();

	return status;
}
	
public async Task<PermissionStatus> CheckAndRequestWriteCalendarPermission()
{
	var status = await Permissions.CheckStatusAsync<Permissions.CalendarWrite>();

	if (status == PermissionStatus.Granted)
	{
		return status;
	}

	if (status == PermissionStatus.Denied)
	{
		status = await Permissions.RequestAsync<Permissions.CalendarWrite>();
		return status;
	}

	status = await Permissions.RequestAsync<Permissions.CalendarWrite>();

	return status;
}
```

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

iOS:

<img src="https://user-images.githubusercontent.com/10572315/262338408-5f3ab3af-6dd1-4fe0-a8c9-fed8a84da64b.png" height="400" alt="Screenshot1"/> <img src="https://user-images.githubusercontent.com/10572315/262338398-6faeea40-aa0e-4b39-863c-f335a82deed5.png" height="400" alt="Screenshot1"/> <img src="https://user-images.githubusercontent.com/10572315/262338401-6c9a992b-657b-4c47-9240-c7f4c89fef18.png" height="400" alt="Screenshot1"/>

<img src="https://user-images.githubusercontent.com/10572315/262338410-3daae351-edc6-4bbe-a82d-ad99a312a551.png" height="400" alt="Screenshot1"/> <img src="https://user-images.githubusercontent.com/10572315/262338419-4c196081-34ea-4fb3-a1cb-8a824ba2b3c5.png" height="400" alt="Screenshot1"/> <img src="https://user-images.githubusercontent.com/10572315/262338420-2387b161-08ff-42b1-9498-a6b18bd54753.png" height="400" alt="Screenshot1"/>

Android:

<img src="https://user-images.githubusercontent.com/10572315/262338398-6faeea40-aa0e-4b39-863c-f335a82deed5.png" height="400" alt="Screenshot1"/> <img src="https://user-images.githubusercontent.com/10572315/262338394-a3fb03ad-ebf5-4aad-a39a-50696de211a0.png" height="400" alt="Screenshot1"/> <img src="https://user-images.githubusercontent.com/10572315/262338396-a508ccb4-e9b6-49bc-8525-de41145d0a7b.png" height="400" alt="Screenshot1"/>

Mac OS:

<img src="https://user-images.githubusercontent.com/10572315/262338382-7cc3b322-ca56-40e9-a47c-b692edba0ea2.png" height="400" alt="Screenshot1"/>

<img src="https://user-images.githubusercontent.com/10572315/262338387-3db0c302-0342-4d04-ac6e-9655fc6f1a48.png" height="400" alt="Screenshot1"/>

<img src="https://user-images.githubusercontent.com/10572315/262338392-d2d77fb6-6336-4118-8f50-aa0f34171f7c.png" height="400" alt="Screenshot1"/>