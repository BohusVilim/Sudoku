using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sudoku.Enums;

namespace Sudoku.Models
{
    public class Number
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public Rows Row { get; set; }
        public Columns Column { get; set; }
        public Sections Section { get; set; }
    }
}
