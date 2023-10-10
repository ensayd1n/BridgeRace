using UnityEngine;

public class BridgeController : MonoBehaviour
{
    public GameObject[] BridgeSteps;
    
    public GameObject BridgeEntrance;
    public GameObject BridgeOutput;

    [HideInInspector]
    public Transform TargetMoveTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TargetMoveTransform = BridgeOutput.transform;
        }
        else
        {
            TargetMoveTransform = BridgeEntrance.transform;
        }
    }
}
