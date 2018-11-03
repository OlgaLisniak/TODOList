namespace TODOList.Data.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public override string ToString()
        {
            string task =  Id + "\n" + Title + "\n";
            return task;
        }
    }
}
