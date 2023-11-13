using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Plugin.Maui.AddToCalendar.Sample;

public partial class MainViewModel : ObservableObject
{
	// Service
	IAddToCalendar addToCalendarService;

	[ObservableProperty]
	ObservableCollection<string> calendarPickerList;

	[ObservableProperty]
	string selectedCalendarItem;

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

	[RelayCommand]
	async Task AddEventWithDateToCalendarAsync()
	{
		try
		{
			var calendarStatusRead = await this.CheckAndRequestReadCalendarPermission();
			if (calendarStatusRead != PermissionStatus.Granted)
			{
				return;
			}

			var calendarStatusWrite = await this.CheckAndRequestWriteCalendarPermission();
			if (calendarStatusWrite != PermissionStatus.Granted)
			{
				return;
			}

			var result = await Application.Current.MainPage.DisplayAlert("Add to calendar", $"Would you like to add this event to your personal calendar?{Environment.NewLine}{Environment.NewLine}Please select one of your calendars", "Add Event", "Cancel");
			if (!result)
			{
				return;
			}

			var calendars = this.addToCalendarService.GetCalendarList();
			if (calendars.Count <= 0)
			{
				return;
			}

			var selectedCalendar = calendars.FirstOrDefault();

			// ...figure out which calendar to use, e.g. by prompting the user and considering the CanEditEvents property...
			if (calendars.Count > 1)
			{
				CalendarPickerList = new ObservableCollection<string>();
				foreach (var itemCalendar in calendars)
				{
					CalendarPickerList.Add(itemCalendar);
				}

				if (CalendarPickerList.Count > 0)
				{
					WeakReferenceMessenger.Default.Send(new OpenCalendarPickerMessage(string.Empty));
				}
			}
			else
			{
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

				WeakReferenceMessenger.Default.Send(new CloseCalendarPickerMessage(string.Empty));

				await Application.Current.MainPage.DisplayAlert("Calendar registration successful", $"The event was successfully added to calendar '{selectedCalendar}'!", "OK");
			}
		}
		catch (System.Exception ex)
		{
			Debug.WriteLine($"Error in AddJobWithDateToCalendarCommand: {ex.Message}");
		}
	}

	/// <summary>
	/// CheckAndRequestReadCalendarPermission
	/// </summary>
	/// <returns></returns>
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

	/// <summary>
	/// CheckAndRequestWriteCalendarPermission
	/// </summary>
	/// <returns></returns>
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

	/// <summary>
	/// ctor
	/// </summary>
	public MainViewModel(IAddToCalendar addToCalendarService)
	{
		this.addToCalendarService = addToCalendarService;
	}
}

