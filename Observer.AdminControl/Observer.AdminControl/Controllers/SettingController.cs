using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Observer.Data;
using Observer.Data.Models;

namespace Observer.AdminControl.Controllers
{
    public class SettingController : Controller
    {
        private IObservationObjectRepository<WorkerSettings> _settings;
        public IObservationObjectRepository<WorkerSettings> Settings
        {
            get
            {
                if (_settings == null)
                {
                    _settings = new JsonRepository<WorkerSettings>(@"C:\Users\sierr\source\repos\Observer\Observer.Watcher\Models\JsonWrite.json");
                    _settings.Init();
                }
                return _settings;
            }
            set
            {
                if (value != null)
                    _settings = value;
            }
        }

        // GET: SettingController
        public ActionResult Index()
        {
            return RedirectToAction("Index", "View");
        }

        // GET: SettingController/Edit/5
        public ActionResult Edit() => View();

        // POST: SettingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WorkerSettings worker)
        {
            try
            {
                if (WorkerSettings.Equals(worker, Settings))
                    return RedirectToAction("Index", "View");

                Settings.Elements!.EmailSettings.DelayInterval = worker.EmailSettings.DelayInterval;
                Settings.Elements!.EmailSettings.From = worker.EmailSettings.From;
                Settings.Elements!.EmailSettings.To = worker.EmailSettings.To;
                Settings.Elements!.EmailSettings.Host = worker.EmailSettings.Host;
                Settings.Elements!.EmailSettings.Port = worker.EmailSettings.Port;
                Settings.Elements!.WatcherSettings.Interval = worker.WatcherSettings.Interval;

                Settings.Update();

                return RedirectToAction("Index", "View");
            }
            catch
            {
                return View();
            }
        }
    }
}
