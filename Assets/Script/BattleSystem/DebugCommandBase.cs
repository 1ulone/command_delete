using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DebugCommandBase 
{
    private string cmdID;
    private string cmdDesc;
    private string cmdFormat;

    public string commandID { get { return cmdID; } }
    public string commandDesc { get { return cmdDesc; } }
    public string commandFormat { get { return cmdFormat; } }

    public DebugCommandBase(string id, string description, string format)
    {
        cmdID = id;
        cmdDesc = description;
        cmdFormat = format;
    }
}

public class DebugCommand : DebugCommandBase
{
    private Action cmd;

    public DebugCommand(string id, string description, string format, Action command) : base(id, description, format)
    {
        this.cmd = command;
    }

    public void Invoke()
    {
        cmd.Invoke();
    }
}

public class DebugCommand<T1> : DebugCommandBase
{
    private Action<T1> cmd;

    public DebugCommand(string id, string description, string format, Action<T1> command) : base(id, description, format)
    {
        this.cmd = command;
    }

    public void Invoke(T1 value)
    {
        cmd.Invoke(value);
    }
}
