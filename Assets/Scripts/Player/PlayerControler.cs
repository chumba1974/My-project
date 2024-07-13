using BHS;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerControler", menuName = "InputController")]

public class PlayerController : InputController
{
    public override bool RetrieveJumpInput()
    {
        return Input.GetButtonDown("Jump");
    }

    public override float RetrieveMoveInput()
    {
        return Input.GetAxisRaw("Horizontal");
    }
}
