using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum GameStare
{
	Normal,
	InstanceSphere,
	Move
}

public class GameManager : MonoBehaviour {
	public static int level;
	public GameStare state;
	public LayerMask touchMask;
	public LayerMask gamePlayMask;
	public SphereControl sphere;
	public BoxControl box;
	public float force;
	public float timeEffect = 900;
	public Material[] ballMats;
	public Material[] BoxMats;
	[Range(200, 400)]
	public int resolution = 10;
	public ParticleSystem particleSystem;
	private int currentResolution;
	private ParticleSystem.Particle[] points;

	RaycastHit playHit;
	RaycastHit touchHit;
	RaycastHit touchHitLast;
	Ray camRay;
	Vector3 startTouch;
	Vector3 endTouch;
	Vector3 velocity;
	public bool testMode = false;
	public int levelTest = 0;
	float increment;
	public static int shootTotal;

	GameObject map;
	void Awake()
	{
		Application.targetFrameRate = 60;
		//shootTotal = 0;
		if(testMode && levelTest > 0)
		{
			PlayerPrefs.SetInt ("LevelCurrent", levelTest);
		}
		InstanceMap ();
		AudioListener.pause = (PlayerPrefs.GetInt ("sound") == 0);
		AdsControl.Instance.RequestBannerTop ();
		AdsControl.Instance.ShowBanner ();
	}

	public void InstanceMap()
	{
		if(map != null)
		{
			DestroyImmediate (map);
		}
		sphere.audi.clip = null;
		print (PlayerPrefs.GetInt ("LevelCurrent"));
		map = Instantiate(Resources.Load("Map" + PlayerPrefs.GetInt ("LevelCurrent").ToString (), typeof(GameObject))) as GameObject;
		//map.GetComponentsInChildren<BoxCollider> () [0].gameObject.GetComponent<MeshRenderer> ().enabled = false;
		map.GetComponentInChildren<BoxControl> ().SetMaterial (BoxMats [0], BoxMats [1], BoxMats [2]);
//		Transform background = map.transform.FindChild ("Ground");
//		background.eulerAngles = new Vector3 (85.0f,0.0f,0.0f);
		box = FindObjectOfType <BoxControl> ();
		state = GameStare.Normal;
		particleSystem.gameObject.SetActive (false);
		particleSystem.gameObject.SetActive (true);
	}

	// Use this for initialization
	void Start () {
		shootTotal = 0;
		state = GameStare.Normal;
		CreatePoints ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown (0) && !WasAButton())
		{
			camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			if(Physics.Raycast (camRay, out playHit, 100, gamePlayMask))
			{
				if(Physics.Raycast (camRay, out touchHit, 100, touchMask))
				{
					InstanceSprece ();
					state = GameStare.InstanceSphere;
				}
				else
				{
					//Camera.main.GetComponent <MoveCamera>().enabled = true;
//					DisableSphere ();
					state = GameStare.Move;
				}

			}
		}
		else if (Input.GetMouseButton (0) && !WasAButton()) 
		{
			if (state == GameStare.InstanceSphere) {
				camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
				if(Physics.Raycast (camRay, out playHit, 100, gamePlayMask))
				{
					if(Physics.Raycast (camRay, out touchHit, 100, touchMask))
					{
						endTouch = touchHit.point;
					}
					else 
					{
						Vector3 deriction = sphere.transform.position - playHit.point;
						if(Physics.Raycast (startTouch - deriction.normalized * 50, deriction, out touchHitLast, 100, touchMask))
						{
							endTouch = touchHitLast.point;
						}
					}
				
				}
				MoveEffect ();
			}
			else if(state == GameStare.Move)
			{
				//MoveLevel ();
			}
		}
		else if(Input.GetMouseButtonUp (0) && !WasAButton())
		{
			if (state == GameStare.InstanceSphere) {
				AddForceSphere ();
				shootTotal++;
			}
			else if(state == GameStare.Move)
			{
				
			}
			state = GameStare.Normal;
		}
	}

	private bool WasAButton()
	{
		UnityEngine.EventSystems.EventSystem ct
		= UnityEngine.EventSystems.EventSystem.current;

		if (! ct.IsPointerOverGameObject() ) return false;
		if (! ct.currentSelectedGameObject ) return false;
		if (ct.currentSelectedGameObject.GetComponent <Button>() == null )
			return false;

		return true;
	}

	private void CreatePoints () {
		currentResolution = resolution;
		points = new ParticleSystem.Particle[resolution];
		increment = 0.7f / (resolution - 1);
		for(int i = 0; i < resolution; i++){
			float x = i * increment;
			points[i].position = new Vector3(x, 0f, 0f);
			points[i].color = new Color(x, x, x, 1/x);
		}
		points [100].size = 0.3f;
	}

	void UpdateParticle () {
		if (currentResolution != resolution || points == null) {
			CreatePoints();
		}
		float sizePoint = 0.1f - Mathf.Sqrt (velocity.magnitude)/timeEffect;
		float newTime = Mathf.Sqrt (velocity.magnitude) / timeEffect; 
		Vector2 deltaTouch = new Vector2 ((startTouch.x - endTouch.x) / 100, (startTouch.y - endTouch.y) / 100);
		points [0].position = new Vector3(endTouch.x, endTouch.y, endTouch.z - 0.1f);
		points [100].position = new Vector3(startTouch.x, startTouch.y, startTouch.z - 0.1f);
		for (int i = 1; i < resolution; i++) {
			if (i > 100) {
				points [i].position = new Vector3 (points [i - 1].position.x + velocity.x * newTime,
					points [i - 1].position.y + velocity.y * newTime + 0.5f * Physics.gravity.y * newTime * newTime,
					points [i - 1].position.z);
				velocity = new Vector3 (velocity.x, velocity.y + Physics.gravity.y * newTime, velocity.z);
				points [i].size = sizePoint;
			}
			else if(i < 100)
			{
				points [i].position = new Vector3 (points [i - 1].position.x + deltaTouch.x,
					points [i - 1].position.y + deltaTouch.y,
					points [i - 1].position.z);
				points [i].size = sizePoint;
			}

		}
		particleSystem.SetParticles(points, points.Length);
	}

	void InstanceSprece()
	{
		startTouch = touchHit.point;
		sphere.gameObject.SetActive (true);
		sphere.Start ();
		sphere.gameObject.transform.position = playHit.point;
		sphere.GetComponent<Renderer>().sharedMaterial = ballMats[Random.Range(0, ballMats.Length)];
		box.Start ();
		for (int i = 0; i < resolution; i++) {
			float x = i * increment;
			points[i].color = new Color(x, x, x, 1 - (float)i/resolution);
		}

	}

//	void DisableSphere()
//	{
//		sphere.gameObject.SetActive (false);
//	}

	void MoveLevel()
	{
		
	}

	void MoveEffect()
	{
		velocity = (startTouch - endTouch) * sphere.force;
		UpdateParticle ();
	}

	void AddForceSphere()
	{
		sphere.SetVelocity (startTouch - endTouch);
		for (int i = 0; i < resolution; i++) {
			points[i].color = new Color(1f, 0f, 0f, 1 - (float)i/resolution);
		}
		particleSystem.SetParticles(points, points.Length);
	}
}
