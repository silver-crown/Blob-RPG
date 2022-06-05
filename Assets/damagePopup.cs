using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class damagePopup : MonoBehaviour
{
    [SerializeField] TextMeshPro txt;
    private float vanishingTimer;
    private Color fadeColor;

    private void Awake() {
        fadeColor = txt.color;    
    }
    public void setNumber(int damage){
              txt.SetText(damage.ToString());
    }
    private void Update() {
        //popup rises upwards
        float ySpeed = 1.0f;
        transform.position += new Vector3(0, ySpeed) * Time.deltaTime;
        //popup vanishes
        vanishingTimer -= Time.deltaTime;
        if(vanishingTimer < 0){
            float disappearSpeed = 3f;
            fadeColor.a -= disappearSpeed * Time.deltaTime;
            txt.color = fadeColor;
        }
        if(fadeColor.a < 0){
            Destroy(gameObject);
        }
    }
}
