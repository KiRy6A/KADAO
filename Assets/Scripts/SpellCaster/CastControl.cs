using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CastControl : MonoBehaviour, IPointerDownHandler, /*IBeginDragHandler*/ IDragHandler, IPointerUpHandler
{
	[SerializeField] float END_INPUT = 3;
	const float READY_TO_INPUT = 0;
	public float _timer = 0;

	private Coroutine _coroutine;
	private PointerEventData _pointerEventData;
	private Camera _camera;

	private LineRenderer _castLine;
	private CircleCollider2D _castCollider;

	private CanvasGroup _allCanvasGroup;
	private CanvasGroup _canvasGroup;

	public CanvasScaler _canvasScaler;

	public bool isWriting
	{
		private set { _canvasGroup.blocksRaycasts = !value; _castCollider.enabled = value;}
		get { return !_canvasGroup.blocksRaycasts; }
	}

	private void Start()
	{
		_canvasScaler = GetComponentInParent<CanvasScaler>();
		_camera = Camera.main;
		_castLine = transform.GetChild(0).GetComponent<LineRenderer>();
		_castCollider = transform.GetChild(0).GetComponent<CircleCollider2D>();
		_allCanvasGroup = transform.parent.GetComponentInParent<CanvasGroup>();
		_canvasGroup = GetComponentInParent<CanvasGroup>();

		_castLine.transform.position = (Vector2)_camera.transform.position;
		_castLine.positionCount = 0;
		_castCollider.enabled = false;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		_allCanvasGroup.blocksRaycasts = false;
		_coroutine = StartCoroutine(CountTimeForAttack());
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (isWriting)
		{
			_castLine.positionCount += 1;
			Vector3 nextPos = (_camera.ScreenToWorldPoint(eventData.position)* _canvasScaler.scaleFactor - _camera.transform.position);
			nextPos.z = 0;
			_castCollider.offset = nextPos;
			_castLine.SetPosition(_castLine.positionCount - 1, nextPos);
		}
		else
		{
			OnPointerUp(eventData);
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		_castLine.positionCount = 0;
		isWriting = false;
		_allCanvasGroup.blocksRaycasts = true;
		StopCoroutine(_coroutine);
	}

	private IEnumerator CountTimeForAttack()
	{
		isWriting = true;
		_timer = READY_TO_INPUT;

		while (_timer < END_INPUT)
		{
			_timer += 0.1f;
			yield return new WaitForSeconds(0.1f);
		}

		isWriting = false;
		OnDrag(_pointerEventData);
	}
}