using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.Localization;


public static class ServiceCollectionResourceLocalizationManagerExtension
{
    public static IServiceCollection AddYamlResourceLocalization(this IServiceCollection services)
    {
        services.AddScoped<ILocalizationService, ResourceLocalizationManager>(delegate
        {
            Dictionary<string, Dictionary<string, string>> dictionary = new Dictionary<string, Dictionary<string, string>>();
            //string[] directories = Directory.GetDirectories(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Features"));
            string[] directories = Directory.GetDirectories(Path.Combine(Path.GetDirectoryName(AppContext.BaseDirectory), "..", "..", "..", "..", "Application", "Features"));
            foreach (string obj in directories)
            {
                string fileName = Path.GetFileName(obj);
                string path = Path.Combine(obj, "Resources", "Locales");
                if (Directory.Exists(path))
                {
                    string[] files = Directory.GetFiles(path);
                    foreach (string text in files)
                    {
                        string? fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text);
                        int num = fileNameWithoutExtension.IndexOf('.');
                        string text2 = fileNameWithoutExtension;
                        int num2 = num + 1;
                        string key = text2.Substring(num2, text2.Length - num2);
                        if (File.Exists(text))
                        {
                            if (!dictionary.ContainsKey(key))
                            {
                                dictionary.Add(key, new Dictionary<string, string>());
                            }

                            dictionary[key].Add(fileName, text);
                        }
                    }
                }
            }

            return new ResourceLocalizationManager(dictionary);
        });
        return services;
    }
}