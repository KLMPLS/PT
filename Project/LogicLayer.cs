namespace MyApplication.Logic
{
    using MyApplication.Data;

    public class LogicLayer
    {
        private DataLayer dataLayer;

        public LogicLayer()
        {
            dataLayer = new DataLayer();
        }

        public void AddData(string data)
        {
            dataLayer.AddData(data);
        }

        public List<string> GetData()
        {
            return dataLayer.GetData();
        }
    }
}
