using UnityEngine;

public class Vampire : Enemy
{
	[SerializeField] private float _jumpSpeed = 2.0f;

	private float _defaultSpeed;
	private float _time = 0;

	private void Start()
	{
		_player = FindFirstObjectByType<Player>().transform;
		_controller = GetComponent<Animator>();
		_rb = GetComponent<Rigidbody2D>();

		_defaultSpeed = _speed;
	}

	private void Update()
	{
		_time += Time.deltaTime;
		if(_time >= 5)
		{
			_time = 0;
			_speed = _jumpSpeed;
		}

		if (!_isStanned && !_isDead)
		{
			if (!_isRunning && !_isAttacking)
			{
				if (_speed == _jumpSpeed)
				{
					StartCoroutine(Move(_player.position));
				}
				else
				{
					StartCoroutine(Move(-1 * _player.position));
				}
			}
			_controller.SetFloat("Velocity", _rb.linearVelocityX * _rb.linearVelocityX + _rb.linearVelocityY * _rb.linearVelocityY);

			if (_rb.linearVelocityX > 0.02f) Flip(false);
			else if (_rb.linearVelocityX < -0.02f) Flip(true);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "AttackSpell" && !_isDamaged)
		{
			Stop();
			StartCoroutine(Damaged());
			TakeDamage(collision.GetComponent<IDamage>().Damage());
			StartCoroutine(Stan(2f));
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.tag == "Player" && !_isAttacking)
		{
			Stop();
			StartAttack();
		}
	}
}