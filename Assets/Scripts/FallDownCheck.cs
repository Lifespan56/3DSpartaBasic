using UnityEngine;

public class FallDownCheck : MonoBehaviour
{
    private LayerMask TargetLayerMask;

    void Start()
    {
        TargetLayerMask = LayerMask.NameToLayer("Player");
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer == TargetLayerMask)
        {
            collision.gameObject.transform.localPosition = new Vector3(0, 2, 0);
        }
        else
        {
            Destroy(collision.gameObject);
        }

    }

}

