using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridAnimator : MonoBehaviour {
    [SerializeField] private float scrollSpeedX = 0.0f;
    [SerializeField] private float scrollSpeedY = 0.0f;
	
	private void LateUpdate() {
        float offsetX = Time.time * scrollSpeedX;
        float offsetY = Time.time * scrollSpeedY;

        Vector2 offset = new Vector2(offsetX, offsetY);

        GetComponent<Renderer>().material.SetTextureOffset("_MainTex", offset);
	}
}
