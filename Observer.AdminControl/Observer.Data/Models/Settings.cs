using Newtonsoft.Json;

namespace Observer.Data.Models
{
    /// <summary>
    /// Модель для конфигурационного файла отправки по почте.
    /// </summary>
    public class WorkerSettings
    {
        [JsonProperty("smtpclient", NullValueHandling = NullValueHandling.Ignore)]
        public EmailSetting EmailSettings { get; set; } = new();

        [JsonProperty("watchersetting", NullValueHandling = NullValueHandling.Ignore)]
        public WatcherSetting WatcherSettings { get; set; } = new();
    }

    public class EmailSetting
    {
        /// <summary>
        /// Интервал отправки сообщения
        /// </summary>
        [JsonProperty("delayinterval", NullValueHandling = NullValueHandling.Ignore)]
        public int DelayInterval { get; set; } = 1;

        [JsonProperty("from", NullValueHandling = NullValueHandling.Ignore)]
        public string From { get; set; }

        [JsonProperty("to", NullValueHandling = NullValueHandling.Ignore)]
        public string To { get; set; }

        [JsonProperty("host", NullValueHandling = NullValueHandling.Ignore)]
        public string Host { get; set; }

        [JsonProperty("port", NullValueHandling = NullValueHandling.Ignore)]
        public int Port { get; set; } = 25;
    }

    public class WatcherSetting
    {
        /// <summary>
        /// Интервал цикла опроса
        /// </summary>
        [JsonProperty("interval", NullValueHandling = NullValueHandling.Ignore)]
        public int Interval { get; set; } = 1;
    }
}
