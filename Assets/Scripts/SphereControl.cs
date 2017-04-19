using UnityEngine;
using System.Collections;

public class SphereControl : MonoBehaviour {
	public AudioClip triggerBox;
	public AudioClip finishBox;
	public AudioSource audi;
	public Rigidbody rigid;
	public float force = 1;
	public LayerMask touchMask;
	BoxControl box;
	public GameObject m_SuccessParticle;
	public GameObject m_WaterParticle;

	public void Start () {
		rigid.isKinematic = true;
	}
	
	public void SetVelocity(Vector3 deriction)
	{
		rigid.isKinematic = false;
		rigid.velocity = deriction * force;
	}

	void OnCollisionEnter(Collision collision) {
		Debug.Log (collision.gameObject.name);
		if (collision.gameObject.tag == "BoxFinish") {
			PlayAudio (finishBox);
			box = collision.gameObject.GetComponent <BoxControl> ();
			if (box == null) {
				box = collision.gameObject.GetComponentInParent <BoxControl> ();
			}
			GameObject obj = Instantiate (m_SuccessParticle, box.gameObject.transform) as GameObject;
			obj.transform.localScale = Vector3.one;
			obj.transform.localPosition = Vector3.zero;
			obj.transform.localRotation = Quaternion.Euler (Vector3.zero);
			Destroy (obj, 3.0f);
			box.FinishBox ();
			gameObject.SetActive (false);
		} else if (collision.gameObject.tag == "BoxFrame") {
			box = collision.gameObject.GetComponentInParent <BoxControl> ();
			if (box != null) {
				box.ChangeColor (Color.red);
			}
			if (audi.clip != finishBox)
				PlayAudio (triggerBox);
		}
	}

	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.name == "waterFBX") {
			GameObject obj = Instantiate (m_WaterParticle, gameObject.transform.position, Quaternion.identity) as GameObject;
			Destroy (obj, 2.0f);
		}
	}

	void PlayAudio(AudioClip clip)
	{
		audi.clip = clip;
		audi.Play ();
	}

//	void OnBecameInvisible()
//	{
//		gameObject.SetActive (false);
//	}
		
}
