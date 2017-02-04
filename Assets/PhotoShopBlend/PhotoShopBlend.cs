﻿using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PhotoShopBlend : MonoBehaviour {

	public Shader curShader;
	public Texture blendTexture;

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
			material.SetTexture("_BlendTex", blendTexture);

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
