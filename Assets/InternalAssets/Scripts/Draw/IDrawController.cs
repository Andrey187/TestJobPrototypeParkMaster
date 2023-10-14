using System;

public interface IDrawController
{
    public int CountOfTries { get; }
    public bool CharacterMoving { get; set; }

    public event Action PathDrawingCompleted;
}
