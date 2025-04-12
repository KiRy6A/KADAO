using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellsSystem
{
	public abstract class AbstaractSpellCaster : MonoBehaviour
	{
		[SerializeField] protected GameObject _outlinePrefab;
		[SerializeField] protected GameObject _spellPrefab;

		public abstract void ThrowSpell(Vector2 pos, Vector2 target);

		public virtual GameObject CastSpell(Transform parent)
		{
			return Instantiate(_outlinePrefab, parent);
		}
	}
}