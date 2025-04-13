using UnityEngine;

public class Sceleton2 : Enemy
{
	private void Start()
	{
		_player = FindFirstObjectByType<Player>().transform;
		_controller = GetComponent<Animator>();
		_rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		if (!_isStanned && !_isDead)
		{
			if (!_isRunning && !_isAttacking)
			{
				StartCoroutine(Move(_player.position));
			}
			_controller.SetFloat("Velocity", _rb.linearVelocityX * _rb.linearVelocityX + _rb.linearVelocityY * _rb.linearVelocityY);

			if (_rb.linearVelocityX > 0.02f) Flip(false);
			else if (_rb.linearVelocityX < -0.02f) Flip(true);
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.tag == "AttackSpell")
		{
			Stop();
			TakeDamage(collision.GetComponent<IDamage>().Damage());
			StartCoroutine(Stan(2f));
		}
		else if (collision.tag == "Player" && !_isAttacking)
		{
			Stop();
			StartAttack();
		}
	}
}
