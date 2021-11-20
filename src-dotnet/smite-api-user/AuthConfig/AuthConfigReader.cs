using YamlDotNet.RepresentationModel;

namespace KCode.SMITEClient.AuthConfig
{
    public static class AuthConfigReader
    {
        private static readonly string _filepath = ".auth.yaml";

        /// <exception cref="InvalidOperationException"></exception>
        public static Auth Read()
        {
            var fi = new FileInfo(_filepath);
            if (!fi.Exists) throw new InvalidOperationException($"Missing auth config file {_filepath}");

            try
            {
                var (devId, authKey) = ParseConfigFile(fi);
                return new Auth(devId, authKey);
            }
            catch (KeyNotFoundException e)
            {
                throw new InvalidOperationException($"Invalid auth config file format of {_filepath}. {e.Message}");
            }
        }

        private static (int devId, string authKey) ParseConfigFile(FileInfo fi)
        {
            var yaml = new YamlStream();
            using var f = File.OpenText(fi.FullName);
            yaml.Load(f);

            var mapping = (YamlMappingNode)yaml.Documents[0].RootNode;
            var devId = int.Parse(mapping.Children["devid"].ToString(), provider: null);
            var authKey = mapping.Children["authkey"].ToString();
            return (devId, authKey);
        }
    }
}
