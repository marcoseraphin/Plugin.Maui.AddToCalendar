using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Plugin.Maui.AddToCalendar.Sample;

public class OpenCalendarPickerMessage : ValueChangedMessage<string>
{
	public OpenCalendarPickerMessage(string value) : base(value) { }
}
