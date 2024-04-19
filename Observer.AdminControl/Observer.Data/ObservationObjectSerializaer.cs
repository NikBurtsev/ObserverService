using Newtonsoft.Json;

namespace Observer.Data
{
    public partial class ObserverObjectsSerializer<T>
    {
        public static T FromJson(string json) => JsonConvert.DeserializeObject<T>(json, Converter.Settings);
        public static string ToJson(T self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}