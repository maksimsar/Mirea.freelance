using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Mirea.freelance.backend.data
{
    public class GitLabClient
    {
        private readonly HttpClient    _http;
        private readonly GitLabSettings _cfg;

        public GitLabClient(HttpClient http, IOptions<GitLabSettings> cfg)
        {
            _http = http;
            _cfg  = cfg.Value;
            _http.BaseAddress = new Uri($"{_cfg.BaseUrl}/projects/{_cfg.ProjectId}/");
            _http.DefaultRequestHeaders.Add("PRIVATE-TOKEN", _cfg.PrivateToken);
        }

        public async Task<IEnumerable<GitLabFile>> ListFilesAsync(string path = null, string @ref = "main")
        {
            // Собираем URL-запрос
            var url = $"repository/tree?ref={@ref}";
            if (!string.IsNullOrEmpty(path))
                url += $"&path={Uri.EscapeDataString(path)}";

            // Выполняем
            var res = await _http.GetAsync(url);

            // Если директории нет или она пуста — просто возвращаем пустой список
            if (res.StatusCode == System.Net.HttpStatusCode.NotFound)
                return Enumerable.Empty<GitLabFile>();

            res.EnsureSuccessStatusCode();
            return await JsonSerializer.DeserializeAsync<IEnumerable<GitLabFile>>(
                await res.Content.ReadAsStreamAsync()
            );
        }


        public async Task<GitLabFileContent> GetFileAsync(string path, string @ref = "main")
        {
            var url = $"repository/files/{Uri.EscapeDataString(path)}?ref={@ref}";
            var res = await _http.GetAsync(url);
            if (res.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;
            res.EnsureSuccessStatusCode();
            return await JsonSerializer.DeserializeAsync<GitLabFileContent>(
                await res.Content.ReadAsStreamAsync());
        }

        public async Task CreateFileAsync(string path, string branch, string contentBase64, string commitMessage)
        {
            var body = new
            {
                branch,
                content        = contentBase64,
                commit_message = commitMessage,
                encoding       = "base64"
            };
            var payload = new StringContent(
                JsonSerializer.Serialize(body),
                Encoding.UTF8,
                "application/json");
            var res = await _http.PostAsync(
                $"repository/files/{Uri.EscapeDataString(path)}",
                payload);
            res.EnsureSuccessStatusCode();
        }

        public async Task UpdateFileAsync(string path, string branch, string contentBase64, string commitMessage)
        {
            var body = new
            {
                branch,
                content        = contentBase64,
                commit_message = commitMessage,
                encoding       = "base64"
            };
            var payload = new StringContent(
                JsonSerializer.Serialize(body),
                Encoding.UTF8,
                "application/json");
            var res = await _http.PutAsync(
                $"repository/files/{Uri.EscapeDataString(path)}",
                payload);
            res.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<GitLabCommit>> ListCommitsAsync(string path, string @ref = "main")
        {
            var url = $"repository/commits?ref_name={@ref}&path={Uri.EscapeDataString(path)}";
            var res = await _http.GetAsync(url);
            res.EnsureSuccessStatusCode();
            return await JsonSerializer.DeserializeAsync<IEnumerable<GitLabCommit>>(
                await res.Content.ReadAsStreamAsync());
        }

        public async Task DeleteFileAsync(string path, string branch, string commitMessage)
        {
            var url = $"repository/files/{Uri.EscapeDataString(path)}"
                    + $"?branch={Uri.EscapeDataString(branch)}"
                    + $"&commit_message={Uri.EscapeDataString(commitMessage)}";
            var req = new HttpRequestMessage(HttpMethod.Delete, url);
            var res = await _http.SendAsync(req);
            res.EnsureSuccessStatusCode();
        }
    }

    public class GitLabFile
    {
        public string type { get; set; }  // "blob" или "tree"
        public string name { get; set; }
        public string path { get; set; }
    }

    public class GitLabFileContent
    {
        public string file_name { get; set; }
        public string file_path { get; set; }
        public string content   { get; set; } // base64
        public string encoding  { get; set; }
    }

    public class GitLabCommit
    {
        public string   id             { get; set; }
        public string   short_id       { get; set; }
        public string   title          { get; set; }
        public string   message        { get; set; }
        public DateTime committed_date { get; set; }
    }
}
