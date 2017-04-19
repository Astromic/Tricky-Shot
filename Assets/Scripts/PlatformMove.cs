using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformMove : MonoBehaviour
{
    public enum TypeMoving
    {
        vertical,
        left,
        right
    }

    public enum TypeMove
    {
        Forward,
        Learp
    }
    public bool turn = false;
    public FindPath path;
    public float speed = 2.0f;
    public TypeMove typeMove = TypeMove.Forward;

    [HideInInspector]
    public TypeMoving _moving;                                                  // Current direct platform moving
	Rigidbody rigid;
    IEnumerator<Transform> _currentPoints;
    Vector3 curenPosition;
    Vector3 angle;
    private float scaleY;
    void Start()
    {
        
    }

	void OnEnable()
	{
		_currentPoints = path.GetPath();
		_currentPoints.MoveNext();
		curenPosition = transform.position;
		scaleY = transform.localScale.y;
		transform.position = _currentPoints.Current.position;
	}

    void LateUpdate()
    {
        if (typeMove == TypeMove.Forward)
        {
			transform.position = Vector3.MoveTowards(transform.position, _currentPoints.Current.position, Time.deltaTime * speed);
        }
            
        else if (typeMove == TypeMove.Learp)
        {
			transform.position = Vector3.Lerp(transform.position, _currentPoints.Current.position, Time.deltaTime * speed);
        }

        if(turn)                                                           // xoay theo quy dao
        {
            if (curenPosition != _currentPoints.Current.position)
            {
                if(!path.loop)
                {
                    transform.localScale = new Vector3(transform.localScale.x, scaleY * path.direct, transform.localScale.x);
                }
                angle = SetAngle(gameObject, transform.position, _currentPoints.Current.position);
                curenPosition = _currentPoints.Current.position;
                transform.eulerAngles = angle;
            }
        } 
           
        var distance = (transform.position - _currentPoints.Current.position).magnitude;
        CheckTypeMoving();
        
        if (distance < 0.1f)
            _currentPoints.MoveNext();
    }

    // Check type move
    void CheckTypeMoving()
    {
        if (_currentPoints.Current.position.x == transform.position.x)
            _moving = TypeMoving.vertical;
        else if (_currentPoints.Current.position.x < transform.position.x)
            _moving = TypeMoving.left;
        else
            _moving = TypeMoving.right;
    }

    Vector3 SetAngle(GameObject target, Vector3 from, Vector3 to)
    {
        return new Vector3(target.transform.eulerAngles.x, target.transform.eulerAngles.y, 180 / Mathf.PI * (Mathf.Atan2(to.y - from.y, to.x - from.x)));
    }

    void LerpAngle(Vector3 angle, float speed)
    {
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, angle, speed);

    }
}
