using System;

public class TrackingEnemyFsm : EnemyFsm
{
    // Pass the 'enemy' parameter to the base class constructor to fix CS7036
    public TrackingEnemyFsm(Enemy enemy) : base(enemy)
    {
        // Constructor logic (if any) goes here
        
    }

    public void update()
    {
        // Update the FSM
        base.Update();
    }
}
