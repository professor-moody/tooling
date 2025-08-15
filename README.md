
# Offensive Security Tools Collection

A comprehensive, automated build pipeline for essential Windows offensive security tools. Fresh builds delivered weekly from official sources.

## Tools Included

### Active Directory & Azure
- **[PingCastle](https://github.com/vletoux/pingcastle)** - Active Directory security assessment and auditing
- **[ADRecon](https://github.com/adrecon/ADRecon)** - Active Directory reconnaissance with Excel reporting
- **[AzureADRecon](https://github.com/adrecon/AzureADRecon)** - Azure AD/Entra ID reconnaissance tool
- **[Adalanche](https://github.com/lkarlslund/Adalanche)** - Active Directory ACL visualizer and explorer
- **[SharpHound](https://github.com/BloodHoundAD/SharpHound)** - BloodHound data collector for AD analysis

### Kerberos & Authentication
- **[Rubeus](https://github.com/GhostPack/Rubeus)** - Kerberos interaction and abuse toolkit
- **[Mimikatz](https://github.com/gentilkiwi/mimikatz)** - Windows credential extraction utility

### Certificate Services
- **[Certify](https://github.com/GhostPack/Certify)** - AD CS enumeration and abuse (C#)
- **[Certipy](https://github.com/ly4k/Certipy)** - AD CS enumeration and abuse (Python)
- **[Whisker](https://github.com/eladshamir/Whisker)** - Shadow credentials manipulation

### Privilege Escalation
- **[Seatbelt](https://github.com/GhostPack/Seatbelt)** - Security enumeration for privilege escalation
- **[SharpUp](https://github.com/GhostPack/SharpUp)** - Windows privilege escalation checks
- **[WinPEAS](https://github.com/carlospolop/PEASS-ng/tree/master)** - Windows privilege escalation awesome scripts
- **[Crassus](https://github.com/vu-ls/Crassus)** - Windows privilege escalation discovery

### Lateral Movement & Exploitation
- **[SharpWSUS](https://github.com/nettitude/SharpWSUS)** - WSUS lateral movement tool
- **[PowerMad](https://github.com/Kevin-Robertson/Powermad)** - MachineAccountQuota and DNS exploitation
- **[Inveigh](https://github.com/Kevin-Robertson/Inveigh)** - .NET IPv4/IPv6 MITM platform

### Information Gathering
- **[Snaffler](https://github.com/SnaffCon/Snaffler)** - Network share enumeration and sensitive data discovery
- **[LaZagne](https://github.com/AlessandroZ/LaZagne)** - Local password and credential recovery

### Execution & Evasion
- **[Stracciatella](https://github.com/mgeeky/Stracciatella)** - OpSec-safe PowerShell runspace from C#

## Start Here!

Download the latest release from the [Releases](../../releases) page. Each release contains pre-compiled binaries for all tools.

## FAQ

### Why automate compilation?
Compiling offensive security tools from source is time-consuming and requires various build dependencies. Our automated pipeline saves hours of setup time, letting you focus on what matters - the assessment.

### Are PowerShell scripts included?
Yes! Each release bundles everything - compiled binaries and PowerShell scripts - giving you a complete toolkit ready for deployment.

### Did you put malicious stuff in these?
Nope - that's the point. 

### What versions are compiled?
Always the latest. Our pipeline pulls the most recent commit from each tool's official repository. Check commit messages for exact version references.

### How often are releases updated?
Weekly automated releases ensure you always have access to the latest features and bug fixes.

### Missing your favorite tool?
Open an issue! We're always looking to expand our collection with community-recommended tools.

## License

Each tool maintains its original license. Please review individual tool repositories for specific licensing terms.

## Disclaimer

These tools are for authorized security assessments only. Users are responsible for compliance with all applicable laws and regulations.

---

*Automated with ❤️ for the security community*
