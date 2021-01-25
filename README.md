# JeopardyMVC
This Jeopardy version uses Entity Framework.  The following are stored in the **SQL database**:
1. **Categories** (id, name, active toggle-used to identify categories used for game play-T/F)
2. **Questions** (id, categoryID, question, point value)
3. **Answers** (id, questionID, answer, correct toggle-used to identify answer as true or false-T/F)

New categories can be created.  Data can be updated, displayed, or deleted.  The controller accesses database data and passes it to the selected view.
**Views** include:
1. **Home** - Welcome page
2. **Categories** - to interact with the stored categories data
3. **Questions** - to interact with the stored questions data
4. **Answers** - to interact with the stored answers data
5. **Play** - to play the Jeopardy game

## Jeopardy Game Play
The actual game play is controlled in JavaScript.  The CSS display property is used to toggle between the game board
grid view, the question view, and the completion box view.
