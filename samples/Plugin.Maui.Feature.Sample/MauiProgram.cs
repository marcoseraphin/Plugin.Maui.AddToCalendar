using Microsoft.Extensions.DependencyInjection;
using Plugin.Maui.AddToCalendar;

namespace Plugin.Maui.AddToCalendar.Sample;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddTransient<MainPage>();
		builder.Services.AddSingleton<IAddToCalendar>(AddToCalendarService.Default);

		return builder.Build();
	}
}