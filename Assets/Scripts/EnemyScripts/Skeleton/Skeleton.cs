using UnityEngine;

public class Skeleton : Enemy
{
	private Transform _player;

	private void Start()
	{
		_player = FindFirstObjectByType<Player>().transform;
	}

	private void Update()
	{
		if(!_isRun)
		{
			StartCoroutine(Move(_player.position));
		}
	}
}
