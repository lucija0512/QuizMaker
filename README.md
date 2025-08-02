# Quiz Maker API

A simple .NET Web API for managing quizzes with extensible export functionality.

### Installation

1. Clone the repository
2. Update connection string in `appsettings.json`
3. Run database migrations
4. Start the application

## API Endpoints

### Quiz
- `GET /api/quiz` - Gets a paginated list of quizzes
- `GET /api/quiz/{id}` - Gets quiz by identifier
- `GET /api/quiz/questions` - Gets questions by search text
- `POST /api/quiz` - Creates a new quiz
- `PUT /api/quiz/{id}` - Updates an existing quiz
- `DELETE /api/quiz/{id}` - Soft deletes a quiz

### QuizExport
- `GET /api/quizexport/exporttypes` - Gets available export types
- `GET /api/quizexport/{id}` - Exports a quiz to a file of the specified type

## Managed Extensibility Framework
Enables extensible export functionality through MEF:
1. Add new `IExporter` implementation to `FileExporters` Class Library
2. Build and copy DLL to `ExtensionsLibrary` folder
3. New exporter is automatically available without recompiling
