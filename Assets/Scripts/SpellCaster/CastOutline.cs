using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CastOutline : MonoBehaviour
{
	private bool _isFailed = false;
	private bool _isCasted = false;
	private int _iPoints = 10;

	public void UpdateNumberOfPoints()
	{
		int i = 0;
		foreach (Transform g in GetComponentsInChildren<Transform>())
		{
			if(g.gameObject.activeInHierarchy) ++i;
		}
		_iPoints = i;
	}

	public bool IsFailed { get { return _isFailed; } }
	public bool IsCasted {  get { return _isCasted; } }

	private void Awake()
	{
		StartCoroutine(CountPoints());
	}

	private IEnumerator CountPoints()
	{
		yield return new WaitWhile(() => _iPoints >= 2);
		_isCasted = true;
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.tag == "Caster")
		{
			_isFailed = true;
		}
	}
}
