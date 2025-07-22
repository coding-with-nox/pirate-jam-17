using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "TileSet", menuName = "Scriptable Objects/TileSet")]
public class TileSet : ScriptableObject
{
    public Sprite [] tileList;
	public Sprite [] doorList;
	public Sprite [] chestList;
	public Sprite [] wallList;
	public Sprite [] clutterList;
}
