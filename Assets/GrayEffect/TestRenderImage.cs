using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TestRenderImage : MonoBehaviour {

	public Shader curShader;
	public float grayScaleAmount = 1.0f;
	private Material curMaterial;

	// Use this for initialization
	void Start () {

		if(!SystemInfo.supportsImageEffects)
		{
			enabled = false;
			return;
		}

		if(!curShader || !curShader.isSupported)
		{
			enabled = false;
			return;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		grayScaleAmount = Mathf.Clamp(grayScaleAmount, 0.0f, 1.0f);
	}

	Material material
	{
		get
		{
			if(curMaterial == null)
			{
				curMaterial = new Material(curShader);
				curMaterial.hideFlags = HideFlags.HideAndDontSave;
			}

			return curMaterial;
		}
	}

	void OnRenderImage(RenderTexture source, RenderTexture dest)
	{
		if(curShader != null)
		{
			material.SetFloat("_LuminosityAmount", grayScaleAmount);
			Graphics.Blit(source, dest, material);
		}
		else
		{
			Graphics.Blit(source, dest);
		}
	}

	void OnDisable()
	{
		if(curMaterial)
		{
			DestroyImmediate(curMaterial);
		}
	}
}
