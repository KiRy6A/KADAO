using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	[SerializeField] private int _maxHp = 5;
	[SerializeField] private int _maxMana = 8;
	[SerializeField] private int _maxStamina = 5;

	[SerializeField] private int _hp = 5;
    [SerializeField] private int _mana = 8;
    [SerializeField] private float _stamina = 5;

    [SerializeField] private float _currentSpeed = 10f;
    [SerializeField] private float _defaultSpeed = 10f;
    [SerializeField] private float _runSpeed = 20f;

	private Rigidbody2D _rb;
    private Stats _stats;

	private bool _isDamaged = false;

	private float _x;
	private float _y;

	public int Hp { get { return _hp; } }
    public int Mana { get { return _mana; } set { _mana = value; } }
    public int Stamina { get { return (int)_stamina; } }
    public bool isRunning { get { return Input.GetKey(KeyCode.LeftShift); } }

    //[HideInInspector] public Animator animator;

    private void Start()
    {
        //animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _stats = FindAnyObjectByType<Stats>();
    }

    private void Update()
    {
        if (isRunning && _stamina > 0f)
        {
			if (_stamina < 0.02f) _stamina = -3;
            
            _stamina -= Time.deltaTime * 1.7f;
			_currentSpeed = _runSpeed;
        }
        else 
        {
            if (_stamina < _maxStamina)
            {
                _stamina += Time.deltaTime;
            }

			_currentSpeed = _defaultSpeed;
        }

        _stats.UpdateStamina();

		_x = Input.GetAxis("Horizontal");
        _y = Input.GetAxis("Vertical");

        //animator.SetFloat("x", /*write source of data*/.x);
        //animator.SetFloat("y", /*write source of data*/.y);
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(_x, _y).normalized * _currentSpeed;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.tag == "EnemyAttack" && !_isDamaged)
        {
            _hp -= collision.GetComponentInParent<IDamage>().Damage();

            if (_hp <= 0)
            {
                _hp = 0;
                StartCoroutine(Die());
            }
            else
            {
                StartCoroutine(TakeDamage());
			}

			_stats.UpdateHp();
		}
	}

    private IEnumerator Die()
    {
        if (PlayerPrefs.HasKey("floorcounter"))
        {
            PlayerPrefs.SetInt("floorcounter", 1);
            PlayerPrefs.Save();
        }

        //_animator.SetTrigger("Dead");
        yield return new WaitForSeconds(5);

		SceneManager.LoadScene("MainMenu");
	}

	private IEnumerator TakeDamage()
    {
        _isDamaged = true;
        yield return new WaitForSeconds(0.2f);
        _isDamaged = false;
    }
}