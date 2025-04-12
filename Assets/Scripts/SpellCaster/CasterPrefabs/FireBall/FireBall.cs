using System.Collections;
using UnityEngine;

public class FireBall : MonoBehaviour
{
	public float _damage = 1.0f;

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
			StartCoroutine(Explode());
		}
	}

	private IEnumerator Explode()
	{
		_rb.linearVelocity = Vector2.zero;
		GetComponent<CircleCollider2D>().radius *= 4;

		yield return new WaitForSeconds(0.5f);
		Destroy(gameObject);
	}
}
