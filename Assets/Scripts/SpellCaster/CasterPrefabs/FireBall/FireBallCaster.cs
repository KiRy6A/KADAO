using SpellsSystem;
using UnityEngine;

namespace SpellsSystem
{
	public class FireBallCaster : AbstaractSpellCaster
	{
		public override void ThrowSpell(Vector2 pos, Vector2 target)
		{
			GameObject fireball = Instantiate(_spellPrefab, pos, Quaternion.FromToRotation(Vector2.right, target - pos));
		}
	}
}