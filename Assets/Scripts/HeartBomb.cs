public class HeartBomb : Bomb
{
    protected override void ReturnBomb()
    {
        base.ReturnBomb();
        HeartBombPool.Instance.ReturnToPool(this);
    }
}
