using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Task = TODOList.Data.Models.Task;

namespace TODOList.Data
{
    public class CustomDB
    {
        private string FilePathDB { get; set; }

        public CustomDB(string pathDb)
        {
            FilePathDB = pathDb;

            if (!File.Exists(FilePathDB))
            {
                File.WriteAllText(FilePathDB, "0");
            }
        }

        public IEnumerable<Task> GetAll()
        {
            List<Task> tasks = new List<Task>();
            var lines = ReadLines(FilePathDB);

            for (int i = 0; i < lines.Count() - 1; i+=2)
            {
                Task task = new Task { Id = Convert.ToInt32(lines[i]), Title = lines[i + 1] };
                tasks.Add(task);
            }

            return tasks;
        }

        public Task Get(int? id)
        {
            var lines = ReadLines(FilePathDB);

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
            task.Id = Convert.ToInt32(GetFirstLine(FilePathDB)) + 1;
            
            List<string> data = ReadLines(FilePathDB);

            File.WriteAllText(FilePathDB, task.Id.ToString());

            File.AppendAllText(FilePathDB, "\n");

            if (data.Count > 0)
            {
                File.AppendAllLines(FilePathDB, data);
            }

            File.AppendAllText(FilePathDB, task.ToString());
        }

        public void Update(Task task)
        {
            var lines = ReadLines(FilePathDB);

            for (int i = 0; i < lines.Count() - 1; i+=2)
            {
                if (lines[i].StartsWith(task.Id.ToString()))
                {
                    lines[i + 1] = lines[i + 1].Replace(lines[i + 1], task.Title);

                    File.WriteAllText(FilePathDB, GetFirstLine(FilePathDB) + "\n");
                    File.AppendAllLines(FilePathDB, lines);
                }
            }
        }

        public void Delete(int id)
        {
            var lines = ReadLines(FilePathDB);

            for (int i = 0; i < lines.Count() - 1; i += 2)
            {
                if (lines[i].StartsWith(id.ToString()))
                {
                    lines.Remove(lines[i]);
                    lines.Remove(lines[i]);

                    File.WriteAllText(FilePathDB, GetFirstLine(FilePathDB) + "\n");
                    File.AppendAllLines(FilePathDB, lines);
                }
            }
        }

        public bool Find(int id)
        {
            var lines = ReadLines(FilePathDB);

            for (int i = 0; i < lines.Count() - 1; i += 2)
            {
                if (id == Convert.ToInt32(lines[i]))
                {
                    return true;
                }
            }
            return false;
        }

        private List<string> ReadLines(string path)
        {
            return File.ReadLines(path).Skip(1).ToList();
        }

        private string GetFirstLine(string path)
        {
            return File.ReadLines(path).ToList()[0];
        }
    }
}
