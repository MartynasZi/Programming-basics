using Unity.VisualScripting;
using UnityEngine;

public class ForwardRayInteractor : MonoBehaviour
{
    //For picking up objects.
    
    [Header("Ray settings")]
    [SerializeField] float range = 2f;
    [SerializeField] LayerMask hitMask = ~0;         
    [SerializeField] Color gizmoColor = Color.cyan;

    private GameplayManager gM;

    private void Start()
    {
        gM = gameObject.GetComponentInParent<GameplayManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, range, hitMask, QueryTriggerInteraction.Collide))
            {
                if (hit.collider.CompareTag("ControlRod") && !gM.isHoldingRod)
                {
                    PickupRod(hit);
                }

                if(hit.collider.CompareTag("Reactor"))
                {
                    DeliverRod();
                }
            }
        }
    }

    void PickupRod(RaycastHit hit)
    {
        gM.PickupRod();
        Destroy(hit.transform.gameObject);
    }

    void DeliverRod()
    {
        gM.DeliverRod();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawRay(transform.position, transform.forward * range);

        // Optional: show hit point while in play mode
        if (Application.isPlaying &&
            Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, range, hitMask, QueryTriggerInteraction.Ignore))
        {
            Gizmos.DrawSphere(hit.point, 0.05f);
        }
    }
}
