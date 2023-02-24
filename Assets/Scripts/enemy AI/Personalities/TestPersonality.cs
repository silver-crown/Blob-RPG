using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPersonality : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
        GetComponent<Prototype01>().MakeTestUtility("shit in pants", "toileting");

    }
    void Update(){
        //calculate utility for toileting pants
        GetComponent<Prototype01>().AssignCategoryValue("toileting", 354);

    }
}
