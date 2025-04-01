namespace MyApplication.Data
{
    public class DataLayer
    {
        private List<string> dataStore = new List<string>();

        public void AddData(string data)
        {
            dataStore.Add(data);
        }

        public List<string> GetData()
        {
            return dataStore;
        }
    }
}
