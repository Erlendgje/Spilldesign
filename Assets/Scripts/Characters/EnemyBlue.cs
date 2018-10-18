using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlue : Enemy
{

	bool detected = false;

	private void OnTriggerStay(Collider other)
	{
		if (!detected) {
			int layerMask = 1 << 9;
			layerMask = ~layerMask;

			RaycastHit hit;
			if (Physics.Raycast(transform.position, other.transform.position - transform.position, out hit, 10, layerMask)) {
				if (hit.transform.gameObject.CompareTag("Player")) {
					base.spotted = true;
					GameManager.gameManager.player.canMove(false);
					base.moving = false;
					GetComponent<DialogueHandler>().activate(true);
					detected = true;
				}
			}
		}
	}
}
