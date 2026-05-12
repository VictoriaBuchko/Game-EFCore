using Games.Data;
using Games.Models;
using Microsoft.EntityFrameworkCore;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

using var db = new GamesDbContext();

//створення бд
db.Database.Migrate();

//початкові дані
if (!db.Games.Any())
{
    db.Games.AddRange(
        new Game
        {
            Name = "The Witcher 3",
            Studio = "CD Projekt Red",
            Genre = "RPG",
            ReleaseDate = new DateTime(2015, 5, 19),
            GameMode = "Однокористувацький",
            CopiesSold = 50000000
        },
        new Game
        {
            Name = "Minecraft",
            Studio = "Mojang",
            Genre = "Sandbox",
            ReleaseDate = new DateTime(2011, 11, 18),
            GameMode = "Багатокористувацький",
            CopiesSold = 238000000
        },
        new Game
        {
            Name = "God of War",
            Studio = "Santa Monica Studio",
            Genre = "Action",
            ReleaseDate = new DateTime(2018, 4, 20),
            GameMode = "Однокористувацький",
            CopiesSold = 23000000
        },
        new Game
        {
            Name = "Among Us",
            Studio = "InnerSloth",
            Genre = "Party",
            ReleaseDate = new DateTime(2018, 6, 15),
            GameMode = "Багатокористувацький",
            CopiesSold = 15000000
        },
        new Game
        {
            Name = "Red Dead Redemption 2",
            Studio = "Rockstar Games",
            Genre = "Action",
            ReleaseDate = new DateTime(2018, 10, 26),
            GameMode = "Однокористувацький",
            CopiesSold = 61000000
        },
        new Game
        {
            Name = "Cyberpunk 2077",
            Studio = "CD Projekt Red",
            Genre = "RPG",
            ReleaseDate = new DateTime(2020, 12, 10),
            GameMode = "Однокористувацький",
            CopiesSold = 25000000
        }
    );

    db.SaveChanges();
    Console.WriteLine("Ігри успішно додані до бази даних!\n");
}

//всі ігри
Console.WriteLine("Всі ігри");
var allGames = db.Games.ToList();
foreach (var g in allGames)
{
    Console.WriteLine($"{g.Name}, {g.Studio}, {g.Genre}, {g.ReleaseDate:yyyy}, {g.GameMode}, {g.CopiesSold:#,0} копій");
}

//завдання 1 пошук
Console.WriteLine("\nза назвою гри: 'Minecraft'");
db.Games
    .Where(g => g.Name == "Minecraft")
    .ToList()
    .ForEach(g => Console.WriteLine($"{g.Name}, {g.Studio}"));

Console.WriteLine("\nза студією: 'CD Projekt Red'");
db.Games
    .Where(g => g.Studio == "CD Projekt Red")
    .ToList()
    .ForEach(g => Console.WriteLine($"{g.Name}, {g.Studio}"));

Console.WriteLine("\nза студією і назвою: 'Rockstar Games', 'Red Dead Redemption 2'");
db.Games
    .Where(g => g.Studio == "Rockstar Games" && g.Name == "Red Dead Redemption 2")
    .ToList()
    .ForEach(g => Console.WriteLine($"{g.Name}, {g.Studio}"));

Console.WriteLine("\nза стилем: 'RPG'");
db.Games
    .Where(g => g.Genre == "RPG")
    .ToList()
    .ForEach(g => Console.WriteLine($"{g.Name}, {g.Genre}"));

Console.WriteLine("\nза роком релізу: 2018");
db.Games
    .Where(g => g.ReleaseDate.Year == 2018)
    .ToList()
    .ForEach(g => Console.WriteLine($"{g.Name}, {g.ReleaseDate:dd.MM.yyyy}"));

//завдання 2 статистика
Console.WriteLine("\nОднокористувацькі ігри");
db.Games
    .Where(g => g.GameMode == "Однокористувацький")
    .ToList()
    .ForEach(g => Console.WriteLine($"{g.Name}"));

Console.WriteLine("\nБагатокористувацькі ігри");
db.Games
    .Where(g => g.GameMode == "Багатокористувацький")
    .ToList()
    .ForEach(g => Console.WriteLine($"{g.Name}"));

Console.WriteLine("\nГра з максимальною кількістю проданих копій");
var maxSold = db.Games.OrderByDescending(g => g.CopiesSold).First();
Console.WriteLine($"{maxSold.Name} — {maxSold.CopiesSold:#,0} копій");

Console.WriteLine("\nГра з мінімальною кількістю проданих копій");
var minSold = db.Games.OrderBy(g => g.CopiesSold).First();
Console.WriteLine($"{minSold.Name} — {minSold.CopiesSold:#,0} копій");

