using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup cineTargetGroup;
    //Reference to main camera
     private Camera mainCamera;
    private List<Target> targets = new List<Target>();

    public Target CurrentTarget {get; private set;}

    private void Start() 
    {
        mainCamera = Camera.main;
    }
    

    private void OnTriggerEnter(Collider other) 
    {
        if(!other.TryGetComponent<Target>(out Target target)){return;}
        {
            targets.Add(target);
            target.OnDestroyed += RemoveTarget;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(!other.TryGetComponent<Target>(out Target target)){return;}
        {
            targets.Remove(target);

            RemoveTarget(target);
        }
    }

    public bool SelectTarget()
    {
        if (targets.Count == 0){return false;}

        Target closestTarget = null;
        float closestTargetDistance = Mathf.Infinity; //This sets it to largest number possible

        //Loop to check to see where object are in our camera
        foreach(Target target in targets)
        {
            Vector2 viewPos = mainCamera.WorldToViewportPoint(target.transform.position);

            //OR check if any of these are true we continue
            if(!target.GetComponentInChildren<Renderer>().isVisible)
            {
                continue;
            }

            //FIure out how far object is from the center
            Vector2 toCenter = viewPos - new Vector2(0.5f, 0.5f);//The closer they are the smaller this vector is
            //magnitude tells you how big a vector is
            if(toCenter.sqrMagnitude < closestTargetDistance)
            {
                closestTarget = target;
                closestTargetDistance = toCenter.sqrMagnitude;
            }
        }

        //if there are no targets on screen
        if(closestTarget == null){return false;}
        //if we do have a closest target
        CurrentTarget = closestTarget;
        cineTargetGroup.AddMember(CurrentTarget.transform, 1f, 2f);
        
        return true;
    }

    public void Cancel()
    {
        if(CurrentTarget == null) {return;}
        cineTargetGroup.RemoveMember(CurrentTarget.transform);
        CurrentTarget = null;
    }

    private void RemoveTarget(Target target)
    {
        if(CurrentTarget == target)
        {
            cineTargetGroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget = null;
        }

        target.OnDestroyed -= RemoveTarget;
        targets.Remove(target);
    }
}
