using UnityEngine;

public class CasterPoint : MonoBehaviour
{
	private void OnTriggerStay2D(Collider2D collision)
	{
		if(collision.tag == "Caster")
		{
			GetComponentInParent<CastOutline>().UpdateNumberOfPoints();
			gameObject.SetActive(false);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Caster")
		{
			GetComponentInParent<CastOutline>().UpdateNumberOfPoints();
			gameObject.SetActive(false);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Caster")
		{
			GetComponentInParent<CastOutline>().UpdateNumberOfPoints();
			gameObject.SetActive(false);
		}
	}
}
