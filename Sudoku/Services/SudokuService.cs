using Sudoku.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Services
{
    public class SudokuService
    {
        public Models.Sudoku _sudoku;
        public NumberService _numberService;
        public SudokuService(Models.Sudoku sudoku, NumberService numberService)
        {
            _sudoku = sudoku;
            _numberService = numberService;
        }

        public void CreateSudoku()
        {
            _sudoku.Numbers.AddRange(_numberService.GenerateNumbers());

            MakePuzzle();
        }

        public void MakePuzzle()
        {
            var random = new Random();
            var sections = _sudoku.Numbers.Select(a => a.Section).ToList();
            var uniqueSections = sections.Distinct().ToList();

            foreach (var section in uniqueSections)
            {
                foreach (var number in _sudoku.Numbers.Where(a => a.Section == section))
                {
                    int i = random.Next(1, 4);
                    if (i == 1)
                    {
                        number.Value = 0;
                    }
                }
            }
        }
    }
}
