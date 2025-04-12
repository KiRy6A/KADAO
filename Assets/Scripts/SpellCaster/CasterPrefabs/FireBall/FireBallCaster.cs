using SpellsSystem;
using UnityEngine;

namespace SpellsSystem
{
	public class FireBallCaster : AbstaractSpellCaster
	{
		[SerializeField] private float _speed = 1.0f;
		public override void ThrowSpell(Vector2 pos, Vector2 target)
		{
			Instantiate(_spellPrefab, pos + (target - pos).normalized, Quaternion.FromToRotation(Vector2.right, target - pos)).GetComponent<Rigidbody2D>().AddForce((target - pos).normalized * _speed);
		}
	}
}