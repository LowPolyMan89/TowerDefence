using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerTargetSelector : MonoBehaviour
{

    public Target GetClosesTarget(float maxDist, List<Target> targets, Transform _currentTransform)
    {
        if (targets.Count < 1) return null;
        List<Target> listtosort = CalculateMobsDistance(_currentTransform, targets);
        Target t = listtosort.OrderBy(x => x.Distance).First();
        if (t.Distance <= maxDist) return t;
        else return null;

    }

    public List<Target> CalculateMobsDistance(Transform _currentTransform, List<Target> targets)
    {
        if (targets.Count < 1) return null;

        targets.ForEach(x => {
            if(x != null) 
                 x.Distance = Vector3.Distance(x.Mob.gameObject.transform.position, _currentTransform.position);
        });

        return targets;
    }
 
}
