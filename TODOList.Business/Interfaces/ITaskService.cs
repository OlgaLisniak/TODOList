using System.Collections.Generic;
using TODOList.Business.DTO;

namespace TODOList.Business.Interfaces
{
    public interface ITaskService
    {
        List<TaskDTO> GetAllTasks();
        TaskDTO GetTask(int? id);
        void Create(TaskDTO task);
        void Update(TaskDTO task);
        void Delete(int id);
    }
}
