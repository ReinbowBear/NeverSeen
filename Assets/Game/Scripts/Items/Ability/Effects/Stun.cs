
public class Stun : Effect
{
    public override void DoEffect(Entity character)
    {
        character.manna.StopCoroutine(character.manna.coroutine);
    }

    public override void FalseEffect(Entity character)
    {
        character.manna.TakeManna(0);
    }
}
