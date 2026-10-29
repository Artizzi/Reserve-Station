// SPDX-FileCopyrightText: 2026 Reserve Station
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server.NPC.Queries.Considerations;

namespace Content.Server._Reserve.NPC;

/// <summary>
/// Возвращает 0, если к этой цели уже направляется другой NPC, который находится к ней ближе.
/// Нужно, чтобы несколько чистоботов не ехали к одной и той же луже.
/// </summary>
public sealed partial class TargetNotClaimedCon : UtilityConsideration
{
}
