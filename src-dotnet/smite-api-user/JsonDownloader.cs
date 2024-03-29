﻿using KCode.SMITEAPI;

namespace KCode.SMITEClient
{
    public sealed class JsonDownloader
    {
        private readonly RequestClient _client;
        private readonly string _basePath;

        public TimeSpan UpdateAfter { get; set; } = TimeSpan.FromHours(20);

        public JsonDownloader(RequestClient c, string basePath)
        {
            _client = c;
            _basePath = basePath;
        }

        private FileInfo TargetFile(string filenameOrRelPath) => new FileInfo(Path.Combine(_basePath, filenameOrRelPath));

        public bool Update(string filenameOrRelPath, Func<RequestClient, Task<string>> requestMethod)
        {
            if (requestMethod == null) throw new ArgumentNullException(nameof(requestMethod));

            Directory.CreateDirectory(_basePath);

            var fi = TargetFile(filenameOrRelPath);
            if (!fi.Exists || DateTime.UtcNow - fi.LastWriteTimeUtc > UpdateAfter)
            {
                fi.Directory.Create();
                File.WriteAllText(fi.FullName, requestMethod(_client).Result);
                return true;
            }
            return false;
        }
    }
}
