# AGENTS.md

This file provides guidance to agents when working with code in this repository.

## Stack

- **Language / Framework**: C# (.NET 10, `net10.0-windows7.0`), Windows Forms (used only for Clipboard access)
- **No NuGet dependencies** — everything is in BCL (`System.Windows.Forms`, `System.Text.Json`, `System.Text.RegularExpressions`)
- **No test project** exists in the solution; there is no test runner or lint toolchain configured

## Build & Run Commands

```powershell
# Build
dotnet build MdToOffice/MdToOffice.csproj

# Run (default — reads markdown from Windows Clipboard, writes result back to Clipboard)
dotnet run --project MdToOffice/MdToOffice.csproj

# Run with FIAP profile (applies fiap.css + FIAP RemoveTag config in addition to "all")
dotnet run --project MdToOffice/MdToOffice.csproj -- FIAP
```

There are no lint, format, or test commands — none are configured.

## Architecture & Data Flow

1. **Input**: Reads markdown text from the Windows Clipboard (`Clipboard.GetText`)
2. **Convert**: `MarkDown.ConverterParaHtml()` in [`MdToOffice/MarkDown.cs`](MdToOffice/MarkDown.cs) converts markdown to HTML fragments
3. **Wrap**: `Program.cs` wraps the fragment in a full HTML document with an inline CSS stylesheet
4. **Post-process**: `SubstituirTagsHtml()` applies find-replace rules from `Config/ConfigFile.json` to massage the HTML for Word compatibility
5. **Output**: `CopiarHtmlParaClipboard()` formats the HTML using the CF_HTML clipboard format spec (byte-offset header) and writes it to the Clipboard; also writes `Temp/index.html` for debugging

## Critical Non-Obvious Details

- **`[STAThread]` is mandatory** on `Main` — Windows Forms Clipboard APIs require an STA COM apartment; removing it silently breaks clipboard writes
- **Runtime assets must be present alongside the binary** — `Config/ConfigFile.json`, `Style/*.css`, and `Temp/index.html` are copied to the output directory (`CopyToOutputDirectory: Always`). When running via `dotnet run`, the working directory is the project root, so these paths resolve correctly. If you copy the binary elsewhere, copy those directories too.
- **CF_HTML byte-offset header**: `CriarClipboardHtml()` builds the `Version:0.9 / StartHTML / EndHTML / StartFragment / EndFragment` header manually with 10-digit zero-padded offsets. Offsets are **byte positions in the final UTF-8 string**, computed from `string.Length` (assumes ASCII/Latin-1 content — would be wrong for multibyte characters).
- **`Temp/index.html` is overwritten on every run** — it holds the last rendered output and is tracked by git (not gitignored); it's useful for debugging Word paste issues.
- **`ConfigFile.json` profiles are additive**: the `"all"` profile is always loaded first; named profiles (e.g., `FIAP`) are appended to the list and their replacements run in sequence after `"all"`.
- **`blockquote` and `pre` are mapped to table structures**, not to their natural HTML equivalents, because Word's HTML paste engine does not preserve `blockquote`/`pre` padding — the table wrapper is intentional.

## Code Style (as observed in the codebase)

- **Naming**: All identifiers in [`MarkDown.cs`](MdToOffice/MarkDown.cs) are in **Brazilian Portuguese** (`CursorDeLinhas`, `LeitorDeBlocos`, `ConversorDeParagrafo`, etc.). New types/methods in that file must follow this convention.
- **Naming**: [`Program.cs`](MdToOffice/Program.cs) and [`Models/ConfigFile.cs`](MdToOffice/Models/ConfigFile.cs) use English. Do not mix languages within a file.
- **Nullable enabled** project-wide; use `?` annotations and null-checks as needed.
- **ImplicitUsings enabled** — common BCL namespaces are available without explicit `using` directives.
- Internal implementation classes in `MarkDown.cs` use `internal sealed` — keep new helpers the same.
- Static helper classes use `internal static` (e.g., `PadroesMarkdown`, `TextoHtml`).
- Regex patterns are defined as `static readonly` fields on dedicated static classes, not inline.
