using UnityEngine;
using System.Collections;
public class CameraShake : MonoBehaviour
{
	// How long the object should shake for.
	public float shakeDuration = 0f;
	private float currentShakeDuration = 0f;

	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;

	private bool m_isShaking = false;

	Vector3 originalPos;

	void OnEnable()
	{
		originalPos = transform.localPosition;
	}

	public void Shake()
    {
		if(m_isShaking) { return; }
		StartCoroutine("ShakeTransform");
	}

	IEnumerator ShakeTransform()
    {
		m_isShaking = true; // lock
		currentShakeDuration = shakeDuration;
		Vector3 targetPosition = Vector3.zero;
		while (currentShakeDuration > 0f)
        {
			targetPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			targetPosition.z = originalPos.z;
			transform.localPosition = targetPosition;
			currentShakeDuration -= decreaseFactor;
			yield return new WaitForSeconds(.01f);
		}
		transform.localPosition = originalPos;
		m_isShaking = false;
	}
}