using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BlogHandler
{
    internal class Program
    {
        private const string _address = @"https://localhost:44304/";
        private const string _filePath = "results.txt";
        private static readonly (int, int) _idRange = (4, 13);
        private static List<string> _posts;

        static void Main(string[] args)
        {
            _posts = new List<string>(_idRange.Item2 - _idRange.Item1 + 1);
            Task<string>[] tasks = new Task<string>[_posts.Capacity];

            for (int i = 0, startId = _idRange.Item1, endId = _idRange.Item2; startId <= endId; startId++, i++)
            {
                tasks[i] = Task.Run(() => GetPostAsync(startId));
            }

            Task.WaitAll(tasks);

            for(int i = 0; i < tasks.Length; i++)
            {
                _posts[i] = Parse(tasks[i].Result) + "\n\n";
            }

            SavePosts(_posts);
        }
        private static async Task<string> GetPostAsync(int id)
        {
            using var client = new HttpClient()
            {
                BaseAddress = new Uri(_address)
            };
            string response = string.Empty;
            try
            {
                response = await client.GetStringAsync($"posts/{id}");
            }
            catch (HttpRequestException e)
            {
                response = e.Message;
            }
            return response;
        }
        private static string Parse(string s) => JsonConvert.DeserializeObject<Post>(s).ToString();
        private static void SavePosts(List<string> posts)
        {
            File.WriteAllText(_filePath, String.Empty);

            posts.ForEach((e) => {
                File.AppendAllText(_filePath, e);
            });
        }
    }
}
