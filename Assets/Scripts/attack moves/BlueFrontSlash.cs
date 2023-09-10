using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BlueFrontSlash : PlayerMove
{
    public override void Attack() {
        base.Attack();
        Debug.Log("Performing front slash attack using the style method");
    }
}