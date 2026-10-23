## ThreatCheck
Modified version of [Matterpreter's](https://twitter.com/matterpreter) [DefenderCheck](https://github.com/matterpreter/DefenderCheck).

Takes a binary as input (either from a file on disk or a URL), splits it until it pinpoints that exact bytes that the target engine will flag on and prints them to the screen. This can be helpful when trying to identify the specific bad pieces of code in your tool/payload.

```text
C:\>ThreatCheck.exe --help
  -e, --engine    (Default: Defender) Scanning engine. Options: Defender, AMSI
  -f, --file      Analyze a file on disk
  -u, --url       Analyze a file from a URL
  -t, --type      File type to scan. Options: Bin, Script
  --help          Display this help screen.
  --version       Display version information.
```

### Example
```text
C:\Users\Rasta>ThreatCheck.exe -f Downloads\Grunt.bin -e AMSI
[+] Target file size: 31744 bytes
[+] Analyzing...
[!] Identified end of bad bytes at offset 0x6D7A
00000000   65 00 22 00 3A 00 22 00  7B 00 32 00 7D 00 22 00   e·"·:·"·{·2·}·"·
00000010   2C 00 22 00 74 00 6F 00  6B 00 65 00 6E 00 22 00   ,·"·t·o·k·e·n·"·
00000020   3A 00 7B 00 33 00 7D 00  7D 00 7D 00 00 43 7B 00   :·{·3·}·}·}··C{·
00000030   7B 00 22 00 73 00 74 00  61 00 74 00 75 00 73 00   {·"·s·t·a·t·u·s·
00000040   22 00 3A 00 22 00 7B 00  30 00 7D 00 22 00 2C 00   "·:·"·{·0·}·"·,·
00000050   22 00 6F 00 75 00 74 00  70 00 75 00 74 00 22 00   "·o·u·t·p·u·t·"·
00000060   3A 00 22 00 7B 00 31 00  7D 00 22 00 7D 00 7D 00   :·"·{·1·}·"·}·}·
00000070   00 80 B3 7B 00 7B 00 22  00 47 00 55 00 49 00 44   ·?³{·{·"·G·U·I·D
00000080   00 22 00 3A 00 22 00 7B  00 30 00 7D 00 22 00 2C   ·"·:·"·{·0·}·"·,
00000090   00 22 00 54 00 79 00 70  00 65 00 22 00 3A 00 7B   ·"·T·y·p·e·"·:·{
000000A0   00 31 00 7D 00 2C 00 22  00 4D 00 65 00 74 00 61   ·1·}·,·"·M·e·t·a
000000B0   00 22 00 3A 00 22 00 7B  00 32 00 7D 00 22 00 2C   ·"·:·"·{·2·}·"·,
000000C0   00 22 00 49 00 56 00 22  00 3A 00 22 00 7B 00 33   ·"·I·V·"·:·"·{·3
000000D0   00 7D 00 22 00 2C 00 22  00 45 00 6E 00 63 00 72   ·}·"·,·"·E·n·c·r
000000E0   00 79 00 70 00 74 00 65  00 64 00 4D 00 65 00 73   ·y·p·t·e·d·M·e·s
000000F0   00 73 00 61 00 67 00 65  00 22 00 3A 00 22 00 7B   ·s·a·g·e·"·:·"·{
```
```text
C:\Users\Rasta>ThreatCheck.exe -f Downloads\launcher.ps1 -e AMSI -t Script
[+] Target file size: 2988 bytes
[+] Analyzing...
[!] Identified end of bad bytes at offset 0x175
00000000   00 6C 00 79 00 2E 00 47  00 45 00 74 00 54 00 79   ·l·y·.·G·E·t·T·y
00000010   00 50 00 45 00 28 00 27  00 53 00 79 00 73 00 74   ·P·E·(·'·S·y·s·t
00000020   00 65 00 6D 00 2E 00 4D  00 61 00 6E 00 61 00 67   ·e·m·.·M·a·n·a·g
00000030   00 65 00 6D 00 65 00 6E  00 74 00 2E 00 41 00 75   ·e·m·e·n·t·.·A·u
00000040   00 74 00 6F 00 6D 00 61  00 74 00 69 00 6F 00 6E   ·t·o·m·a·t·i·o·n
00000050   00 2E 00 41 00 6D 00 73  00 69 00 27 00 2B 00 27   ·.·A·m·s·i·'·+·'
00000060   00 55 00 74 00 69 00 6C  00 73 00 27 00 29 00 3B   ·U·t·i·l·s·'·)·;
00000070   00 0D 00 0A 00 24 00 52  00 65 00 66 00 2E 00 47   ·····$·R·e·f·.·G
00000080   00 65 00 54 00 46 00 49  00 65 00 4C 00 64 00 28   ·e·T·F·I·e·L·d·(
00000090   00 27 00 61 00 6D 00 73  00 69 00 49 00 6E 00 69   ·'·a·m·s·i·I·n·i
000000A0   00 74 00 46 00 27 00 2B  00 27 00 61 00 69 00 6C   ·t·F·'·+·'·a·i·l
000000B0   00 65 00 64 00 27 00 2C  00 27 00 4E 00 6F 00 6E   ·e·d·'·,·'·N·o·n
000000C0   00 50 00 75 00 62 00 6C  00 69 00 63 00 2C 00 53   ·P·u·b·l·i·c·,·S
000000D0   00 74 00 61 00 74 00 69  00 63 00 27 00 29 00 2E   ·t·a·t·i·c·'·)·.
000000E0   00 53 00 65 00 74 00 56  00 61 00 6C 00 75 00 65   ·S·e·t·V·a·l·u·e
000000F0   00 28 00 24 00 4E 00 75  00 4C 00 6C 00 2C 00 24   ·(·$·N·u·L·l·,·$
```
