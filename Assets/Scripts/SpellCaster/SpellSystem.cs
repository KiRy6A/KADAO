using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellsSystem
{
	public abstract class AbstaractSpellCaster : MonoBehaviour
	{
		[SerializeField] protected GameObject _outlinePrefab;
		[SerializeField] protected GameObject _spellPrefab;
		[SerializeField] public Sprite _templateCast;
		[SerializeField] public string _name;
		[SerializeField] public int _manaCost;
		[SerializeField] public string _description;
		public abstract void ThrowSpell(Vector2 pos, Vector2 target);

		public virtual GameObject CastSpell(Transform parent)
		{
			return Instantiate(_outlinePrefab, parent);
		}
	}
}