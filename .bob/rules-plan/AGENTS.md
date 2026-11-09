# AGENTS.md — Plan Mode

This file provides guidance when planning changes or designing features for this repository.

## Non-Obvious Architectural Constraints

- **Pipeline is linear and single-threaded** — read Clipboard → convert markdown → wrap HTML → post-process → write Clipboard. There is no async, no parallelism, and no shared state.
- **Post-processing (`SubstituirTagsHtml`) is order-dependent** — the `RemoveTag` list in `ConfigFile.json` is applied sequentially; a later rule may match output produced by an earlier rule. Reordering entries can silently break rendering.
- **Block parser is priority-ordered** — `ConversoresDeBloco.Padrao()` registers converters in a fixed list; the first converter whose `Reconhece()` returns `true` wins. `ConversorDeParagrafo` is always last (fallback). New converters must be inserted before it.
- **Inline protection (`TrechosProtegidos`)** uses private-use Unicode codepoints `\uE000`–`\uE001` as placeholder markers. Any feature that processes inline text must pass through this mechanism to avoid corrupting code spans, links, and images.
- **No test harness exists** — the `Mock/fiapMarkDownExample1.md` file is the only test fixture and is exercised manually. Any planned feature changes should account for the absence of automated validation.
- **Windows-only constraint is structural** — `System.Windows.Forms.Clipboard` and the CF_HTML format are Windows-specific. There is no cross-platform path; do not plan to abstract this unless willing to replace the clipboard mechanism entirely.
- **CSS is not embedded in code** — stylesheets are external files loaded at runtime from the `Style/` directory. Adding a new style profile requires a new `.css` file, a `.csproj` entry, and a `Program.cs` switch case.
