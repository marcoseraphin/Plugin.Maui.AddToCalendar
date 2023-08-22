namespace Plugin.Maui.AddToCalendar;

public static class AddToCalendarService
{
	static IAddToCalendar? defaultImplementation;

	/// <summary>
	/// Provides the default implementation for static usage of this API.
	/// </summary>
	public static IAddToCalendar Default =>
		defaultImplementation ??= new AddToCalendarServiceImplementation();

	internal static void SetDefault(IAddToCalendar? implementation) =>
		defaultImplementation = implementation;
}
