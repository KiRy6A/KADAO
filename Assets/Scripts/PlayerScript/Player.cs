using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
  [SerializeField] private float _speed = 10f;
  Rigidbody2D rb;

  private float x;
  private float y;

  //[HideInInspector] public Animator animator;

  private void Start()
  {
    //animator = GetComponent<Animator>();
    rb = GetComponent<Rigidbody2D>();
  }

  private void Update()
  {
    x = Input.GetAxis("Horizontal");
    y = Input.GetAxis("Vertical");

    //animator.SetFloat("x", /*write source of data*/.x);
    //animator.SetFloat("y", /*write source of data*/.y);
  }

  private void FixedUpdate()
  {
    rb.linearVelocity = new Vector2 (x, y).normalized * _speed;
  }
}
