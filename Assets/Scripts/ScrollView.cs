using UnityEngine;
using System.Collections;

[System.Serializable]
public class ScrollAxis
{
	public bool isAxis;
	public Vector2 deltaScroll;
	public float deltaAxis;
	public float deltaFix;
}

public class ScrollView : MonoBehaviour
{
	public enum ScrollViewType
	{
		fix,
		fixLerp,
		lerp
	}

	public ScrollViewType typeScroll = ScrollViewType.lerp;
	public Transform _transform;
	public ScrollAxis scrollX;
	public ScrollAxis scrollY;
	[Range (0, 1)]
	public float speedLerpEndTouch;
	[Range (0, 1)]
	public float speedLerpOnTouch;
	public float speedInputEndLimit = 1;
	public float deltaStartScroll = 1;
	public bool touchOn = false;
	bool scro = false;
	public Vector3 velocityInput = Vector3.zero;

	Vector3 velocity = Vector3.zero;
	public Vector3 targetPosition;
	delegate void  DrawTarget();
	DrawTarget drawTarget;
	DrawTarget drawEndTarget;
	void OnEnable()
	{
		SetDrawTarget ();
		SetDrawEndTarget ();
	}

	void OnDisable()
	{
		
	}

	void Update ()
	{
		Scroll ();
	}

	public void TouchStart()
	{
		touchOn = true;
		velocity = Vector3.zero;
	}

	public void TouchDraw(Vector3 velocityInp)
	{
		velocityInput = velocityInp;
	}

	[ContextMenu ("TouchEnd")]
	public void TouchEnd ()
	{
		touchOn = false;
		scro = false;
		if (typeScroll == ScrollViewType.fixLerp) {
			targetPosition = _transform.position;
			if (scrollX.isAxis) {
				if (velocity.x < -speedInputEndLimit) {
					targetPosition = new Vector3 (Mathf.Clamp (Mathf.FloorToInt ((_transform.localPosition.x - scrollX.deltaScroll.x) / scrollX.deltaFix) * scrollX.deltaFix + scrollX.deltaScroll.x, scrollX.deltaScroll.x, scrollX.deltaScroll.y), targetPosition.y, targetPosition.z);
				} else if (velocity.x > speedInputEndLimit) {
					targetPosition = new Vector3 (Mathf.Clamp (Mathf.CeilToInt ((_transform.localPosition.x - scrollX.deltaScroll.x) / scrollX.deltaFix) * scrollX.deltaFix + scrollX.deltaScroll.x, scrollX.deltaScroll.x, scrollX.deltaScroll.y), targetPosition.y, targetPosition.z);
				} else {
					targetPosition = new Vector3 (Mathf.Clamp (Mathf.RoundToInt ((_transform.localPosition.x - scrollX.deltaScroll.x) / scrollX.deltaFix) * scrollX.deltaFix + scrollX.deltaScroll.x, scrollX.deltaScroll.x, scrollX.deltaScroll.y), targetPosition.y, targetPosition.z);
				}
			}
			if (scrollY.isAxis) {
				if (velocity.y < -speedInputEndLimit) {
					targetPosition = new Vector3 (targetPosition.x, Mathf.Clamp (Mathf.FloorToInt ((_transform.localPosition.y - scrollY.deltaScroll.x) / scrollY.deltaFix) * scrollY.deltaFix + scrollY.deltaScroll.x, scrollY.deltaScroll.x, scrollY.deltaScroll.y), targetPosition.z);
				} else if (velocity.y > speedInputEndLimit) {
					targetPosition = new Vector3 (targetPosition.x, Mathf.Clamp (Mathf.CeilToInt ((_transform.localPosition.y - scrollY.deltaScroll.x) / scrollY.deltaFix) * scrollY.deltaFix + scrollY.deltaScroll.x, scrollY.deltaScroll.x, scrollY.deltaScroll.y), targetPosition.z);
				} else {
					targetPosition = new Vector3 (targetPosition.x, Mathf.Clamp (Mathf.RoundToInt ((_transform.localPosition.y - scrollY.deltaScroll.x) / scrollY.deltaFix) * scrollY.deltaFix + scrollY.deltaScroll.x, scrollY.deltaScroll.x, scrollY.deltaScroll.y), targetPosition.z);
				}
			}
		}
	}

