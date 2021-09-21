using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CryptRush.Core
{
    public class CameraVFX : MonoBehaviour
    {
		[SerializeField] Transform camTransform = null;

		public IEnumerator CameraShake(float duration, float shakeAmount)
		{
			Vector3 localPosition = camTransform.localPosition;

			float realDuration = duration;

			while (realDuration > 0)
			{
				camTransform.position = camTransform.position + Random.insideUnitSphere * shakeAmount;

				yield return new WaitForEndOfFrame();

				realDuration -= Time.deltaTime;
			}

			camTransform.localPosition = localPosition;
		}
	}
}