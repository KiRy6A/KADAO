using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	[SerializeField] public int _maxHp = 5;
	[SerializeField] public int _maxMana = 8;
	[SerializeField] public int _maxStamina = 5;

	[SerializeField] private int _hp = 5;
    [SerializeField] private int _mana = 8;
    [SerializeField] private float _stamina = 5;

    [SerializeField] private float _currentSpeed = 10f;
    [SerializeField] private float _defaultSpeed = 10f;
    [SerializeField] private float _runSpeed = 20f;

	private Rigidbody2D _rb;

    public Stats _stats;

	private bool _isDamaged = false;

	private float _x;
	private float _y;

	public int Hp { get { return _hp; } }
    public int Mana { get { return _mana; } set { _mana = value; } }
    public int Stamina { get { return (int)_stamina; } }
    public bool isRunning { get { return Input.GetKey(KeyCode.LeftShift); } }

    public Animator _animator;

    public int timer=30000;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _stats = FindAnyObjectByType<Stats>();

        if (!PlayerPrefs.HasKey("timer"))
        {
            PlayerPrefs.SetInt("timer", timer);
            PlayerPrefs.Save();
        }
        else
        {
            if (SceneManager.GetActiveScene().name == "LevelScene" && PlayerPrefs.GetInt("floorcounter") == 1)
            {
                PlayerPrefs.SetInt("timer", 30000);
                PlayerPrefs.Save();
            }
            else if (SceneManager.GetActiveScene().name == "Betweenlevels")
            {
                PlayerPrefs.SetInt("timer", PlayerPrefs.GetInt("timer")+300);
                PlayerPrefs.Save();
            }
        }
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

                _animator.SetFloat("x", _x + (_y * _y));
            }

    private void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(_x, _y).normalized * _currentSpeed;
        if (SceneManager.GetActiveScene().name == "LevelScene" || SceneManager.GetActiveScene().name == "BossFight")
            PlayerPrefs.SetInt("Timer", PlayerPrefs.GetInt("Timer") - 1);
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

        _animator.SetTrigger("Dead");
        yield return new WaitForSeconds(1);

		SceneManager.LoadScene("LevelScene");
	}

	private IEnumerator TakeDamage()
    {
        _isDamaged = true;
        yield return new WaitForSeconds(0.2f);
        _isDamaged = false;
    }
}