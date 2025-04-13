using SpellsSystem;
using System.Collections;
using UnityEditor.Animations;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
	[SerializeField] protected int _hp;
	[SerializeField] protected float _speed;
	[SerializeField] private float _randomMovement = 1.0f;
	[SerializeField] protected float _attackRange = 1.0f;
	[SerializeField] private int _damage = 1;

	protected Animator _controller;
	protected Rigidbody2D _rb;

	protected bool _isAttacking = false;
	protected bool _isStanned = false;
	protected bool _isDead = false;
	protected bool _isRunning = false;

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
		StopAllCoroutines();
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
		_rb.linearVelocity = Vector2.zero;
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

		yield return new WaitForSeconds(Random.Range(0.7f, 2.0f));

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

	public IEnumerator Die()
	{
		foreach (Collider2D col in GetComponentsInChildren<Collider2D>()) 
		{
			col.enabled = false;
		}

		yield return new WaitForSeconds(5.0f);
		Destroy(gameObject);
	}
}
