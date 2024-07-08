using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton

    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;
    public Transform targetHead;
    public Transform targetArm;

    private Equipment[] _currentEquipment;
    private GameObject[] _currentObjects;
    private Inventory _inventory;

    private void Start()
    {
        _inventory = Inventory.instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        _currentEquipment = new Equipment[numSlots];
        _currentObjects = new GameObject[numSlots];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        // Unequip the current item in the slot
        Unequip(slotIndex);

        // Set the new item in the equipment slot
        _currentEquipment[slotIndex] = newItem;

        // Create new object and add necessary components
        GameObject newObject = new GameObject(newItem.name);
        MeshFilter meshFilter = newObject.AddComponent<MeshFilter>();
        meshFilter.mesh = newItem.mesh;
        MeshRenderer meshRenderer = newObject.AddComponent<MeshRenderer>();
        meshRenderer.material = newItem.material;

        // Attach the object to the appropriate bone
        if (newItem.equipSlot == EquipmentSlot.Head)
        {
            newObject.transform.SetParent(targetHead);
        }
        else if (newItem.equipSlot == EquipmentSlot.Weapon) // or any other slot for arm
        {
            newObject.transform.SetParent(targetArm);
        }

        newObject.transform.localPosition = Vector3.zero;
        newObject.transform.localRotation = Quaternion.identity;

        // Store the new object in the slot
        _currentObjects[slotIndex] = newObject;

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, null);
        }

        Debug.Log($"Equipped new item: {newItem.name} in slot: {newItem.equipSlot}");
    }

    public void Unequip(int slotIndex)
    {
        if (_currentEquipment[slotIndex] != null)
        {
            if (_currentObjects[slotIndex] != null)
            {
                Destroy(_currentObjects[slotIndex]);
            }

            Equipment oldItem = _currentEquipment[slotIndex];
            _inventory.Add(oldItem);

            _currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }
    }

    public void UnequipAll()
    {
        for (int i = 0; i < _currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }
}
