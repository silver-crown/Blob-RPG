using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack : MonoBehaviour
{
    [SerializeField] private string moveName;
    [SerializeField] private int experience; 
    [SerializeField] private int level;
    [SerializeField] private int expToNextLevel;
    [SerializeField] private int damage;
    public string animationName;
    [SerializeField] private bool isUnlocked;
    [SerializeField] protected bool attacking;
    [SerializeField] public Collider2D[] hitboxes; 

    public virtual void Attack(){
        //activate hitboxes and do funky stuff

        Debug.Log("entered Attack function");
        attacking = true;
        //check for enemy collision
        foreach(Collider2D h in hitboxes){
            Collider2D[] cols = Physics2D.OverlapCircleAll(h.bounds.center, h.bounds.extents.x);
            foreach(Collider2D c in cols){
                if (c.TryGetComponent<Enemy>(out Enemy enemy)){
                    Debug.Log(c.name);
                    //instantiate a popup on the collider's center with the correct damage dealt
                    GameObject popUp = Instantiate(CombatManager.CM.blueDmgPopup, h.bounds.center, Quaternion.identity) as GameObject;
                    damagePopup dmgPopup = popUp.GetComponent<damagePopup>();
                    dmgPopup.setNumber(enemy.Hurt(damage));
                    //should wait a frame
                }

            }
        } 
    }

    private void OnDrawGizmosSelected() {
        foreach (Collider2D h in hitboxes){
            Gizmos.DrawWireSphere(h.bounds.center, h.bounds.extents.x); 
        }
    }
}
