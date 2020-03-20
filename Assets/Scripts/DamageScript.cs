using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageScript : MonoBehaviour
{
	private float MoveSpeed = 2.0f;
	private float AlphaSpeed = 2.0f;

	private TextMeshPro DamageText;
	private Color Alpha;

	public int Damage;

	Transform cameraPos;

    void Start()
    {
		DamageText = GetComponent<TextMeshPro>();
		DamageText.text = Damage.ToString();
		cameraPos = Camera.main.transform;

		Alpha = DamageText.color;
		Invoke("DestroyObject", 2f);
    }

    void Update()
    {
		transform.LookAt(transform.position + cameraPos.rotation * Vector3.forward, cameraPos.rotation * Vector3.up);

		this.transform.Translate(new Vector3(0f, MoveSpeed * Time.deltaTime, 0f));

		Alpha.a = Mathf.Lerp(Alpha.a, 0f, Time.deltaTime * AlphaSpeed);
		DamageText.color = Alpha;
    }

	private void DestroyObject()
	{
		Destroy(gameObject);
	}
}
