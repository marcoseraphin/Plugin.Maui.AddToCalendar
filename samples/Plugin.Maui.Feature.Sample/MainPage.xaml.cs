using CommunityToolkit.Mvvm.Messaging;

namespace Plugin.Maui.AddToCalendar.Sample;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;

		WeakReferenceMessenger.Default.Register<OpenCalendarPickerMessage>(this, (r, m) =>
		{
			CalendarPicker.IsVisible = true;
			CalendarPicker.Focus();
		});


		WeakReferenceMessenger.Default.Register<CloseCalendarPickerMessage>(this, (r, m) =>
		{
			CalendarPicker.Unfocus();
			CalendarPicker.IsVisible = false;
		});
	}
}
