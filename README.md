# SRE Conf 2026 — Legacy Order Service

Sample legacy .NET application used for the "Modernização de Aplicações
Legadas com GitHub Copilot" demo at SRE Conf 2026.

This codebase intentionally represents a typical legacy system:

- Pinned to an outdated serializer (`Newtonsoft.Json 9.0.1`) with known
  advisories — surfaced by **Dependabot**.
- Builds SQL queries via raw string concatenation
  (`OrderRepository.GetOrdersForCustomer`) — surfaced by **CodeQL**.
- Low test coverage, no automated validation of behavior.

Used live to demonstrate:

1. Visibility into operational/security risk (Security tab).
2. AI-assisted modernization and validation (GitHub Copilot).
3. Closing the loop with **Copilot Autofix**.
