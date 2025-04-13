using SpellsSystem;
using UnityEngine;

public class PrayingCaster : AbstaractSpellCaster
{
	public override void ThrowSpell(Vector2 pos, Vector2 target)
	{
		Player player = FindFirstObjectByType<Player>();
		player.Mana += 3;

		if(player.Mana > player._maxMana) 
		{
			player.Mana = player._maxMana;
		}

		player._stats.UpdateMana();
	}
}
