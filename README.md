# 🎄 BookAChristmasHam

Ett menybaserat C#-program för att hantera bokning av julskinka, användare och företag.

## 🧩 Designprinciper

### Direkt sparning vid CRUD-operationer

För att undvika att data går förlorad eller blir inkonsekvent när användaren navigerar mellan menyer, sparas alla ändringar direkt till fil i varje CRUD-metod (t.ex. `Add`, `Delete`, `Update`).

Detta är särskilt viktigt i menybaserade appar där användaren kan göra flera operationer utan att avsluta eller explicit spara. Genom att spara direkt säkerställs att:

- Alla ändringar är permanenta direkt  
- Systemet alltid är synkroniserat med filen  
- Ingen data går förlorad om användaren gör flera operationer i följd

## 📁 Projektstruktur



---

## 🚀 Kom igång

1. Öppna projektet i din IDE (t.ex. Visual Studio eller VS Code)
2. Kör `Program.cs` för att starta applikationen

```bash
dotnet run


