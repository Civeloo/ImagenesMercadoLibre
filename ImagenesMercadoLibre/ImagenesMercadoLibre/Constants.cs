using Newtonsoft.Json;

namespace ImagenesMercadoLibre
{
    public static class Constants
    {
        static JsonSerializerSettings jsnSerializerSettings;
        public static JsonSerializerSettings JsnSerializerSettings
        {
            get
            {
                jsnSerializerSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                return jsnSerializerSettings;
             }
        }
    }
}