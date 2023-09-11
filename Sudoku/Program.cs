using Sudoku.Services;

var sudoku = new Sudoku.Models.Sudoku();
var numberService = new NumberService();
var sudokuService = new SudokuService(sudoku, numberService);

sudokuService.CreateSudoku();

int i = 0;

foreach (var number in sudoku.Numbers)
{
    if (number.Value == 0)
    {
        Console.Write(" ");
    }
    else
    {
        Console.Write(number.Value);
    }
    
    i++;

    if (i % 3 == 0 && i % 9 != 0)
    {
        Console.Write(" | ");
    }

    if (i % 9 == 0)
    {
        Console.WriteLine();
    }

    if (i % 27 == 0 && i % 81 != 0)
    {
        Console.WriteLine("---------------");
    }
}

Console.WriteLine();