using UnityEngine;
using TMPro;
using UnityEditor.Experimental.GraphView;

public class InventoryInfoUI : MonoBehaviour
{
    //used in the village scene
    #region SERIALIZABLE
    [SerializeField] GameObject go_noMaterialSelectedUI;
    [SerializeField] GameObject go_materialSelectedUI;

    [SerializeField] TextMeshProUGUI txt_amountAvailable;
    [SerializeField] TextMeshProUGUI txt_price;
    [SerializeField] TextMeshProUGUI txt_materialName;
    [SerializeField] TextMeshProUGUI txt_materialInfo;

    [SerializeField] InventoryUI inventory;

    #endregion
    #region PRIVATE VARS
    bool _init = false;
    bool _isMaterialSelected = false;
    int _price = 0;
    int _availableAmount = 0;
    MaterialName _materialSelected;
    int Price
    {
        set
        {
            _price = value;
            txt_price.text = value.ToString();
        }
    }
    int AvailableAmount
    {
        set
        {
            _availableAmount = value;
            txt_amountAvailable.text = value.ToString();
        }
    }
    bool IsMaterialSelected
    {
        set
        {
            if (_init || _isMaterialSelected != value)
            {
                _isMaterialSelected = value;

                go_noMaterialSelectedUI.SetActive(!value);
                go_materialSelectedUI.SetActive(value);
            }
        }
    }
    MaterialName MaterialSelected
    {
        set
        {
            _materialSelected = value;
            txt_materialName.text = value.ToString();
            txt_materialInfo.text = GetMaterialInfo(value);
        }
    }
    #endregion
    #region PUBLIC VARS
    #endregion
    #region PRIVATE FUNCS
    private void Awake()
    {

    }
    private void OnEnable()//al inicio y al activar un objeto
    {
        _init = true;

        UpdateInfoUI();

        //A�adir callback sobre el material seleccionado en InventoryUI
        inventory.OnSelectedMaterial += OnSelectedMaterial;
        inventory.OnUnSelectedMaterial += OnUnSelectedMaterial;
    }

    private void UpdateInfoUI()
    {
        if (inventory.HasMaterialSelected)
            OnSelectedMaterial(inventory.SelectedMaterial);
        else
            OnUnSelectedMaterial();
    }

    private void OnDisable()
    {
        _init = false;
        inventory.OnSelectedMaterial -= OnSelectedMaterial;
    }

    private void OnSelectedMaterial(MaterialName material)
    {
        MaterialSelected = material;
        IsMaterialSelected = true;
        AvailableAmount = GameData.Inventory.GetAmount(material);
        Price = GameData.MaterialsPrices[material];
    }
    private void OnUnSelectedMaterial()
    {
        IsMaterialSelected = false;
    }

    string GetMaterialInfo(MaterialName material)
    {
        if (material == MaterialName.Obsidiana ||material == MaterialName.Hierro ||
            material == MaterialName.Carbon)
            return "Este material sirve para mejorar el pico y para su venta a cambio de monedas";
        else if (material == MaterialName.Cuarzo)
            return "Este material sirve para fabricar cascos y para su venta a cambio de monedas";
        else if (material == MaterialName.Bronce)
            return "Este material sirve para fabricar prendas y cascos, y para su venta a cambio de monedas";
        else if (material == MaterialName.RolloTela)
            return "Este material sirve para fabricar prendas y para su venta a cambio de monedas";
        else return "Este material se puede vender a cambio de monedas en la tienda";

    }   

    #endregion
}