	void Scroll ()
	{
		if (touchOn) {
			if(!scro)
			{
				bool scroX = Mathf.Abs (velocityInput.x) > deltaStartScroll;
				bool scroY = Mathf.Abs (velocityInput.y) > deltaStartScroll;
				if((scrollX.isAxis && scroX) || (scrollY.isAxis && scroY))
				{
					scro = true;
				}
			}
			else
			{
				velocity = Vector3.Lerp (velocity, velocityInput, speedLerpOnTouch);
				//			if (typeScroll == ScrollViewType.fix) {
				//				targetPosition = new Vector3 ((scrollX.isAxis) ? Mathf.Clamp (_transform.localPosition.x + velocity.x, scrollX.deltaScroll.x, scrollX.deltaScroll.y) : _transform.localPosition.x, (scrollY.isAxis) ? Mathf.Clamp (_transform.localPosition.y + velocity.y, scrollY.deltaScroll.x, scrollY.deltaScroll.y) : _transform.localPosition.y, _transform.localPosition.z);
				//			} else {
				//				targetPosition = new Vector3 ((scrollX.isAxis) ? Mathf.Clamp (_transform.localPosition.x + velocity.x, scrollX.deltaScroll.x - scrollX.deltaAxis, scrollX.deltaScroll.y + scrollX.deltaAxis) : _transform.localPosition.x, (scrollY.isAxis) ? Mathf.Clamp (_transform.localPosition.y + velocity.y, scrollY.deltaScroll.x - scrollY.deltaAxis, scrollY.deltaScroll.y + scrollY.deltaAxis) : _transform.localPosition.y, _transform.localPosition.z);
				//			}
				drawTarget();
				_transform.localPosition = Vector3.Lerp (_transform.localPosition, targetPosition, speedLerpOnTouch);
			}

		} else { 
//			if (typeScroll == ScrollViewType.fix) {
//				return;
//			} else if (typeScroll == ScrollViewType.fixLerp) {
//				if (scrollX.isAxis) {
//					if (_transform.localPosition.x > scrollX.deltaScroll.x && _transform.localPosition.x < scrollX.deltaScroll.y) {
//						_transform.localPosition = Vector3.Lerp (_transform.localPosition, targetPosition, speedLerpEndTouch);
//					} else {
//						_transform.localPosition = Vector3.Lerp (_transform.localPosition, new Vector3 (Mathf.Clamp (_transform.localPosition.x, scrollX.deltaScroll.x, scrollX.deltaScroll.y), _transform.localPosition.y, _transform.localPosition.z), speedLerpEndTouch);
//					}
//				}
//				if (scrollY.isAxis) {
//					if (_transform.localPosition.y > scrollY.deltaScroll.x && _transform.localPosition.y < scrollY.deltaScroll.y) {
//						transform.localPosition = Vector3.Lerp (transform.localPosition, targetPosition, speedLerpEndTouch);
//					} else {
//						_transform.localPosition = Vector3.Lerp (_transform.localPosition, new Vector3 (_transform.localPosition.x, Mathf.Clamp (_transform.localPosition.y, scrollY.deltaScroll.x, scrollY.deltaScroll.y), transform.localPosition.z), speedLerpEndTouch);
//					}
//				}
//			} else if (typeScroll == ScrollViewType.lerp) {
//				if (scrollX.isAxis) {
//					if (_transform.localPosition.x > scrollX.deltaScroll.x && _transform.localPosition.x < scrollX.deltaScroll.y) {
//						velocity = new Vector3 (Mathf.Lerp (velocity.x, 0, speedLerpEndTouch), velocity.y, velocity.z);
//						_transform.localPosition = new Vector3 (Mathf.Clamp (_transform.localPosition.x + velocity.x, scrollX.deltaScroll.x, scrollX.deltaScroll.y), _transform.localPosition.y, _transform.localPosition.z);
//					} else {
//						_transform.localPosition = Vector3.Lerp (_transform.localPosition, new Vector3 (Mathf.Clamp (_transform.localPosition.x, scrollX.deltaScroll.x, scrollX.deltaScroll.y), _transform.localPosition.y, _transform.localPosition.z), speedLerpEndTouch);
//					}
//				}
//				if (scrollY.isAxis) {
//					if (_transform.localPosition.y > scrollY.deltaScroll.x && _transform.localPosition.y < scrollY.deltaScroll.y) {
//						velocity = new Vector3 (velocity.x, Mathf.Lerp (velocity.y, 0, speedLerpEndTouch), velocity.z);
//						_transform.localPosition = new Vector3 (_transform.localPosition.x, Mathf.Clamp (_transform.localPosition.y + velocity.y, scrollY.deltaScroll.x, scrollY.deltaScroll.y), _transform.localPosition.z);
//					} else {
//						_transform.localPosition = Vector3.Lerp (_transform.localPosition, new Vector3 (_transform.localPosition.x, Mathf.Clamp (_transform.localPosition.y, scrollY.deltaScroll.x, scrollY.deltaScroll.y), _transform.localPosition.z), speedLerpEndTouch);
//					}
//				}
//			}
			drawEndTarget();
		}
	}

