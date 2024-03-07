using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{
    private static List<int> totalRecords = new List<int>();
    private static string path = Application.persistentDataPath + "/save.dat";

    public static void SaveRecord(int points)
    {
        totalRecords.Add(points);
        totalRecords.Sort();
        totalRecords.Reverse();

        if (totalRecords.Count > 5)
            totalRecords.RemoveAt(totalRecords.Count - 1);

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        RecordData data = new RecordData(totalRecords);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static List<int> LoadRecord()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            RecordData data = formatter.Deserialize(stream) as RecordData;
            stream.Close();
            totalRecords = data.GetTotalRecords();
        }
        else
        {
            MeterCeros();
        }
        return totalRecords;
    }

    private static void MeterCeros()
    {
        totalRecords.Clear();
        for (int i = 0; i < 5; i++)
        {
            totalRecords.Add(0);
        }
    }
}
