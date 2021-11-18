using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager CM;
    public bool Fighting;
    public Animator transition;
    public enum Backdrops{
        Forest,
        Field,
        Water,
        Beach,
        Desert,
        Volcano,
        MountainTop,
        House,
        Castle,
        Town
    }
    public Backdrops backdrop;
    public Entity[] enemies;
    private void Awake() {
        //If a manager doesn't already exist, make this the manager
        if (CM == null) {
            DontDestroyOnLoad(this);
            CM = this;
        }
        //if there is a manager 
        else if (CM != this) {
            Destroy(gameObject);
        }
    }
}
