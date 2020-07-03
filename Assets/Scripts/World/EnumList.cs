public enum Privileges
{
    admin,
    player,
    guest
}

public static class BotBehaviorList {
    public enum Behaviors
    {
        aggressive, // типичное было
        passive, // трус
        follower // беспомощный трус
    }

    public static IBotBehavior Assign(Behaviors behavior) {
        switch (behavior) {
            case Behaviors.aggressive:
                return new AggressiveBehavior();
            case Behaviors.passive:
                return new PassiveBehavior();
            case Behaviors.follower:
                return null;
            default:
                return null;
        }
    }
}