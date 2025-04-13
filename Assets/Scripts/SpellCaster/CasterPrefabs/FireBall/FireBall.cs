using System.Collections;
using UnityEngine;

public class FireBall : MonoBehaviour, IDamage
{
	[SerializeField] private int _damage = 1;

	private bool _isExpleded = false;
	private Rigidbody2D _rb;

	private void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!_isExpleded && collision.tag != "Player")
		{
			_isExpleded = true;
			StartCoroutine(Explode());
		}
	}

	private IEnumerator Explode()
	{
			_rb.linearVelocity = Vector2.zero;
			GetComponent<CircleCollider2D>().radius *= 4;

			yield return new WaitForSeconds(0.2f);
			Destroy(gameObject);
	}

	public int Damage()
	{
		return _damage;
	}
}
