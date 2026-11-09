# AGENTS.md — Ask Mode

This file provides guidance when answering questions about this repository.

## Non-Obvious Context

- **This is a CLI tool, not a library or GUI app** — it has a `Main()` entry point and runs to completion. The Windows Forms dependency is only for `System.Windows.Forms.Clipboard`; there is no actual form or UI.
- **Markdown parser is entirely custom** — there is no Markdig, CommonMark.NET, or other markdown library. All parsing logic lives in [`MdToOffice/MarkDown.cs`](../../MdToOffice/MarkDown.cs).
- **The output target is Microsoft Word via paste** — the HTML is not displayed in a browser. Structural choices (wrapping `<blockquote>` and `<pre>` in `<table>` elements, appending dozens of `<BR>` tags at the end) exist specifically to work around Word's HTML paste engine quirks.
- **Profiles in `ConfigFile.json`** are post-processing find-replace rules, not rendering themes. The CSS file selection is separate from the profile config.
- **`Temp/index.html`** is the last-run debug output — open it in a browser to inspect what would be pasted into Word.
- **The codebase is bilingual**: business logic classes (`MarkDown.cs`) use Brazilian Portuguese naming; entry point and model files use English.
