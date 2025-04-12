using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CastOutline : MonoBehaviour
{
	private bool _isFailed = false;
	private bool _isCasted = false;
	private int _size = 0;

	public int _touchedPoints = 1;

	public bool IsFailed { get { return _isFailed; } }
	public bool IsCasted {  get { return _isCasted; } }

	private void Awake()
	{
		_size = transform.childCount;
		StartCoroutine(CountPoints());
	}

	private IEnumerator CountPoints()
	{
		yield return new WaitUntil(() => _touchedPoints >= _size);
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
