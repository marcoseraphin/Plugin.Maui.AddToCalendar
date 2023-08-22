using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Plugin.Maui.AddToCalendar.Sample;

public class CloseCalendarPickerMessage : ValueChangedMessage<string>
{
	public CloseCalendarPickerMessage(string value) : base(value) { }
}