Console.WriteLine("\nТоп-3 найпопулярніших ігор");
db.Games
    .OrderByDescending(g => g.CopiesSold)
    .Take(3)
    .ToList()
    .ForEach(g => Console.WriteLine($"{g.Name} — {g.CopiesSold:#,0} копій"));

Console.WriteLine("\nТоп-3 найнепопулярніших ігор");
db.Games
    .OrderBy(g => g.CopiesSold)
    .Take(3)
    .ToList()
    .ForEach(g => Console.WriteLine($"{g.Name} — {g.CopiesSold:#,0} копій"));

//завдання 3 додавання
Console.WriteLine("\nДодавання нової гри");
Console.Write("Введіть назву нової гри: ");
string newName = Console.ReadLine() ?? "";

Console.Write("Студія: ");
string newStudio = Console.ReadLine() ?? "";

var exists = db.Games.Any(g => g.Name == newName && g.Studio == newStudio);

if (exists)
{
    Console.WriteLine("Така гра вже існує!");
}
else
{
    Console.Write("Жанр: ");
    string newGenre = Console.ReadLine() ?? "";

    Console.Write("Рік релізу (наприклад 2023): ");
    int newYear = int.TryParse(Console.ReadLine(), out int y) ? y : 2024;

    Console.Write("Режим (Однокористувацький / Багатокористувацький): ");
    string newMode = Console.ReadLine() ?? "";

    Console.Write("Кількість проданих копій: ");
    int newCopies = int.TryParse(Console.ReadLine(), out int c) ? c : 0;

    db.Games.Add(new Game
    {
        Name = newName,
        Studio = newStudio,
        Genre = newGenre,
        ReleaseDate = new DateTime(newYear, 1, 1),
        GameMode = newMode,
        CopiesSold = newCopies
    });

    db.SaveChanges();
    Console.WriteLine("Гру додано!");
}

//завдання 3 редагування
Console.WriteLine("\nРедагування гри");
Console.Write("Назва: ");
string editName = Console.ReadLine() ?? "";

Console.Write("Студія: ");
string editStudio = Console.ReadLine() ?? "";

var gameToEdit = db.Games
    .FirstOrDefault(g => g.Name == editName && g.Studio == editStudio);

if (gameToEdit == null)
{
    Console.WriteLine("Гру не знайдено!");
}
else
{
    Console.WriteLine($"Знайдено: {gameToEdit.Name}, {gameToEdit.Studio}, {gameToEdit.Genre}, {gameToEdit.GameMode}, {gameToEdit.CopiesSold:#,0}");
    Console.WriteLine("Що змінити? (1 - назву, 2 - студію, 3 - жанр, 4 - режим, 5 - продажі)");
    string choice = Console.ReadLine() ?? "";

    switch (choice)
    {
        case "1":
            Console.Write("Нова назва: ");
            gameToEdit.Name = Console.ReadLine() ?? gameToEdit.Name;
            break;
        case "2":
            Console.Write("Нова студія: ");
            gameToEdit.Studio = Console.ReadLine() ?? gameToEdit.Studio;
            break;
        case "3":
            Console.Write("Новий жанр: ");
            gameToEdit.Genre = Console.ReadLine() ?? gameToEdit.Genre;
            break;
        case "4":
            Console.Write("Новий режим: ");
            gameToEdit.GameMode = Console.ReadLine() ?? gameToEdit.GameMode;
            break;
        case "5":
            Console.Write("Нова кількість продажів: ");
            if (int.TryParse(Console.ReadLine(), out int newSales))
                gameToEdit.CopiesSold = newSales;
            break;
    }

    db.SaveChanges();
    Console.WriteLine("Оновлено!");
}

//завдання 3: видалення
Console.WriteLine("\nВидалення гри");
Console.Write("Назва: ");
string delName = Console.ReadLine() ?? "";

Console.Write("Студія: ");
string delStudio = Console.ReadLine() ?? "";

var gameToDelete = db.Games
    .FirstOrDefault(g => g.Name == delName && g.Studio == delStudio);

if (gameToDelete == null)
{
    Console.WriteLine("Гру не знайдено!");
}
else
{
    Console.Write($"Видалити '{gameToDelete.Name}'? (так/ні): ");
    if ((Console.ReadLine() ?? "").ToLower() == "так")
    {
        db.Games.Remove(gameToDelete);
        db.SaveChanges();
        Console.WriteLine("Видалено");
    }
    else
    {
        Console.WriteLine("Видалення скасовано.");
    }
}

Console.WriteLine("\nНатисніть будь-яку клавішу...");
Console.ReadKey();