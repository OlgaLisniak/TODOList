using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Task = TODOList.Data.Models.Task;

namespace TODOList.Data
{
    public class CustomDB
    {
        public string FilePathDB { get; set; }
        private string FilePathId { get; set; }

        public CustomDB(string pathDb, string pathId)
        {
            FilePathDB = pathDb;
            FilePathId = pathId;

            if (!File.Exists(FilePathId))
            {
                File.WriteAllText(FilePathId, "0");
            }
        }

        public List<Task> GetAll()
        {
            List<Task> tasks = new List<Task>();
            List<string> lines = File.ReadLines(FilePathDB).ToList();

            for (int i = 0; i < lines.Count() - 1; i+=2)
            {
                Task task = new Task { Id = Convert.ToInt32(lines[i]), Title = lines[i + 1] };
                tasks.Add(task);
            }

            return tasks;
        }

        public Task Get(int? id)
        {
            List<string> lines = File.ReadLines(FilePathDB).ToList();

            for (int i = 0; i < lines.Count() - 1; i += 2)
            {
                if (id == Convert.ToInt32(lines[i]))
                {
                    return new Task { Id = Convert.ToInt32(lines[i]), Title = lines[i + 1] };
                }
            }

            return null;
        }

        public void Create(Task task)
        {
            task.Id = Convert.ToInt32(File.ReadAllText(FilePathId)) + 1;

            File.AppendAllText(FilePathDB, task.ToString());
            File.WriteAllText(FilePathId, task.Id.ToString());
        }

        public void Update(Task task)
        {
            var lines = File.ReadLines(FilePathDB).ToList();

            for (int i = 0; i < lines.Count() - 1; i+=2)
            {
                if (lines[i].StartsWith(task.Id.ToString()))
                {
                    lines[i + 1] = lines[i + 1].Replace(lines[i + 1], task.Title);

                    File.WriteAllLines(FilePathDB, lines);
                }
            }
        }

        public void Delete(int id)
        {
            var lines = File.ReadLines(FilePathDB).ToList();

            for (int i = 0; i < lines.Count() - 1; i += 2)
            {
                if (lines[i].StartsWith(id.ToString()))
                {
                    lines.Remove(lines[i]);
                    lines.Remove(lines[i]);

                    File.WriteAllLines(FilePathDB, lines);
                }
            }
        }
    }
}
