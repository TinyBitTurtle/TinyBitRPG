using System.IO;
using UnityEngine;

public class ItemDatabase
{
    public string Load(string path)
    {
        TextAsset json = Resources.Load<TextAsset>("itemDatabase");

        string newjson = json.text.Replace("cDataSet", "array");
       return json == null ? null : newjson;
    }
}
