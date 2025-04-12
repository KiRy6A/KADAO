using SpellsSystem;
using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
	[SerializeField] protected int _hp;
	[SerializeField] protected int _speed;
	[SerializeField] private float _randomMovement = 1.0f;

	protected bool _isRun = false;

	public void TakeDamage(int damage)
	{
		_hp -= damage;

		if(_hp <= 0)
		{
			StopAllCoroutines();
			StartCoroutine(Die());
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "AttackSpell")
		{
			TakeDamage(collision.GetComponent<IDamage>().Damage());
		}
	}

	public IEnumerator Move(Vector2 target)
	{
		_isRun = true;
		GetComponent<Rigidbody2D>().linearVelocity = (new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * _randomMovement + target - (Vector2)transform.position).normalized * _speed;

		yield return new WaitForSeconds(Random.Range(2, 4));

		_isRun = false;
		GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
	}

	public IEnumerator Die()
	{
		GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds(0.5f);
		Destroy(gameObject);
	}
}
