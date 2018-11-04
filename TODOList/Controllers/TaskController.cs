using System.Net;
using System.Web.Mvc;
using TODOList.Business.DTO;
using TODOList.Business.Interfaces;
using TODOList.Business.Services;

namespace TODOList.Controllers
{
    public class TaskController : Controller
    {
        private ITaskService taskService = new TaskService();
        
        // GET: Task
        public ActionResult Index()
        {
            return View(taskService.GetAllTasks());
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
                taskService.Create(taskDTO);
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

            TaskDTO taskDTO = taskService.GetTask(id);

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
                taskService.Update(taskDTO);
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
            TaskDTO taskDTO = taskService.GetTask(id);
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
            var taskDTO = taskService.GetTask(id);
            taskService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
