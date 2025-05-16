using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Mirea.freelance.backend.data;

namespace Mirea.freelance.backend.Infrastructure
{
    public sealed class GitLabClientSingleton
    {
        private static readonly Lazy<GitLabClient> _instance = new Lazy<GitLabClient>(() =>
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables();
            var config = builder.Build();

            // Читаем секцию "GitLab" вместо "GitLabSettings"
            var section = config.GetSection("GitLab");
            if (!section.Exists())
                throw new InvalidOperationException(
                    "В appsettings.json отсутствует секция \"GitLab\". " +
                    "Добавьте что-то вроде:\n" +
                    "{\n" +
                    "  \"GitLab\": {\n" +
                    "    \"BaseUrl\": \"https://gitlab.com/api/v4/\",\n" +
                    "    \"PrivateToken\": \"...\",\n" +
                    "    \"ProjectId\": 123456\n" +
                    "  }\n" +
                    "}"
                );

            // Модель настроек должна иметь те же поля, что в JSON
            var settings = section.Get<GitLabSettings>();
            if (settings is null ||
                string.IsNullOrWhiteSpace(settings.BaseUrl) ||
                string.IsNullOrWhiteSpace(settings.PrivateToken))
            {
                throw new InvalidOperationException(
                    "Секция \"GitLab\" найдена, но не заданы обязательные поля BaseUrl и PrivateToken."
                );
            }

            // Собираем HttpClient
            var httpClient = new HttpClient { BaseAddress = new Uri(settings.BaseUrl) };
            var options    = Options.Create(settings);

            return new GitLabClient(httpClient, options);
        });

        private GitLabClientSingleton() { }

        public static GitLabClient Instance => _instance.Value;
    }
}
