using Unity.VisualScripting;
using UnityEngine;

public class CastOutline : MonoBehaviour
{
	private bool _isFailed = false;
	public bool IsFailed { get { return _isFailed; } }

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.tag == "Caster")
		{
			Debug.Log("Falling cast");
			_isFailed = true;
		}
	}
}
