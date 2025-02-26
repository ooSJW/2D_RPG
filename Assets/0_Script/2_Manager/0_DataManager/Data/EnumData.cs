using UnityEngine;

public enum SceneName
{
    None,
    InitializeScene,
    LoadingScene,
    LobbyScene,
    GoblinScene,
    GoblineSkeletonScene,
    SkeletonScene,
    SkeletonMushroomScene,
    MushroomScene,
}
public enum PlayerState
{
    Idle,
    Move,
    Jump,
    Attack,
    GetDamage,
    Death,
}
public enum EnemyName
{
    Goblin,
    Skeleton,
    Mushroom,
}
public enum EnemyState
{
    Idle,
    Move,
    Attack,
    Death,
}