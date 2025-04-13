using UnityEngine;

public class IceSpear : MonoBehaviour, IDamage
{
	[SerializeField] private int _damage = 2;

	private Rigidbody2D _rb;

	private void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag != "Player" && collision.tag != "Enemy")
		{
			Destroy(gameObject);
		}
	}

	public int Damage()
	{
		return _damage;
	}
}
