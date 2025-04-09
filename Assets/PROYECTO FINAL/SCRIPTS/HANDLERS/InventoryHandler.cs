using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    [SerializeField] private List<Item> inventario;
    public List<Item> _Inventario { get => inventario; }

    public int indice;
    public int maxCapacity = 24;
    public int numero;

    [Header("Configuraci�n de destrucci�n")]
    [SerializeField] private GameObject objetoADestruir;
    [SerializeField] private int itemsNecesarios = 8;

    private bool objetoDestruido = false; 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            TirarObjeto();
        }

        if (inventario.Count >= itemsNecesarios && objetoADestruir != null && !objetoDestruido)
        {
            Destroy(objetoADestruir);
            objetoDestruido = true; // Marca como destruido
            Debug.Log("Objeto destruido por haber recolectado " + itemsNecesarios + " items");
        }
    }

    public void AgregarObjeto(Item item)
    {
        if (inventario.Count < maxCapacity)
        {
            inventario.Add(item);
            Debug.Log("Item agregado. Total: " + inventario.Count);
        }
        else
        {
            Debug.LogWarning("Inventario lleno!");
        }
    }

    public void TirarObjeto()
    {
        if (inventario.Count > 0 && indice >= 0 && indice < inventario.Count)
        {
            Instantiate(inventario[indice]._prefab, transform.position, transform.rotation);
            inventario.RemoveAt(indice);
            Debug.Log("Item tirado. Total: " + inventario.Count);
        }
    }
}
