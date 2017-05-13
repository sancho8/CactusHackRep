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

        
    }
}
