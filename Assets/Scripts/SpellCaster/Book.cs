using SpellsSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Book : MonoBehaviour
{
	[SerializeField] public List<AbstaractSpellCaster> _allSpellCasters = new List<AbstaractSpellCaster>();

	private CastControl _castControl;
	private Transform _player;

	private int _iCurrentSpell = 0;

	private AbstaractSpellCaster _currentSpell;
	private GameObject _castingObj;

	private Coroutine _castingCoroutine;

	private bool _isCasted = false;
	private bool _isWaitingUp = false;

	private void Start()
	{
		_castControl = FindAnyObjectByType<CastControl>();
		_player = FindAnyObjectByType<Player>().transform;
	}

	private void Update()
	{
		if(Input.GetMouseButtonDown(0) && _castingObj == null && !_isCasted && _castControl.isWriting)
		{
			_currentSpell = _allSpellCasters[_iCurrentSpell];
			_castingObj = _currentSpell.CastSpell(_castControl.transform);
			_castingCoroutine = StartCoroutine(Casting());
		}

		if(Input.GetMouseButtonUp(0) && _castingObj != null && !_isCasted && !_isWaitingUp)
		{
			StopCasting();
			StopCoroutine(_castingCoroutine);
		}
	}

	private IEnumerator Casting()
	{
		CastOutline castingOutline = _castingObj.GetComponent<CastOutline>();
		yield return new WaitWhile(() => !castingOutline.IsCasted && !castingOutline.IsFailed && _castControl.isWriting);

		_isWaitingUp = true;
		yield return new WaitWhile(() => Input.GetMouseButtonUp(0) == false && _castControl.isWriting);
		
		if(castingOutline.IsCasted && !castingOutline.IsFailed)
		{
			//DELETE!!!
			FindAnyObjectByType<Player>().GetComponent<SpriteRenderer>().color = Color.red;
			//DELETE!!!^^^^^^^^

			_isCasted = true;
			_castControl.enabled = false;
			yield return new WaitWhile(() => Input.GetMouseButtonDown(0) != true);

			//DELETE!!!
			FindAnyObjectByType<Player>().GetComponent<SpriteRenderer>().color = Color.white;
			//DELETE!!!^^^^^^^

			_currentSpell.ThrowSpell(_player.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
			_castControl.enabled = true;
		}

		StopCasting();
	}

	private void StopCasting()
	{
		Destroy(_castingObj);
		_castingObj = null;
		_isCasted = false;
		_isWaitingUp = false;
	}

	public void NextSpell()
	{
		++_iCurrentSpell;

		if (_iCurrentSpell >= _allSpellCasters.Count)
		{
			_iCurrentSpell = 0;
		}

		UpdateSheet();
	}

	public void PrevSpell()
	{
		--_iCurrentSpell;

		if (_iCurrentSpell < 0)
		{
			_iCurrentSpell = _allSpellCasters.Count - 1;
		}

		UpdateSheet();
	}

	private void UpdateSheet()
	{
		_currentSpell = _allSpellCasters[_iCurrentSpell];

		GetComponentInChildren<RawImage>().texture = _currentSpell._templateCast.texture;
	}
}
