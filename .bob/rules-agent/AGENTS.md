# AGENTS.md — Agent (Coding) Mode

This file provides guidance to agents when writing or modifying code in this repository.

## Non-Obvious Coding Rules

- **Do not add NuGet packages** — the project intentionally has zero dependencies. Implement using BCL only.
- **All identifiers in `MarkDown.cs` must be in Brazilian Portuguese** — this is the existing convention for every class, method, field, and parameter in that file. English names in that file will be inconsistent.
- **`[STAThread]` must stay on `Main`** — do not move clipboard logic off the main thread; it will silently fail.
- **Adding a new CLI profile**: add the CSS file to `Style/`, add the profile name to `ConfigFile.json`, add the `CopyToOutputDirectory` entry in the `.csproj`, and add a `case` in the `switch` in `Program.cs`. All four places must be touched.
- **New block-level markdown converters** must implement `IConversorDeBloco` and be registered in `ConversoresDeBloco.Padrao()` — the order in that list determines parsing priority (first `Reconhece()` match wins).
- **Inline text transformations** go in `TextoInline.Processar()` via `TrechosProtegidos` — content that must not be double-processed (code spans, links, images) must be "protected" (replaced with a private-use Unicode placeholder) before emphasis is applied, then restored after.
- **`Temp/index.html` is committed** — do not gitignore it; it is the debug output artefact.
- **CF_HTML offsets** in `CriarClipboardHtml()` use `string.Length`, not UTF-8 byte count. If you ever need to support non-ASCII clipboard content correctly, this must be fixed to use `Encoding.UTF8.GetByteCount`.
