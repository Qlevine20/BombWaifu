public class PotatoBomb : Bomb
{
    protected override void ReturnBomb()
    {
        base.ReturnBomb();
        PotatoBombPool.Instance.ReturnToPool(this);
    }
}
