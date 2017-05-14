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

        public List<string> gameNames;

        public GameContainer CreateNewGameContainer(string name)
        {
            GameContainer gc = new GameContainer(true);
            gc.name = name;
            Save();
            gc.Save(name);
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
            XmlSerializer serializer = new XmlSerializer(typeof(AllGamesContainer));
            TextReader reader = new StreamReader(System.IO.Path.Combine(Application.persistentDataPath, fileName));
            AllGamesContainer allGamesContainer = serializer.Deserialize(reader) as AllGamesContainer;
            reader.Close();
            Debug.Log("AllGamesContainer loaded from XML file\n");
            return allGamesContainer;
        }

    }
}
