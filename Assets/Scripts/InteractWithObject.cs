using UnityEngine;

/// <summary>
/// Monobehaviour class for interacting with the object in front of the player, if one is present.
/// </summary>
public class InteractWithObject : MonoBehaviour
{

    float gridLength;
    [SerializeField] private GameObject frontPoint;
    /// <summary> How long a button han been pressed for</summary>
    float pressTimer;
    /// <summary>The threshold for the press timer </summary>
    float pressTimerThreshold;
    private void Start() {

    }
    private void Update() {
        moveEyes();
    }
    bool IsObjectInteractable() {
        
        return false;
    }


   void moveEyes() {
        if (Input.GetKey(GameManager.GM.Upward)) {
            pressTimer += Time.deltaTime;
            if (pressTimer >= pressTimerThreshold) {
                frontPoint.transform.position += new Vector3(0.0f, gridLength, 0.0f);
            }
        }
        ///<summary> Moving Down on the map</summary>
        else if (Input.GetKey(GameManager.GM.Downward)) {
            pressTimer += Time.deltaTime;
            if (pressTimer >= pressTimerThreshold) {
                frontPoint.transform.position += new Vector3(0.0f, -gridLength, 0.0f);
            }
        }
        ///<summary> Moving Right on the map</summary>
        else if (Input.GetKey(GameManager.GM.Left)) {
            pressTimer += Time.deltaTime;
            if (pressTimer >= pressTimerThreshold) {
                frontPoint.transform.position += new Vector3(-gridLength, 0.0f, 0.0f);
            }
        }
        ///<summary> Moving Right on the map</summary>
        else if (Input.GetKey(GameManager.GM.Right)) {
            pressTimer += Time.deltaTime;
            if (pressTimer >= pressTimerThreshold) {
                frontPoint.transform.position += new Vector3(gridLength, 0.0f, 0.0f);
            }
        }
    }
}