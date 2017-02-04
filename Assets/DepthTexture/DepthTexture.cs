using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class DepthTexture : MonoBehaviour {

	public Shader curShader;
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

		Camera.main.depthTextureMode = DepthTextureMode.Depth;
	}

	// Update is called once per frame
	void Update () {
		
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
