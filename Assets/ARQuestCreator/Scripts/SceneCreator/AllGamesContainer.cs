using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
namespace ARQuestCreator.SceneCreator
{
    [System.Serializable]
    public class AllGamesContainer
    {

        public AllGamesContainer() { }

        public AllGamesContainer(bool isNew)
        {
            if (isNew)
            {
                gameNames = new List<string>();
            }
        }

        public List<string> gameNames;

        public GameContainer CreateNewGameContainer(string name)
        {
            GameContainer gc = new GameContainer(true);
            gc.name = name;
            gc.Save();
            gameNames.Add(name);
            Save();
            gc = GameContainer.Load(name);
            return gc;
        }

        public void Save(string fileName = "allGamesContainer.xml")
        {
            XmlSerializer serializer = new XmlSerializer(typeof(AllGamesContainer));
            TextWriter writer = new StreamWriter(System.IO.Path.Combine(Application.persistentDataPath, fileName));
            serializer.Serialize(writer, this);
            writer.Close();
            Debug.Log("AllGamesContainer saved into XML file\n");
        }

        public static AllGamesContainer Load(string fileName = "allGamesContainer.xml")
        {
            AllGamesContainer allGamesContainer;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(AllGamesContainer));
                TextReader reader = new StreamReader(System.IO.Path.Combine(Application.persistentDataPath, fileName));
                allGamesContainer = serializer.Deserialize(reader) as AllGamesContainer;
                reader.Close();
            }
            catch (IOException e)
            {
                Debug.Log("Non create all game container");
                allGamesContainer = new AllGamesContainer(true);
                allGamesContainer.Save();
            }
            Debug.Log("AllGamesContainer loaded from XML file\n");
            return allGamesContainer;
        }

    }
}
