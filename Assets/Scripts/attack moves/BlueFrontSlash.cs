using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BlueFrontSlash : ComboAttack
{
    
    public override void Attack(){
        Debug.Log("entered Attack function");
        attacking = true;

        foreach(CircleCollider2D h in hitboxes){
            Collider2D col = Physics2D.OverlapCircle(h.transform.position, h.radius);
            Debug.Log(col.name);
        } 
    }

}