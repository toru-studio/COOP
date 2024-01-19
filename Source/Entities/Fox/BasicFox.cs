using COOP.Source.Entities.Fox;
using Godot;

public partial class BasicFox : Fox
{
    public override void _Ready()
    {
        base._Ready();
        base.NormalSpeed = 0.2f;
        base.BlindSpeed = -0.02f;
        base.FleeSpeed = -0.3f;
        base.State = FoxState.NORMAL;
        base.MaxElapsedBlindness = 3.0;
        base.RecoveryRate = 1.0;
        base.RecoverBlindLevel = 20.0;
    }
    public override void ShineOn(double strength)
    {
        base.BlindLevel += strength;
    }
}