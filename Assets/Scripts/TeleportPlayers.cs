using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class TeleportPlayers : NetworkBehaviour
{
    // Método que será llamado para teletransportar a todos los jugadores a una posición específica
    public void TeleportAllPlayers(Vector3 teleportPosition)
    {
        // Verifica si eres el host del servidor
        if (IsServer)
        {
            // Obtén todos los objetos de red de los jugadores conectados
            foreach (var player in NetworkManager.Singleton.ConnectedClientsList)
            {
                // Obtén el objeto de red del jugador
                NetworkObject playerNetworkObject = player.PlayerObject;

                // Verifica si el objeto de red del jugador existe y es válido
                if (playerNetworkObject != null)
                {
                    // Llama al método RPC en el GameObject del jugador para teletransportarlo
                    playerNetworkObject.GetComponent<PlayerController>().TeleportServerRpc(teleportPosition);
                }
            }
        }
    }
}
