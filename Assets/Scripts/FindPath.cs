using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class FindPath : MonoBehaviour {

    public Transform[] ListPoint;
    public bool loop = false;
    [HideInInspector]
    public int direct = 1;

    void Awake()
    {
        ListPoint = transform.GetComponentsInChildren<Transform>();
    }
    public IEnumerator<Transform> GetPath()
    {
        if(ListPoint.Length < 3)
        {
            print("List point assign less than 2 element!!");
            yield break;
        }

        int index = 1;

        while (true)
        {
            yield return ListPoint[index];

            if (index <= 1)
                direct = 1;
            else if(index >= ListPoint.Length - 1)
            {
                if(loop)
                {
                    direct = -(ListPoint.Length - 2);
                }
                else
                {
                    direct = -1;
                }
            }

            index = index + direct;
        }
    }

    public void OnDrawGizmos()
    {
        ListPoint = gameObject.GetComponentsInChildren<Transform>();
        if (ListPoint.Length < 3)
            return;

        var listNotNull = ListPoint.Where(t => t != null).ToList();
        if (loop && ListPoint.Length > 3)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(listNotNull[1].position, listNotNull[ListPoint.Length-1].position);
        }
        for (int i = 2; i < listNotNull.Count; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(listNotNull[i - 1].position, listNotNull[i].position);
        }
            
    }
	    
}
