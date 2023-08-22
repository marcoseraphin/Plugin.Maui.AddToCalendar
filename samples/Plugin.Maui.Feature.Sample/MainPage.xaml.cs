using Plugin.Maui.AddToCalendar;

namespace Plugin.Maui.AddToCalendar.Sample;

public partial class MainPage : ContentPage
{
	readonly IAddToCalendar feature;

	public MainPage(IAddToCalendar feature)
	{
		InitializeComponent();

		this.feature = feature;
	}

	/// <summary>
	/// CheckAndRequestCalendarPermission
	/// </summary>
	/// <returns></returns>
	public async Task<PermissionStatus> CheckAndRequestCalendarPermission()
	{
		var status = await Permissions.CheckStatusAsync<Permissions.CalendarWrite>();

		if (status == PermissionStatus.Granted)
		{
			return status;
		}

		if (status == PermissionStatus.Denied)
		{
			return status;
		}

		status = await Permissions.RequestAsync<Permissions.CalendarWrite>();

		return status;
	}

}
