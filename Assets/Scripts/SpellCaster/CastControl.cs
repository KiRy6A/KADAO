using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class CastControl : MonoBehaviour, IPointerDownHandler, /*IBeginDragHandler*/ IDragHandler, IPointerUpHandler
{
	[SerializeField] float END_INPUT = 3;
	const float READY_TO_INPUT = 0;
	public float _timer = 0;

	private Coroutine _coroutine;
	private PointerEventData _pointerEventData;
	private LineRenderer _castLine;
	private Camera _camera;

	private CanvasGroup _allCanvasGroup;
	private CanvasGroup _canvasGroup;

	public bool isWriting
	{
		private set { _canvasGroup.blocksRaycasts = !value; }
		get { return !_canvasGroup.blocksRaycasts; }
	}

	private void Start()
	{
		_camera = Camera.main;
		_castLine = transform.GetChild(0).GetComponent<LineRenderer>();
		_allCanvasGroup = transform.parent.GetComponentInParent<CanvasGroup>();
		_canvasGroup = GetComponentInParent<CanvasGroup>();

		_castLine.positionCount = 0;
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
			Vector3 nextPos = _camera.ScreenToWorldPoint(eventData.position) - _camera.transform.position;
			nextPos.z = 0;
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