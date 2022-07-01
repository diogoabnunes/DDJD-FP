using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler
{
    public List<Command> GetCommands() {
        List<Command> commands = new List<Command>();

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction == Vector3.zero) commands.Add(new NullCommand()); 
        else if (Input.GetKey(KeyCode.LeftShift)) commands.Add(new RunCommand(direction));
        else commands.Add(new MoveCommand(direction));

        if (Input.GetButtonDown("Jump")) commands.Add(new JumpCommand());
        if (Input.GetButton("Normal Attack")) commands.Add(new LeftBasicAttackCommand());
        if (Input.GetButton("Alt Attack")) commands.Add(new RightBasicAttackCommand());
        if (Input.GetKeyDown(KeyCode.Q)) commands.Add(new Ability1Command());
        if (Input.GetKeyDown(KeyCode.E)) commands.Add(new Ability2Command());
        if (Input.GetKeyDown(KeyCode.R)) commands.Add(new SwapWeaponCommand());

        return commands;
    }

    public bool checkForBasicAttackButtonDown(){
        return Input.GetKey(KeyCode.Mouse0);
    }
}
