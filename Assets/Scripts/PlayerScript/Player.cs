using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private int _hp = 3;
    [SerializeField] private int _mana = 4;
    [SerializeField] private int _stamina = 5;
    [SerializeField] private float _speed = 10f;

    public int Hp { get { return _hp; } }
    public int Mana { get { return _mana; } }
    public int Stamina { get { return _stamina; } }

    private Rigidbody2D rb;

    private bool _isDamaged = false;

    private float _x;
    private float _y;

    //[HideInInspector] public Animator animator;

    private void Start()
    {
        //animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _x = Input.GetAxis("Horizontal");
        _y = Input.GetAxis("Vertical");

        //animator.SetFloat("x", /*write source of data*/.x);
        //animator.SetFloat("y", /*write source of data*/.y);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(_x, _y).normalized * _speed;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "EnemyAttack" && !_isDamaged) 
        { 
            StartCoroutine(TakeDamage());
        }
	}

	private IEnumerator TakeDamage()
    {
        _isDamaged = true;
        yield return new WaitForSeconds(0.2f);
        _isDamaged = false;
    }
}