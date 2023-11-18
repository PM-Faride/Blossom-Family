using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveFirstTime
{
    public static void SaveFirstTimeInfo(bool info)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/firstTime.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        FirstTimeData firstTime = new FirstTimeData(info);

        formatter.Serialize(stream, firstTime);
        stream.Close();
    }

    public static FirstTimeData LoadFirstTimeInfo()
    {
        string path = Application.persistentDataPath + "/firstTime.fun";
        FirstTimeData info;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            info = formatter.Deserialize(stream) as FirstTimeData;
            stream.Close();

        }
        else
        {
            Debug.Log("Save file not found in" + path);
            info = new FirstTimeData(false);
            //return info;
        }
        return info;
    }
}
