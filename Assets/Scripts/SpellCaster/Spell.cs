using UnityEngine;

namespace SpellsSystem
{
  public abstract class SpellCaster : MonoBehaviour
  {
    [SerializeField] protected GameObject _outlinePrefab;
    [SerializeField] protected GameObject _spellPrefab;

    public abstract void ThrowSpell(Vector2 pos, Vector2 target);

    public virtual void CastSpell(GameObject parent)
    {
      GameObject outline = Instantiate(_outlinePrefab, parent.transform);
    }
  }

  public class FireBallCaster : SpellCaster
  {
    public override void ThrowSpell(Vector2 pos, Vector2 target)
    {
      GameObject fireball = Instantiate(_spellPrefab, pos, Quaternion.FromToRotation(Vector2.right, target - pos));
    }
  }
}