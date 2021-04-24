using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{   
    public GameManager gameManager;
    public PlayerController player;
    public DialogueManager dialogueManager;
    public InteractionManager interactionManager;

    void Awake(){
        gameManager = GetComponent<GameManager>();
        dialogueManager = player.GetComponent<DialogueManager>();
        interactionManager = player.GetComponent<InteractionManager>();

        dialogueManager.playerManager = this;
        interactionManager.playerManager = this;
    }
}
