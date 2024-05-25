public struct DamageInfo
{
    public int damage;

    public object owner;

    public DamageInfo(int damage, object owner)
    {
        this.damage = damage;
        this.owner = owner;
    }
}
