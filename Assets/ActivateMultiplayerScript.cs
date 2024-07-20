using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMultiplayerScript : MonoBehaviour
{
    [SerializeField] GameObject Network1;
    [SerializeField] GameObject Network2;
    [SerializeField] GameObject Network3;
    [SerializeField] GameObject Network4;
    [SerializeField] GameObject Network5;
    [SerializeField] GameObject Network6;

    void Awake()
    {
        // Desactivar todos los GameObjects al iniciar la escena
        DeactivateAllNetworks();

        // Luego activar los necesarios según tu lógica
        // Por ejemplo, activar Network1 y Network2
        Network1.SetActive(true);
        Network2.SetActive(true);
    }

    void DeactivateAllNetworks()
    {
        Network1.SetActive(false);
        Network2.SetActive(false);
        Network3.SetActive(false);
        Network4.SetActive(false);
        Network5.SetActive(false);
        Network6.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
