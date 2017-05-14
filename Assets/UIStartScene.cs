using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStartScene : MonoBehaviour {

    void Start()
    {
        for(int i=0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild(0).gameObject.SetActive(true);
    }
	
}
