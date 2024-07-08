using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name;
    public Sprite icon;
    public bool isDefaultItem;

    public Item()
    {
        name = "New Item";
        icon = null;
        isDefaultItem = false;
    }

    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
