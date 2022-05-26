using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler
{
    public Command GetCommand() {
        if (Input.GetButtonDown("Jump")) return new JumpCommand();
        if (Input.GetKeyDown(KeyCode.Mouse0)) return new BasicAttackCommand();

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction == Vector3.zero) return new NullCommand(); 
        if (Input.GetKey(KeyCode.LeftShift)) return new RunCommand(direction);
        
        return new MoveCommand(direction);
    }
}
