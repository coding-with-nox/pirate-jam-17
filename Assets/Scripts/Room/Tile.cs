using UnityEngine;

public class Tile : MonoBehaviour
{
	public bool isWall;
	[SerializeField]GameObject shadowS,shadowW,shadowN,shadowE;
	public void Setup(Sprite sprite, float posX, float posY){
		transform.localPosition = new Vector3(posX,posY,0.2f);
		
		GetComponent<SpriteRenderer>().sprite = sprite;
		isWall = true;
		
		
	}
	public void Setup(Sprite sprite, float posX, float posY,bool wallS,bool wallW,bool wallN,bool wallE){
		transform.localPosition = new Vector3(posX,posY,0.2f);
		
		GetComponent<SpriteRenderer>().sprite = sprite;
		GetComponent<Collider2D>().isTrigger = true;
		isWall = false;
		
		shadowS.SetActive(wallS);
		shadowW.SetActive(wallW);
		shadowN.SetActive(wallN);
		shadowE.SetActive(wallE);
	}
	
}
