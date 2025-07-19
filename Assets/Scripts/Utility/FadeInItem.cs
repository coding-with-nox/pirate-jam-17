using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FadeInItem : MonoBehaviour
{
	[SerializeField]Color initial,final;
	[SerializeField]float timeToFade;
	[SerializeField]bool gradual;
	public enum Mode {
		looping,
		inAndOut,
		selfDestroy,
		selfDeactivate
	}
	[SerializeField]Mode mode;
	bool reversed, setup;
	float timePassed;
	SpriteRenderer imgRend;
	Image imgUI;
	TMP_Text text;
	enum Target{
		empty,
		uiImg,
		sprite,
		text,
	}
	Target target;
    void Start()
    {
        
    }
	void SetColor(Color c){
		switch (target){
			case Target.uiImg:{
				imgUI.color = c;
				break;
			}
			case Target.sprite:{
				imgRend.color = c;
				break;
			}
			case Target.text:{
				text.color = c;
				break;
			}
			case Target.empty:{
				print ("Missing render target, cannot proceed - did you not read the other warning on startup?");
				break;
			}
		}
	}
	public void Setup(float time)
	{
		if (!setup){// this 'if pyramid' is both glorious and bad
			setup = true;
			imgRend = gameObject.GetComponent<SpriteRenderer>();
			target = Target.sprite;
			if (imgRend == null){
				imgUI = gameObject.GetComponent<Image>();
				target = Target.uiImg;
				if (imgUI == null){
					text = gameObject.GetComponent<TMP_Text>();
					target = Target.text;
					if (text == null){
						target = Target.empty;
						print("could not find a renderer for "+this+", aborting setup");
						return;
					}
				}
			}
			
		}
		reversed = false;
		timePassed = 0;
		SetColor(initial);
		timeToFade = time;
		gameObject.SetActive(true);
	}
	public void Setup(float time, Color initialColor)
	{
		initial = initialColor;
		if (final.a == 0){// if alpha is 0 (going for a fade in/out) it will be better to just move the alpha value instead of turning everything to white/black
			final = new Color (initialColor.r,initialColor.g,initialColor.b,0);
		}
		Setup(time);
	}
	//I don't think I've ever made a lazier overload
	//tbh quite proud of how lazy it is
	//can't use normal default cause color should never be null
	public void Setup(float time, Color initialColor, Color finalColor)
	{
		final = finalColor;
		Setup(time,initialColor);
	}
	public void Setup(float time, Color initialColor, Color finalColor, Mode newMode)
	{
		mode = newMode;
		Setup(time,initialColor,finalColor);
	}
    void Update()
    {
		bool selfDestruct = false;
		if (reversed)
		{
			timePassed -= Time.deltaTime;
			if (timePassed <= 0){
				timePassed = 0;
				if (mode == Mode.looping){
					reversed = false;
				}
				else if (mode == Mode.inAndOut){
					reversed = false;
					mode = Mode.selfDeactivate;
				}
				else 
				{
					selfDestruct = true;
				}
			}
			
		}
		else 
		{
			timePassed += Time.deltaTime;
			if (timePassed >= timeToFade){
				timePassed = timeToFade;
				if (mode == Mode.looping){
					reversed = true;
				}
				else if (mode == Mode.inAndOut){
					reversed = true;
					mode = Mode.selfDeactivate;
				}
				else 
				{
					selfDestruct = true;
				}
			}
		}
		if (gradual)
		{
			SetColor((initial*(1-(timePassed/timeToFade)))+(final*(timePassed/timeToFade)));
		}
		else 
		{
			if ((reversed||selfDestruct) && timePassed == timeToFade)
			{
				SetColor(final);
			}
			else if (!reversed && timePassed == 0){
				SetColor(initial);
			}
		}
		if (selfDestruct)
		{
			//Destroy(gameObject.GetComponent<FadeInItem>());
			if (mode == Mode.selfDestroy){
				Destroy(this);
			}
			else if (mode == Mode.selfDeactivate){
				gameObject.SetActive(false);
			}
		}
    }
	
}
