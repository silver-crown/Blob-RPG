using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMove : MonoBehaviour
{
    [SerializeField] private string moveName;
    [SerializeField] private int experience; 
    [SerializeField] private int level;
    [SerializeField] private int expToNextLevel;
    [SerializeField] private int damage;
    public string animationName;
    [SerializeField] private bool isUnlocked = false;
    public int unlocksAtLevel = 1;
    [SerializeField] protected bool attacking;
    [SerializeField] public Collider2D[] hitboxes;
    [SerializeField] public player2DController player;


    public virtual void Attack(){
        player = PlayerMoveManager.PMM.GetPlayer2D();
        transform.localScale = player.transform.localScale;
        //activate hitboxes and do funky stuff
        if (!player.attacking) {
            player.SetAnimationBools(animationName);
            player.attacking = true;
            Debug.Log("entered Attack function");
            //check for enemy collision
            foreach(Collider2D h in hitboxes){
                Collider2D[] cols = Physics2D.OverlapCircleAll(h.bounds.center, h.bounds.extents.x);
                foreach(Collider2D c in cols){
                    if (c.TryGetComponent<Enemy>(out Enemy enemy)){
                        Debug.Log(animationName + " connected with enemy");
                        //instantiate a popup on the collider's center with the correct damage dealt
                        GameObject popUp = Instantiate(CombatManager.CM.blueDmgPopup, c.bounds.center, Quaternion.identity) as GameObject;
                        damagePopup dmgPopup = popUp.GetComponent<damagePopup>();
                        dmgPopup.setNumber(enemy.Hurt(damage));
                        //should wait a frame
                    }
                }
            }
        }
    }
  /*  private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "2DEnemy") {
            Debug.Log("connected with enemy in ATTACK");
            foreach(Collider2D h in hitboxes) {
                GameObject popUp = Instantiate(CombatManager.CM.blueDmgPopup, h.bounds.center, Quaternion.identity) as GameObject;
                damagePopup dmgPopup = popUp.GetComponent<damagePopup>();
                dmgPopup.setNumber(other.GetComponent<Enemy>().Hurt(damage));
            }
        }
    }*/
    private void OnDrawGizmosSelected() {
        foreach (Collider2D h in hitboxes){
            Gizmos.DrawWireSphere(h.bounds.center, h.bounds.extents.x); 
        }
    }
}
