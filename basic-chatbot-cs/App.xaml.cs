using System;
using System.Windows;
using System.Windows.Media;

namespace basic_chatbot_cs
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Initialize any application-wide services here
            InitializeExceptionHandling();

            // Apply any application-wide styles or themes
            ApplyGlobalStyles();
        }

        private void InitializeExceptionHandling()
        {
            // Global exception handling
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                MessageBox.Show($"An unexpected error occurred: {args.ExceptionObject}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            };

            DispatcherUnhandledException += (sender, args) =>
            {
                MessageBox.Show($"An application error occurred: {args.Exception.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                args.Handled = true;
            };
        }

        private void ApplyGlobalStyles()
        {
            // Set application-wide font
            Resources.Add(SystemFonts.MessageFontFamilyKey, new FontFamily("Segoe UI"));

            // Set application-wide colors
            Resources["PrimaryColor"] = Color.FromRgb(0, 122, 255);
            Resources["SecondaryColor"] = Color.FromRgb(229, 229, 234);
            Resources["AccentColor"] = Color.FromRgb(52, 199, 89);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // Clean up any resources here
            base.OnExit(e);
        }
    }
}