using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    // Este m�todo ser� llamado remotamente para teletransportar al jugador
    [ServerRpc(RequireOwnership = false)]
    public void TeleportServerRpc(Vector3 newPosition)
    {
        // Verifica que el jugador tenga permiso para realizar este RPC
        if (!IsServer)
            return;

        // Actualiza la posici�n del GameObject del jugador en todos los clientes
        transform.position = newPosition;
    }
}
