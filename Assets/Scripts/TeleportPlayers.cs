using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class TeleportPlayers : NetworkBehaviour
{
    // M�todo que ser� llamado para teletransportar a todos los jugadores a una posici�n espec�fica
    public void TeleportAllPlayers(Vector3 teleportPosition)
    {
        // Verifica si eres el host del servidor
        if (IsServer)
        {
            // Obt�n todos los objetos de red de los jugadores conectados
            foreach (var player in NetworkManager.Singleton.ConnectedClientsList)
            {
                // Obt�n el objeto de red del jugador
                NetworkObject playerNetworkObject = player.PlayerObject;

                // Verifica si el objeto de red del jugador existe y es v�lido
                if (playerNetworkObject != null)
                {
                    // Llama al m�todo RPC en el GameObject del jugador para teletransportarlo
                    playerNetworkObject.GetComponent<PlayerController>().TeleportServerRpc(teleportPosition);
                }
            }
        }
    }
}
