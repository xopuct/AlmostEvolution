using Entitas;
public static class IndexExtensions
{
    public static void AddEntityIndexes(this Contexts contexts)
    {
        //Main weapon index
        contexts.game.fieldIndex = new PrimaryEntityIndex<GameEntity, Vector2i>(
            "EntityByPositionType",
            contexts.game.GetGroup(Matcher<GameEntity>.AllOf(GameMatcher.Position)),
            (e, c) =>
            {
                var posComp = c is PositionComponent ? (PositionComponent)c : e.position;//. e.hasPosition ? (Vector2i)e.position : new Vector2i(-1, -1);
                return (Vector2i)posComp;
            }
        );
        contexts.game.AddEntityIndex(contexts.game.fieldIndex);

    }

    public static GameEntity GetCellByPosition(this GameContext context, Vector2i position)
    {
        return context.fieldIndex.GetEntity(position);
    }

    public static bool IsFree(this GameContext context, Vector2i position)
    {
        if(context.ValidateCoord(position))
            return context.fieldIndex.GetEntity(position) == null;
        return false;
    }

    public static bool Move(this GameContext context, GameEntity entity, Vector2i position)
    {
        if ( context.IsFree(position))
        {
            entity.ReplacePosition(position.x, position.y);
            return true;
        }
        return false;
    }
    static bool ValidateCoord(this GameContext context, Vector2i position)
    {
        return position.x >= 0 && position.x < context.fieldWidth && position.y >= 0 && position.y < context.fieldHeight;
    }
}

public partial class GameContext
{
    public PrimaryEntityIndex<GameEntity, Vector2i> fieldIndex;
    internal float fieldWidth = 100;
    internal float fieldHeight = 60;
}
