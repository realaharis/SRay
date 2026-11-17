# SRay 2.1 — Windows UI implementation

Base: PattN `my-releases`. Development branch: `feature/sray-windows-ui`.

## Confirmed design
- Windows 10/11 x64 first; WPF native UI, preserve PattN ServiceLib and connection engine.
- Matte charcoal background, restrained violet accent, compact desktop dashboard.
- Server List heading and tiny flag filter controls on the SAME horizontal row, aligned right.
- No search bar. Flag controls approximately 16px, 6px spacing, subtle selected state.
- Show only flags represented by loaded profiles. Globe = all; unknown country = unknown group.
- Filter selection must not change active connection. Preserve server selection and context actions.
- Never infer physical server location solely from profile label as verified fact; distinguish detected endpoint/egress location and fallback/unknown. Cache geolocation and perform async resolution.

## Initial integration points
- `v2rayN/v2rayN/Views/MainWindow.xaml`: WPF shell and window title.
- `v2rayN/v2rayN/Views/ProfilesView.xaml`: server list and header.
- `v2rayN/v2rayN/Views/ProfilesView.xaml.cs`: view integration.
- `v2rayN/ServiceLib`: existing profile data, selection, subscription, and core management.

## Delivery checkpoints
1. Native WPF visual shell, branding, and working existing controls.
2. Compact country filter bound to actual loaded profiles.
3. Dashboard bound to real connection state and traffic data.
4. Windows x64 GitHub Actions build; publish portable ZIP.
5. Test proxy, subscriptions, tray, shutdown and TUN behavior.

Do not ship a browser prototype as a native VPN client. Keep GPL notices and upstream attribution.
