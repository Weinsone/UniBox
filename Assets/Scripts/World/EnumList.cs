public enum Privileges
{
    admin,
    player,
    guest
}

public static class ControllerList
{
    public enum Controllers
    {
        mainPlayer
    }

    public static string Assign(Controllers controller) {
        switch (controller) {
            case Controllers.mainPlayer:
                return "Assistant";
            default:
                return null;
        }
    }
}

public static class BotBehaviorList {
    public enum Behaviors
    {
        aggressive, // типичное было
        passive, // трус
        follower // беспомощный трус
    }

    public static IBotBehavior Assign(IBot root, Behaviors behavior) {
        switch (behavior) {
            case Behaviors.aggressive:
                return new AggressiveBehavior(root);
            case Behaviors.passive:
                return new PassiveBehavior(root);
            case Behaviors.follower:
                return new FollowerBehavior(root);
            default:
                return null;
        }
    }
}