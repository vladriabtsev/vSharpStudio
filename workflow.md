# Development workflow
## New project
For new project next steps are needed:
 * Open Visual Studio
 * Open vSharpStudio window
 * ???
## Export/backup project
## Import project
## Existing project
### Apply changes
 * Save current config
 * Show changes. For deletion or data loss show warning.
 * Open modification transaction ?
 * Apply changes in files by renaming all renamed objects. Use Roslyn for renaming.
 * Regenerate files
   * Adding files. Add to project. Add to git if source code under git.
   * Renaming files. Remove from project and add under new name. Rename file in git.
   * Removing. Remove from project. Remove from git.
 * Save current config as last update
 * Commit modification transaction ?
 * In a case of exception restore files in state which was at the begining
 * 
## Compare and merge projects

# Links

https://modeling-languages.com/text-uml-tools-complete-list/

```plantuml
Alice -> Bob: Authentication Request
Bob --> Alice: Authentication Response

Alice -> Bob: Another authentication Request
Alice <-- Bob: Another authentication Response
```
