using UnityEngine;

public class TriggerTransformController : MonoBehaviour
{
    [HideInInspector] public bool TransformLock;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject)
        {
            TransformLock = false;
            return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject)
        {
            TransformLock = true;
            return;
        }
    }
}
