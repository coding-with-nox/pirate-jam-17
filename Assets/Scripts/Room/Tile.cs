using UnityEngine;

public class Tile : MonoBehaviour
{
	public void Setup(Sprite sprite, bool isWall, float posX, float posY){
		transform.localPosition = new Vector3(posX,posY,0);
		GetComponent<SpriteRenderer>().sprite = sprite;
		GetComponent<Collider2D>().isTrigger = !isWall;
	}
}
