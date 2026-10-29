// SPDX-FileCopyrightText: 2026 Reserve Station
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server.NPC;
using Content.Server.NPC.HTN;
using Content.Server.NPC.HTN.PrimitiveTasks;
using Robust.Shared.Timing;

namespace Content.Server._Reserve.NPC;

/// <summary>
/// Запоминает текущую цель как недоступную на заданное время.
/// Ставится в ветку, которая срабатывает, когда до цели не удалось проложить путь.
/// </summary>
public sealed partial class MarkTargetUnreachableOperator : HTNOperator
{
    [Dependency] private readonly IEntityManager _entManager = default!;
    [Dependency] private readonly IGameTiming _timing = default!;

    [DataField]
    public string TargetKey = "Target";

    [DataField]
    public TimeSpan Duration = TimeSpan.FromSeconds(30);

    private readonly List<EntityUid> _expired = new();

    public override void Startup(NPCBlackboard blackboard)
    {
        base.Startup(blackboard);

        if (!blackboard.TryGetValue<EntityUid>(TargetKey, out var target, _entManager))
            return;

        var owner = blackboard.GetValue<EntityUid>(NPCBlackboard.Owner);
        var comp = _entManager.EnsureComponent<NPCUnreachableTargetsComponent>(owner);
        var now = _timing.CurTime;

        // Чистим протухшие записи, чтобы словарь не рос бесконечно.
        _expired.Clear();
        foreach (var (uid, until) in comp.Targets)
        {
            if (until <= now || !_entManager.EntityExists(uid))
                _expired.Add(uid);
        }

        foreach (var uid in _expired)
        {
            comp.Targets.Remove(uid);
        }

        comp.Targets[target] = now + Duration;
        blackboard.Remove<EntityUid>(TargetKey);
    }

    public override HTNOperatorStatus Update(NPCBlackboard blackboard, float frameTime)
    {
        return HTNOperatorStatus.Finished;
    }
}
