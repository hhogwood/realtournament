using UnityEngine;
using System.Collections;

public class CameraSelect : MonoBehaviour
{
    public PlayerInput playerInput;
    public float raycastDistance = 1000;

    public GameObject lastSelection;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Checking ray");
            RaycastHit hitInfo;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, raycastDistance))
            {
                Debug.Log("Hit " + hitInfo.collider.gameObject.name);

                IControllable target = hitInfo.collider.gameObject.GetComponent<IControllable>();
                //MonoBehaviour[] Behaviour = hitInfo.collider.gameObject.GetComponents<MonoBehaviour>();

                //IControllable target = null;

                //for(int i = 0; i < Behaviour.Length; ++i)
                //{
                //    if(Behaviour[i] is IControllable)
                //    {
                //        target = Behaviour[i] as IControllable;
                //        Debug.Log("Found target");
                //    }
                //}

                if(target != null)
                {
                    Debug.Log("Target controllable object found");

                    hitInfo.collider.gameObject.SendMessage("Selected", SendMessageOptions.DontRequireReceiver);

                    if(lastSelection)
                        lastSelection.SendMessage("Deselected", SendMessageOptions.DontRequireReceiver);

                    playerInput.SetTarget(target);
                    lastSelection = hitInfo.collider.gameObject;  
                }
            }
        }
    }
}
