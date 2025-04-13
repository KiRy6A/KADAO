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

	private int _hp = 0;
	private int _mana = 0;
	private int _stamina = 0;

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
			++_hp;
		}

		for (int i = 0; i < _player.Mana; i++) 
		{
			_manaList.Add(Instantiate(_manaPrefab, _manaSlot)); 
			++_mana; 
		}

		for (int i = 0; i < _player.Stamina; i++) 
		{
			_staminaList.Add(Instantiate(_staminaPrefab, _staminaSlot));
			++_stamina; 
		}
	}

	public void UpdateHp()
	{
		if (_player.Hp >= 0)
		{
			if (_hp < _player.Hp)
			{
				if (_hp == 0)
				{
					_hp = 1; 
					_hpList[0].GetComponent<Image>().enabled = true;
				}

				for (int i = _hp - 1; i < _player.Hp; i++)
				{
					_hpList[i].GetComponent<Image>().enabled = true;
					++_hp;
				}
			}
			else if (_hp > _player.Hp)
			{
				for (int i = _hp - 1; i >= _player.Hp; i--)
				{
					_hpList[i].GetComponent<Image>().enabled = false;
					--_hp;
				}
			}
		}
	}

	public void UpdateMana()
	{
		if (_mana < _player.Mana)
		{
			if (_mana == 0)
			{
				_mana = 1;
				_manaList[0].GetComponent<Image>().enabled = true;
			}

			for (int i = _mana - 1; i < _player.Mana; i++)
			{
				_manaList[i].GetComponent<Image>().enabled = true;
				++_mana;
			}
		}
		else if (_mana > _player.Mana)
		{
			for (int i = _mana - 1; i >= _player.Mana; i--)
			{
				_manaList[i].GetComponent<Image>().enabled = false;
				--_mana;
			}
		}
	}
	
	public void UpdateStamina()
	{
		if (_player.Stamina >= 0)
		{
			if (_stamina < _player.Stamina)
			{
				if(_stamina == 0)
				{ 
					_stamina = 1;
					_staminaList[0].GetComponent<Image>().enabled = true;
				}

				for (int i = _stamina - 1; i < _player.Stamina; i++)
				{
					_staminaList[i].GetComponent<Image>().enabled = true;
					++_stamina;
				}
			}
			else if (_stamina > _player.Stamina)
			{
				for (int i = _stamina - 1; i >= _player.Stamina; i--)
				{
					if (i >= _staminaList.Count) break;
					Debug.Log(i);
					_staminaList[i].GetComponent<Image>().enabled = false;
					--_stamina;
				}
			}
		}
	}
}		