	void SetDrawTarget()
	{
		if(scrollX.isAxis && scrollY.isAxis)
		{
			if(typeScroll == ScrollViewType.fix)
			{
				drawTarget = XYDrawFix;
			}
			else
			{
				drawTarget = XYDrawNofix;
			}
		}
		else if(!scrollX.isAxis && scrollY.isAxis)
		{
			if(typeScroll == ScrollViewType.fix)
			{
				drawTarget = YDrawFix;
			}
			else
			{
				drawTarget = YDrawNoFix;
			}
		}
		else if(scrollX.isAxis && !scrollY.isAxis)
		{
			if(typeScroll == ScrollViewType.fix)
			{
				drawTarget = XDrawFix;
			}
			else
			{
				drawTarget = XDrawNoFix;
			}
		}
		else if(!scrollX.isAxis && !scrollY.isAxis)
		{
			drawTarget = NoneDraw;
		}
	}

	// DrawTarget
	#region    
	void NoneDraw()
	{}

	void XDrawFix()
	{
		targetPosition = new Vector3 (Mathf.Clamp (_transform.localPosition.x + velocity.x, scrollX.deltaScroll.x, scrollX.deltaScroll.y), _transform.localPosition.y, _transform.localPosition.z);
	}

	void YDrawFix()
	{
		targetPosition = new Vector3 (_transform.localPosition.x, Mathf.Clamp (_transform.localPosition.y + velocity.y, scrollY.deltaScroll.x, scrollY.deltaScroll.y), _transform.localPosition.z);
	}

	void XYDrawFix()
	{
		targetPosition = new Vector3 (Mathf.Clamp (_transform.localPosition.x + velocity.x, scrollX.deltaScroll.x, scrollX.deltaScroll.y), Mathf.Clamp (_transform.localPosition.y + velocity.y, scrollY.deltaScroll.x, scrollY.deltaScroll.y), _transform.localPosition.z);
	}

	void XDrawNoFix()
	{
		targetPosition = new Vector3 (Mathf.Clamp (_transform.localPosition.x + velocity.x, scrollX.deltaScroll.x - scrollX.deltaAxis, scrollX.deltaScroll.y + scrollX.deltaAxis),_transform.localPosition.y, _transform.localPosition.z);
	}

	void YDrawNoFix()
	{
		targetPosition = new Vector3 (_transform.localPosition.x, Mathf.Clamp (_transform.localPosition.y + velocity.y, scrollY.deltaScroll.x - scrollY.deltaAxis, scrollY.deltaScroll.y + scrollY.deltaAxis), _transform.localPosition.z);
	}

	void XYDrawNofix()
	{
		targetPosition = new Vector3 (Mathf.Clamp (_transform.localPosition.x + velocity.x, scrollX.deltaScroll.x - scrollX.deltaAxis, scrollX.deltaScroll.y + scrollX.deltaAxis), Mathf.Clamp (_transform.localPosition.y + velocity.y, scrollY.deltaScroll.x - scrollY.deltaAxis, scrollY.deltaScroll.y + scrollY.deltaAxis), _transform.localPosition.z);
	}
	#endregion

