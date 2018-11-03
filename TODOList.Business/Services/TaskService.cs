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
            string pathId = HostingEnvironment.MapPath(@"/App_Data/LastId.txt");

            db = new CustomDB(pathDb, pathId);
        }

        public void Create(TaskDTO taskDTO)
        {
            var task = Mapper.Map<TaskDTO, Task>(taskDTO);
            db.Create(task);
        }

        public void Delete(int id)
        {
            db.Delete(id);
            
        }

        public List<TaskDTO> GetAllTasks()
        {
            var mapper = new MapperConfiguration(task => task.CreateMap<Task, TaskDTO>()).CreateMapper();
            return mapper.Map<List<Task>, List<TaskDTO>>(db.GetAll());
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
