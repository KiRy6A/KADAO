using SpellsSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
	[SerializeField] public List<AbstaractSpellCaster> _allSpellCasters = new List<AbstaractSpellCaster>();

	private Transform _castControl;
	private Transform _player;

	private int _iCurrentSpell = 0;

	private AbstaractSpellCaster _currentSpell;
	private GameObject _castingObj;

	private Coroutine _castingCoroutine;

	private bool _isCasted = false;

	private void Start()
	{
		_castControl = FindAnyObjectByType<CastControl>().transform;
		_player = FindAnyObjectByType<Player>().transform;
	}

	private void Update()
	{
		if(Input.GetMouseButtonDown(0) && _castingObj == null && !_isCasted && _castControl.GetComponent<CastControl>().isWriting)
		{
			_currentSpell = _allSpellCasters[_iCurrentSpell];
			_castingObj = _currentSpell.CastSpell(_castControl);
			_castingCoroutine = StartCoroutine(Casting());
		}

		if(Input.GetMouseButtonUp(0) && _castingObj != null && !_isCasted)
		{
			StopCasting();
			StopCoroutine(_castingCoroutine);
		}
	}

	private IEnumerator Casting()
	{
		CastOutline castingOutline = _castingObj.GetComponent<CastOutline>();
		yield return new WaitWhile(() => !castingOutline.IsCasted && !castingOutline.IsFailed && _castControl.GetComponent<CastControl>().isWriting);
		//yield return new WaitWhile(() => Input.GetMouseButton(0));
		Debug.Log(castingOutline.IsCasted.ToString() + ' ' + castingOutline.IsFailed.ToString());
		
		if(castingOutline.IsFailed)
		{
			StopCasting();
			//Debug.Log("Fail");
		}
		else if(castingOutline.IsCasted)
		{
			_isCasted = true;
			_castControl.GetComponent<CastControl>().enabled = false;
			yield return new WaitWhile(() => Input.GetMouseButtonDown(0) != true);

			_currentSpell.ThrowSpell(_player.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
			_castControl.GetComponent<CastControl>().enabled = true;
			StopCasting();
			//Debug.Log("Throw");
		}
	}

	private void StopCasting()
	{
		Destroy(_castingObj);
		_castingObj = null;
		_isCasted = false;
	}
}
