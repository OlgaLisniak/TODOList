using AutoMapper;
using System.Collections.Generic;
using System.Web.Hosting;
using TODOList.Business.DTO;
using TODOList.Business.Interfaces;
using TODOList.Data;
using Task = TODOList.Data.Models.Task;

namespace TODOList.Business.Services
{
    public class TaskService : ITaskService
    {
        CustomDB db;
        
        static TaskService()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<TaskDTO, Task>();
            });
        }
        public TaskService()
        {
            string pathDb = HostingEnvironment.MapPath(@"/App_Data/Database.txt");

            db = new CustomDB(pathDb);
        }

        public void Create(TaskDTO taskDTO)
        {
            var task = Mapper.Map<TaskDTO, Task>(taskDTO);
            db.Create(task);
        }

        public void Delete(int id)
        {
            if (db.Find(id))
            {
                db.Delete(id);
            }
        }

        public IEnumerable<TaskDTO> GetAllTasks()
        {
            var mapper = new MapperConfiguration(task => task.CreateMap<Task, TaskDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Task>, IEnumerable<TaskDTO>>(db.GetAll());
        }

        public TaskDTO GetTask(int? id)
        {
            if (id != null)
            {
                Task task = db.Get(id);
                return new TaskDTO { Id = task.Id, Title = task.Title };
            }

            return null;
        }

        public void Update(TaskDTO taskDTO)
        {
            var task = Mapper.Map<TaskDTO, Task>(taskDTO);
            db.Update(task);
        }
    }
}
