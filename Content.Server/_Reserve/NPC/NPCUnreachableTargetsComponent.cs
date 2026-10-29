// SPDX-FileCopyrightText: 2026 Reserve Station
//
// SPDX-License-Identifier: AGPL-3.0-or-later

namespace Content.Server._Reserve.NPC;

/// <summary>
/// Цели, до которых NPC не смог проложить путь. Пока запись не истекла, NPC их игнорирует,
/// чтобы не зацикливаться на недоступной цели (например, луже за стеной).
/// </summary>
[RegisterComponent]
public sealed partial class NPCUnreachableTargetsComponent : Component
{
    /// <summary>
    /// Цель -> время, до которого она считается недоступной.
    /// </summary>
    [ViewVariables]
    public Dictionary<EntityUid, TimeSpan> Targets = new();
}