	void SetDrawEndTarget()
	{
		if(scrollX.isAxis && scrollY.isAxis)
		{
			if (typeScroll == ScrollViewType.fix) {
				drawEndTarget = NoneDraw;
			} else if (typeScroll == ScrollViewType.fixLerp) {
				drawEndTarget = UpTouchFixLerpXY;
			} else if (typeScroll == ScrollViewType.lerp)
			{
				drawEndTarget = UpTouchLerpXY;
			}
		}
		else if(!scrollX.isAxis && scrollY.isAxis)
		{
			if (typeScroll == ScrollViewType.fix) {
				drawEndTarget = NoneDraw;
			} else if (typeScroll == ScrollViewType.fixLerp) {
				drawEndTarget = UpTouchFixLerpY;
			} else if (typeScroll == ScrollViewType.lerp)
			{
				drawEndTarget = UpTouchLerpY;
			}
		}
		else if(scrollX.isAxis && !scrollY.isAxis)
		{
			if (typeScroll == ScrollViewType.fix) {
				drawEndTarget = NoneDraw;
			} else if (typeScroll == ScrollViewType.fixLerp) {
				drawEndTarget = UpTouchFixLerpX;
			} else if (typeScroll == ScrollViewType.lerp)
			{
				drawEndTarget = UpTouchLerpX;
			}
		}
		else if(!scrollX.isAxis && !scrollY.isAxis)
		{
			drawEndTarget = NoneDraw;
		}
	}

	void UpTouchFixLerpX()
	{
		if(_transform.localPosition.x < scrollX.deltaScroll.x || _transform.localPosition.x > scrollX.deltaScroll.y)
		{
			targetPosition = new Vector3 (Mathf.Clamp (targetPosition.x, scrollX.deltaScroll.x, scrollX.deltaScroll.y), targetPosition.y, targetPosition.z);
		}
		_transform.localPosition = new Vector3 (Mathf.Lerp (_transform.localPosition.x, targetPosition.x, speedLerpEndTouch), _transform.localPosition.y, _transform.localPosition.z);
	}

	void UpTouchFixLerpY()
	{
		if (_transform.localPosition.y < scrollY.deltaScroll.x && _transform.localPosition.y > scrollY.deltaScroll.y)
		{
			targetPosition = new Vector3 (targetPosition.x, Mathf.Clamp (targetPosition.y, scrollY.deltaScroll.x, scrollY.deltaScroll.y), targetPosition.z);
		}
		_transform.localPosition = new Vector3 ( _transform.localPosition.x, Mathf.Lerp (_transform.localPosition.y, targetPosition.y, speedLerpEndTouch), _transform.localPosition.z);
	}

	void UpTouchFixLerpXY()
	{
		UpTouchFixLerpX ();
		UpTouchFixLerpY ();
	}

	void UpTouchLerpX()
	{
		velocity = new Vector3 (Mathf.Lerp (velocity.x, 0, speedLerpEndTouch), velocity.y, velocity.z);
		targetPosition = new Vector3 (Mathf.Clamp (targetPosition.x + velocity.x, scrollX.deltaScroll.x - scrollX.deltaAxis, scrollX.deltaScroll.y + scrollX.deltaAxis), targetPosition.y, targetPosition.z);
		UpTouchFixLerpX ();
	}

	void UpTouchLerpY()
	{
		velocity = new Vector3 (velocity.x, Mathf.Lerp (velocity.y, 0, speedLerpEndTouch), velocity.z);
		targetPosition = new Vector3 (targetPosition.x, Mathf.Clamp (targetPosition.y + velocity.y, scrollY.deltaScroll.x - scrollY.deltaAxis, scrollY.deltaScroll.y + scrollY.deltaAxis), targetPosition.z);
		UpTouchFixLerpY ();
	}

	void UpTouchLerpXY()
	{
		UpTouchLerpX ();
		UpTouchLerpY ();
	}
}
