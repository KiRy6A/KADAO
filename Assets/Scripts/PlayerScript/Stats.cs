using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
	[SerializeField] private GameObject _hpPrefab;
	[SerializeField] private GameObject _manaPrefab;
	[SerializeField] private GameObject _staminaPrefab;

	[SerializeField] private Transform _hpSlot;
	[SerializeField] private Transform _manaSlot;
	[SerializeField] private Transform _staminaSlot;

	private List<GameObject> _hpList;
	private List<GameObject> _manaList;
	private List<GameObject> _staminaList;

	private Player _player;

	private void Start()
	{
		_player = FindAnyObjectByType<Player>();

		_hpList = new List<GameObject>(_player.Hp);
		_manaList = new List<GameObject>(_player.Mana);
		_staminaList = new List<GameObject>(_player.Stamina);

		for (int i = 0; i < _player.Hp; i++) 
		{
			_hpList.Add(Instantiate(_hpPrefab, _hpSlot));
		}

		for (int i = 0; i < _player.Mana; i++) 
		{
			_manaList.Add(Instantiate(_manaPrefab, _manaSlot)); 
		}

		for (int i = 0; i < _player.Stamina; i++) 
		{
			_staminaList.Add(Instantiate(_staminaPrefab, _staminaSlot));
		}
	}

	public void UpdateHp()
	{
		for (int i = 0; i < _hpList.Count; i++)
		{
			if (i < _player.Hp)
			{
				_hpList[i].GetComponent<Image>().enabled = true;
			}
			else
			{
				_hpList[i].GetComponent<Image>().enabled = false;
			}
		}
	}

	public void UpdateMana()
	{
		for (int i = 0; i < _manaList.Count; i++)
		{
			if (i < _player.Mana)
			{
				_manaList[i].GetComponent<Image>().enabled = true;
			}
			else
			{
				_manaList[i].GetComponent<Image>().enabled = false;
			}
		}
	}
	
	public void UpdateStamina()
	{
		for(int i = 0; i < _staminaList.Count; i++)
		{
			if (i < _player.Stamina)
			{
				_staminaList[i].GetComponent<Image>().enabled = true;
			}
			else
			{
				_staminaList[i].GetComponent<Image>().enabled = false;
			}
		}
	}
}		
