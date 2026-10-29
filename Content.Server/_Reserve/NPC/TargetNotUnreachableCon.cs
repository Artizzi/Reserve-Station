// SPDX-FileCopyrightText: 2026 Reserve Station
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server.NPC.Queries.Considerations;

namespace Content.Server._Reserve.NPC;

/// <summary>
/// Возвращает 0, если NPC недавно не смог дойти до цели (см. <see cref="NPCUnreachableTargetsComponent"/>).
/// </summary>
public sealed partial class TargetNotUnreachableCon : UtilityConsideration
{
}
