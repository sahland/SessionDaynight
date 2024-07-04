using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name;
    public Sprite icon;
    public bool isDefaultItem;

    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }

    public Item()
    {
        name = "New Item";
        icon = null;
        isDefaultItem = false;
    }
}
