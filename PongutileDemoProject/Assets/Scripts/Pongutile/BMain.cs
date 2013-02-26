using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PPageType
{
	None,
	TitlePage,
	InGamePage,
}

public class BMain : MonoBehaviour
{	
	public static BMain instance;
	
	public int scorePlayer1 = 0;
	public int scorePlayer2 = 0;
	
	private PPageType _currentPageType = PPageType.None;
	private PPage _currentPage = null;
	
	private FStage _stage;
	
	private void Start()
	{
		instance = this; 
		
		Go.defaultEaseType = EaseType.Linear;
		Go.duplicatePropertyRule = DuplicatePropertyRuleType.RemoveRunningProperty;
		
		//Time.timeScale = 0.1f;
		
		bool isIPad = SystemInfo.deviceModel.Contains("iPad");
		
		bool shouldSupportPortraitUpsideDown = isIPad; //only support portrait upside-down on iPad
		
		FutileParams fparams = new FutileParams(true,true,true,shouldSupportPortraitUpsideDown);
		
		fparams.AddResolutionLevel(960.0f,	2.0f,	2.0f,	"_Scale2"); //iPhone retina
		
		fparams.origin = new Vector2(0.5f,0.5f);
		
		Futile.instance.Init (fparams);
		
		Futile.atlasManager.LoadAtlas("Atlases/PongutileGameAtlas");		
		Futile.atlasManager.LoadFont("Imagine","ImagineFont"+Futile.resourceSuffix+".png", "Atlases/ImagineFont"+Futile.resourceSuffix);
		
		_stage = Futile.stage;
		
		GoToPage(PPageType.TitlePage);
		//GoToPage(PPageType.InGamePage);
	}

	public void GoToPage (PPageType pageType)
	{
		if(_currentPageType == pageType) return; //we're already on the same page, so don't bother doing anything
		
		PPage pageToCreate = null;
		
		if(pageType == PPageType.TitlePage)
		{
			pageToCreate = new PTitlePage();
		}
		else if (pageType == PPageType.InGamePage)
		{
			pageToCreate = new PInGamePage();
		}  

		
		if(pageToCreate != null) //destroy the old page and create a new one
		{
			_currentPageType = pageType;	
			
			if(_currentPage != null)
			{
				_stage.RemoveChild(_currentPage);
			}
			 
			_currentPage = pageToCreate;
			_stage.AddChild(_currentPage);
			_currentPage.Start();
		}
		
	}
	
	public void ResetScore ()
	{
		scorePlayer1 = scorePlayer2 = 0;
	}
}









