using UnityEngine;

public class Boss : Enemy
{
	[SerializeField] private GameObject weapon;
	private float _time = 0;

	private void Start()
	{
		_player = FindFirstObjectByType<Player>().transform;
		_controller = GetComponent<Animator>();
		_rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		_time += Time.deltaTime;

		if (!_isStanned && !_isDead)
		{
			if(_time >= 6)
			{
				_time = 0;
				Attack();
			}

			if (!_isRunning && !_isAttacking)
			{
				StartCoroutine(Move(_player.position));
			}
			_controller.SetFloat("Velocity", _rb.linearVelocityX * _rb.linearVelocityX + _rb.linearVelocityY * _rb.linearVelocityY);

			if (_rb.linearVelocityX > 0.02f) Flip(false);
			else if (_rb.linearVelocityX < -0.02f) Flip(true);
		}

	}

	private void Teleport()
	{
		
	}

	private void Attack()
	{
		GameObject w = Instantiate(weapon, transform);
		w.f
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "AttackSpell" && !_isDamaged)
		{
			Stop();
			StartCoroutine(Damaged());
			TakeDamage(collision.GetComponent<IDamage>().Damage());
			StartCoroutine(Stan(0.1f));
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
