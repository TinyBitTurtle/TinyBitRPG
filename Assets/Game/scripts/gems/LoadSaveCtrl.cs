using UnityEngine;
using System.IO;

using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Text;

namespace TinyBitTurtle
{
    public partial class LoadSaveCtrl : SingletonMonoBehaviour<LoadSaveCtrl>
    {
        private string permanentPath;
        private ItemDatabase itemDatabase;
        private BinaryFormatter bf = new BinaryFormatter();
        private GameData gameData;

        private void Awake()
        {
            gameData = ScriptableObject.CreateInstance<GameData>();
            itemDatabase = new ItemDatabase();

            permanentPath = Application.persistentDataPath + "/game.sav"; ;

            // load item database
            string json = itemDatabase.Load(permanentPath);
            ItemClass[] itemClass = JsonHelper.getJsonArray<ItemClass>(json);
        }

        //
        public void SaveGameData()
        {
            // set values into save class
            //gameData.currentLevel = currentLevel;

            //// get the save string from the serialized class
            //string stream = JsonUtility.ToJson(permanentPath);

            //// 64 based encode the string
            //byte[] encodedBytes = Encoding.UTF8.GetBytes(stream);
            //string encodedString = Convert.ToBase64String(encodedBytes);

            //// write out the encoded string
            //File.WriteAllText(permanentPath, stream);
        }

        //
        public void LoadGameData()
        {
            // do we have a save file?
            if (File.Exists(permanentPath))
            {
                //// read in the encoded string
                //string stream = File.ReadAllText(permanentPath);

                //// decode it
                //byte[] decryptedBytes = Convert.FromBase64String(stream);
                //string decryptedString = Encoding.UTF8.GetString(decryptedBytes);

                //// set values into save class
                //GameData gameData = JsonUtility.FromJson<GameData>(decryptedString);
                //currentLevel = gameData.currentLevel;
            }
        }

        public void LoadDatabaseItems()
        {
            //gameData = new loadSaveCtrl.GameData();

            //TextAsset json = Resources.Load<TextAsset>("GameInit");

            //return JsonUtility.FromJson<GameData>(json.text);
        }

        public void LoadCrafting()
        {
            //gameData = new loadSaveCtrl.GameData();

            //TextAsset json = Resources.Load<TextAsset>("GameInit");

            //return JsonUtility.FromJson<GameData>(json.text);
        }
    }
}