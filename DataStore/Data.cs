using Newtonsoft.Json;
using CapetropolisTourism.Models;

namespace CapetropolisTourism.DataStore;

public class Data<T> where T : BaseModel
{
    private readonly string path = "DataStore/";

    public  string table { get; set; }

    public Data(string table)
    {
        this.table = table;
    }

    public List<T> GetData()
    {
        using (StreamReader r = new StreamReader(this.path + this.table + ".json"))
        {
            string json = r.ReadToEnd();
            List<T> data = JsonConvert.DeserializeObject<List<T>>(json);
            return data;
        }
    }

    public T AddData(T data)
    {
        List<T> previousData = GetData();
        data.Id = previousData.Count + 1;
        previousData.Add(data);
        string dataJson = JsonConvert.SerializeObject(previousData);
        File.WriteAllText(table, dataJson);
        return data;
    }

    public void UpdateData(T data)
    {
        List<T> previousData = GetData();
        int index = previousData.FindIndex(d => d.Id == data.Id);
        previousData[index] = data;
        string dataJson = JsonConvert.SerializeObject(previousData);
        File.WriteAllText(this.table, dataJson);
    }

    public void DeleteData(T data)
    {
        List<T> previousData = GetData();
        previousData.Remove(data);
        string dataJson = JsonConvert.SerializeObject(previousData);
        File.WriteAllText(this.table, dataJson);
    }
}