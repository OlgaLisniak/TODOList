using System.Net;
using System.Web.Mvc;
using TODOList.Business.DTO;
using TODOList.Business.Services;

namespace TODOList.Controllers
{
    public class TaskController : Controller
    {
        private TaskService db;

        public TaskController()
        {
            db = new TaskService();
        }

        // GET: Task
        public ActionResult Index()
        {
            return View(db.GetAllTasks());
        }
        
        // GET: Task/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Task/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title")] TaskDTO taskDTO)
        {
            if (ModelState.IsValid)
            {
                db.Create(taskDTO);
                return RedirectToAction("Index");
            }

            return View(taskDTO);
        }

        // GET: Task/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TaskDTO taskDTO = db.GetTask(id);

            if (taskDTO == null)
            {
                return HttpNotFound();
            }
            return View(taskDTO);
        }

        // POST: Task/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title")] TaskDTO taskDTO)
        {
            if (ModelState.IsValid)
            {
                db.Update(taskDTO);
                return RedirectToAction("Index");
            }
            return View(taskDTO);
        }

        // GET: Task/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskDTO taskDTO = db.GetTask(id);
            if (taskDTO == null)
            {
                return HttpNotFound();
            }
            return View(taskDTO);
        }

        // POST: Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskDTO taskDTO = db.GetTask(id);
            db.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
