using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace ARQuestCreator.SceneCreator
{
    [System.Serializable]
    public class GameContainer
    {
        public GameContainer() { }

        public GameContainer(bool isNew)
        {
            if (isNew)
            {
                scenes = new List<SceneContainer>();
            }
        }

        public string name;
        
        public List<SceneContainer> scenes;

        public void Save(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GameContainer));
            TextWriter writer = new StreamWriter(System.IO.Path.Combine(Application.persistentDataPath, fileName));
            serializer.Serialize(writer, this);
            writer.Close();
            Debug.Log("GameContainer saved into XML file\n");
        }

        public static GameContainer Load(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GameContainer));
            TextReader reader = new StreamReader(System.IO.Path.Combine(Application.persistentDataPath, fileName));
            GameContainer allGamesContainer = serializer.Deserialize(reader) as GameContainer;
            reader.Close();
            Debug.Log("AllGamesContainer loaded from XML file\n");
            return allGamesContainer;
        }
    }
}
