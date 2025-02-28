﻿using System.Configuration;
using System.Data;
using System.Text.Json;
using System.Windows;

namespace Messenger
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public Data.DataContext dataContext { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            dataContext = new();
        }

        private static String configFileName = "email-settings.json";
        private static JsonElement? settings = null;

        public static String? GetDBName(String name)
        {
            return "DatabaseMessengers.mdf";


        }

        public static String? GetConfiguration(String name)
        {
            if (settings == null)
            {
                if (!System.IO.File.Exists(configFileName))
                {
                    MessageBox.Show($"Файл конфігурації '{configFileName}' Не знайдено ",
                        "Операція не можже бути завершеною",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return null;
                }
                try
                {
                    settings ??= JsonSerializer.Deserialize<JsonElement>(
                   System.IO.File.ReadAllText("email-settings.json"));
                }
                catch
                {
                    MessageBox.Show($"Файл конфігурації '{configFileName}' має неправильну структуру або пошкоджений");
                    return null;
                }

            }

            JsonElement? jsonElement = settings;
            try
            {
                foreach (String key in name.Split(":"))
                {
                    jsonElement = jsonElement?.GetProperty(key);
                }
                //String? host = settings?.GetProperty("smtp").GetProperty("host").GetString();
            }
            catch { return null; }

            return jsonElement?.GetString();
        }
    }

}
