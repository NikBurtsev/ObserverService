using Microsoft.AspNetCore.Mvc;
using Observer.Data;
using Observer.Data.Models;

namespace Observer.AdminControl.Controllers
{
    /// <summary>
    /// Работа с отображение информации о состояниях процессов и сервисов.
    /// </summary>
    public class ViewController : Controller
    {
        private IObservationObjectRepository<ObservationObjects> _repository;

        public IObservationObjectRepository<ObservationObjects> Repository
        {
            get
            {
                if (_repository == null)
                {
                    _repository = new JsonRepository<ObservationObjects>(Path.Combine(@"C:\Users\sierr\source\repos\Observer\Observer.Watcher\Models\JsonFile.json"));
                    _repository.Init();
                }
                return _repository;
            }

            set
            {
                if (value != null)
                    _repository = value;
            }
        }

        public IActionResult Index(int viewDeleted = 0)
        {
            if (Repository.Elements!.Items.Any())
                return View(viewDeleted == 0
                    ? Repository?.Elements?.Items.Where(x => x.IsActive == true && x.ActiveStatus == 1).ToList<ObservationObject>()
                    : Repository?.Elements?.Items);
            return NotFound();
        }

        //Метод для удаления сервиса
        public IActionResult EnableDisable(int id)
        {
            if (Repository.Elements is not null)
            {
                var findItem = Repository.Elements.Items.Find(x => x.Id == id);

                if (findItem != null)
                {
                    findItem.IsActive = !findItem.IsActive;
                    Repository.Update();
                }
                return RedirectToAction("Index");

            }
            return NotFound();
        }

        public IActionResult Edit(int id)
        {
            var findItem = Repository.Elements.Items.Find(x => x.Id == id);

            if (findItem == null)
                return NotFound();
            return View(findItem);
        }

        // POST: ObservController/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,FqdnName,Name,ProgrammType,Message,IsActive,LastNotifySend,ActiveStatus")] ObservationObject observationObject)
        {
            if (ModelState.IsValid && Repository.Elements is not null)
            {
                var findItem = Repository.Elements.Items.Find(x => x.Id == id);

                if (findItem is not null)
                {
                    if (ObservationObject.Equals(findItem, observationObject)) //Как сравнивает?? Но пока работает
                        return RedirectToAction(nameof(Index));

                    findItem.Name = observationObject.Name;
                    findItem.FqdnName = observationObject.FqdnName;
                    findItem.ProgrammType = observationObject.ProgrammType;
                    findItem.IsActive = observationObject.IsActive;

                    Repository.Update();

                    return RedirectToAction(nameof(Index));
                }
                return NotFound(ModelState);
            }
            return View(observationObject);
        }

        public IActionResult CreateNew() => View(new ObservationObject());

        // POST: ObservController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult CreateNew(int id, [Bind("Id,FqdnName,Name,ProgrammType,Message,IsActive,LastNotifySend,ActiveStatus")] ObservationObject observationObject)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id == 0 && Repository.Elements is not null)
                    {
                        observationObject.Id = (Repository.Elements.Items.Max(x => x.Id) + 1);

                        Repository.Elements.Items.Add(observationObject);
                        Repository.Update();
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(observationObject);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public IActionResult IndexWithDeleted(ObservationObject viewDeleted) =>
            RedirectToAction(nameof(Index), new { viewDeleted = 1 });
    }
}
