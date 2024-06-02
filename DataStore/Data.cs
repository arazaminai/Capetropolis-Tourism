using Newtonsoft.Json;
using CapetropolisTourism.Models;
using System.IO;

namespace CapetropolisTourism.DataStore;

public class Data<T> where T : BaseModel
{
    private readonly string path = "DataStore/";

    public  string table { get; set; }

    public Data(string table)
    {
        this.table = this.path + table + ".json";
    }

    public List<T> GetData()
    {
        using (StreamReader r = new StreamReader(this.table))
        {
            string json = r.ReadToEnd();
            List<T> data = JsonConvert.DeserializeObject<List<T>>(json);
            return data;
        }
    }

    public T GetById(int id)
    {
        List<T> data = GetData();
        return data.FirstOrDefault(d => d.Id == id);
    }

    public T AddData(T data)
    {
        List<T> previousData = GetData();
        data.Id = previousData.Count > 0 ? previousData.Last().Id + 1 : 1;
        previousData.Add(data);
        string dataJson = JsonConvert.SerializeObject(previousData);
        System.IO.File.WriteAllText(this.table, dataJson);
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
        previousData.RemoveAll(d => d.Id == data.Id);
        string dataJson = JsonConvert.SerializeObject(previousData);
        File.WriteAllText(this.table, dataJson);
    }
}