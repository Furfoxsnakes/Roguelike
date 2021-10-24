namespace Interfaces
{
    public class IBumpAction
    {
        public virtual bool DoBump(Actor bumpedActor)
        {
            return true;
        }
    }
}