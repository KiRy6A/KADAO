using SpellsSystem;
using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamage
{
	[SerializeField] protected int _hp;
	[SerializeField] protected float _speed;
	[SerializeField] protected Vector2 _minMaxMovementTime;
	[SerializeField] protected float _randomMovement = 1.0f;
	[SerializeField] protected int _damage = 1;

	public int Damage { get { return _damage; } }

	protected private Transform _player;
	protected Animator _controller;
	protected Rigidbody2D _rb;

	[SerializeField] protected bool _isDamaged;
	[SerializeField] protected bool _isAttacking = false;
	[SerializeField] protected bool _isStanned = false;
	[SerializeField] protected bool _isDead = false;
	[SerializeField] protected bool _isRunning = false;

	public void TakeDamage(int damage)
	{
		_hp -= damage;

		if (_hp <= 0)
		{
			_controller.SetBool("Dead", true);
			_isDead = true;
			StartCoroutine(Die());
		}
		else
		{
			_controller.SetTrigger("Damaged");
		}
	}

	protected void Stop()
	{
		_controller.SetFloat("Velocity", 0);
		_rb.linearVelocity = Vector2.zero;

		_isAttacking = false;
		_isRunning = false;
	}

	protected void Flip(bool left)
	{
		if (left && transform.localScale.x > 0 ||
			!left && transform.localScale.x < 0)
		{
			transform.localScale *= Vector2.left + Vector2.up;
		}
	}

	protected void StartAttack()
	{
		_isAttacking = true;
		_controller.SetTrigger("Attack");
	}

	protected void SlashAttack()
	{
		GetComponentInChildren<CircleCollider2D>().enabled = true;
	}

	protected void StopAttack()
	{
		GetComponentInChildren<CircleCollider2D>().enabled = false;
		_isAttacking = false;
	}

	protected IEnumerator Move(Vector2 target)
	{
		_isRunning = true;
		_rb.linearVelocity = (new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * _randomMovement + target - (Vector2)transform.position).normalized * _speed;

		yield return new WaitForSeconds(Random.Range(_minMaxMovementTime.x, _minMaxMovementTime.y));
		_isRunning = false;
		_rb.linearVelocity = Vector2.zero;
	}

	protected IEnumerator Stan(float time)
	{
		_isStanned = true;
		_rb.linearVelocity = Vector2.zero;

		yield return new WaitForSeconds(time);
		_isStanned = false;
	}

	protected IEnumerator Damaged()
	{
		_isDamaged = true;
		yield return new WaitForSeconds(0.21f);
		_isDamaged = false;
	}

	public IEnumerator Die()
	{
		foreach (Collider2D col in GetComponentsInChildren<Collider2D>()) 
		{
			col.enabled = false;
		}

		yield return new WaitForSeconds(4.0f);
		Destroy(gameObject);
	}

	int IDamage.Damage()
	{
		return _damage;
	}
}
