# SRay 2.1 Windows development checkpoint

Native WPF changes on feature/sray-windows-ui; upstream ServiceLib connection engine is retained.

- Dark/DeepPurple defaults, existing theme preferences retained.
- Server List header with embedded 16px flags, 6px spacing, horizontal overflow; search control removed in WPF only.
- Country grouping uses cached egress IP test results first, then an explicit emoji flag in the profile name. Labels are unverified; names and hostnames are never geolocated by guesswork. Unknown profiles have a separate group.
- Existing async IP testing and profile cache supply measured country data; no automatic endpoint geolocation or new external API requests added.
- Filter changes only the WPF collection view. Selected IDs are remembered across filter switches; hidden rows cannot be targets of visible selection actions. No connection activation command is issued by filtering.
- Windows x64 CI builds, runs country checks and uploads a self-contained application artifact. Core binaries are not bundled by this workflow; upstream packaging remains necessary for a complete portable VPN distribution.

Validation on 2026-11-17: local Release win-x64 build succeeded (zero errors; existing ReactiveUI binding warnings). Twelve country grouping checks passed. A direct STA test using the actual ProfilesView/DataGrid passed multi-selection restoration across country switches and explicit deselection. GitHub Actions run 44031971083 also built and published successfully. GUI launch did not expose a targetable window in this environment; visual layout, selection behavior during live subscriptions, tray, proxy, TUN and live connections are not yet runtime-verified. This checkpoint is not a validated working VPN release.
