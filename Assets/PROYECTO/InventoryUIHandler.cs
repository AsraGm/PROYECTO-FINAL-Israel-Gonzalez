using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIHandler : MonoBehaviour
{
    public GameObject inventoryPanel; // El objeto de la hierarchy que contiene el UI de el Inventario
    public GameObject uiItem; // El
                              // prefab de los objetos que se mostraran en el inventario. Contiene Imagen, Nombre y Descripcion del objeto

    public GameObject instanceDestination; // En donde se van a instanciar los items, para poderlos emparentar y que se acomoden segun el Layout Group
    public GameObject[] itemsInstanciados; // Aqui guardo los items instanciados para despues usarlos por pagina, que si del 0 al 7, del 8 al 15 y asi sucesivamente
    public int itemIndexCount = 0; // Llevo la cuenta de cuantos van instanciados, ademas me permite tener el indice de el ultimo item que instancie

    private InventoryHandler inventory; // Referencia al inventario
    public bool inventoryOpened = false; // Si tengo o no abierto el inventario


    public int actualPage = 0;
    public int maxPages = 3;

    private void Start()
    {
        // Consigo referencias
        inventory = FindObjectOfType<InventoryHandler>();
        itemsInstanciados = new GameObject[inventory.maxCapacity]; // Asigno el tamaño del arreglo a mi capacidad maxima de items
    }

    private void Update()
    {
        ToggleInventory();
    }

    private void ToggleInventory()
    {
        if (OpenInventoryInput())
        {
            inventoryOpened = !inventoryOpened;
            inventoryPanel.SetActive(inventoryOpened); // Activa y desactiva el panel de el canvas

            if (inventoryOpened)
            {
                UpdateInventory(); // En caso de que se este abriendo el inventario, lo actualiza, es decir, agrega los items nuevos que hayamos recogido
            }

        }

    }

    private void UpdateInventory()
    {
        for (int i = itemIndexCount; i < inventory._Inventario.Count; i++) // itemIndexCount = 5
        {
            GameObject newUiItem = Instantiate(uiItem); // Instancio el item
            newUiItem.transform.parent = instanceDestination.transform; // Lo emparento en el canvas para que se acomode con el layoutgroup
            newUiItem.GetComponent<UIItem>().SetItemInfo(inventory._Inventario[itemIndexCount]); // Le asigno la informacion consiguiendo el metodo SetInfo de el script UIItem que contiene el item del canvas
            newUiItem.transform.localScale = Vector3.one; // Le reseteo la escala a 1,1,1 por que a veces se escala de manera misteriosa
            itemsInstanciados[i] = newUiItem; // lo agrego a mi arreglo para tenerlo guardado para una futura ocasion

            if (itemIndexCount >= 8)
            {
                newUiItem.SetActive(false);
            }

            itemIndexCount++; // Aumento el indice de items instanciados
        }
    }

    public void NextPage() // Numero maximo de paginas es 3, es 0,1,2
    {
        actualPage++;

        if (actualPage >= 2) // If para revisar que no pases de el limite de paginas
        {
            actualPage = 2;
        }

        int endIndex = Mathf.Min((actualPage * 8) + 8, inventory.maxCapacity); // Obtienes hasta que objeto vas a activar

        for (int i = (actualPage - 1) * 8; i < endIndex - 8; i++) // desactivas los objetos de la pagina anterior
        {
            itemsInstanciados[i].SetActive(false);
        }

        for (int i = actualPage * 8; i < endIndex; i++) // activas los objetos de la nueva pagina
        {
            if (itemsInstanciados[i] != null)
                itemsInstanciados[i].SetActive(true);
            else
                Debug.Log("No existe el objeto " + i);
        }
    }

    public void PreviousPage() // Numero maximo de paginas es 3, es 0,1,2
    {
        actualPage--; // 2 > 1  // 1 > 0 // 0 > 0

        if (actualPage <= 0) // If para revisar que no pases de el limite de paginas
        {
            actualPage = 0;
        }
        int endIndex = Mathf.Min((actualPage * 8 + 8), inventory.maxCapacity); // Obtienes hasta que objeto vas a activar

        for (int i = (actualPage + 1) * 8; i < endIndex + 8; i++) // desactivas los objetos de la pagina siguiente
        {
            itemsInstanciados[i].SetActive(false);
        }

        for (int i = actualPage * 8; i < endIndex; i++) // activas los objetos de la nueva pagina
        {
            if (itemsInstanciados[i] != null)
                itemsInstanciados[i].SetActive(true);
            else
                Debug.Log("No existe el objeto " + i);
        }
    }


    private bool OpenInventoryInput()
    {
        return Input.GetKeyDown(KeyCode.I);
    }

}